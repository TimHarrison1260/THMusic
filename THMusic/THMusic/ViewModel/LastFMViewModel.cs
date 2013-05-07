//***************************************************************************************************
//Name of File:     AlbumViewModel.cs
//Description:      The view model supporting the Albums page, the SplitPage.xaml.
//Author:           Tim Harrison
//Date of Creation: Apr/May 2013.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using Core.Services;
using Core.Interfaces;
using Core.Model;

using THMusic.DataModel;
using THMusic.Data;
using THMusic.Navigation;

namespace THMusic.ViewModel
{
    /// <summary>
    /// This <c>LastFMViewModel</c> is responsible for managing access to the information required
    /// by the Albums List / Detail page.  It is accessed from the Group List view, which shows
    /// Totals for each Group defined.
    /// <para>
    /// The data is not loaded from the constructor, as this would immediately be overridden by 
    /// the data requirements for the suplied UniquId of the Group that was selected from the Group
    /// page.  Instead it is handled from the setter of the <c>UniqueId</c> property.  This accepts
    /// the uniqueId passed from the Group page, and uses it to load the data from the Music
    /// Collection.  
    /// </para>
    /// <para>
    /// The UniqueId property consists of a <see cref="THMusic.DataModel.GroupId"/>, which is made 
    /// up of the <see cref="THMusic.DataModel.GroupTypeEnum"/> the Id of the corresponding grouping 
    /// type.
    /// </para>
    /// <para>
    /// The data is loaded into the model, as a result of the Search button being clicked
    /// The data is retrieved from the LastFM "album.GetInfo" call.  the data is imported to the
    /// Domain model, by clicking the Import button being clicked.  once the data is imported
    /// rthe page navigates back to the main Grouping page.
    /// </para>
    /// </summary>
    /// <remarks>
    /// This class contains properties that the main View can data bind to.
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// You can also use Blend to data bind with the tool's support.
    /// See http://www.galasoft.ch/mvvm
    /// </remarks>
    public class LastFMViewModel : ViewModelBase
    {
        private readonly INavigationService _navigator; //  Navigation interface
        private readonly ILastFMModelDataService _dataService;    //  The model statservice classed that consumes the service

        /// <summary>
        /// ctor: Initialised a new instance of the AlbumViewModel class
        /// </summary>
        /// <param name="ArtistRepository">A reference to the ArtistRepository, injected at runtime</param>
        /// <param name="LastFMService">A reference to the LastFMService, injected at runtime.</param>
        /// <remarks>
        /// This follows the example supplied by the MVVMLight framework with modifications to use
        /// constructor injection via the SimpleIoC container.
        /// </remarks>
        public LastFMViewModel(ILastFMModelDataService LastFMModelDataService, INavigationService NavigationService)
        {
            if (LastFMModelDataService == null)
                throw new ArgumentNullException("LastFMModelDataservice", "No valid LastFM DataService supplied");
            _dataService = LastFMModelDataService;
            if (NavigationService == null)
                throw new ArgumentNullException("NavigationService", "No valid Navigation Service supplied");
            _navigator = NavigationService;

            //  Set up the data
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
                //  TODO: Add Design time data, though not a priority
            }
            else
            {
                // Code runs "for real"
                //  No runtime data required here, the model is loaded which the page
                //  is navigated to as it accepts a group Id, and type to determine
                //  the correct grouping to load.  The options are Artists, Genres or 
                //  Playlists.
            }

            //  Set up the command handlers 
            LastFMSearchCommand = new RelayCommand(lastFMSearchHandler);
            LastFMClearSearchCommand = new RelayCommand(lastFMClearSearchHandler);
            LastFMImportCommand = new RelayCommand(lastFMImportHandler);
        }


        //  ***************************  Commands  ***********************************
        public RelayCommand LastFMSearchCommand { get; set; }
        private async void lastFMSearchHandler()
        {
            //  Initialte the search using the values from the search criteria.
            if (this.SearchAlbum != null && this.SearchArtist != null)
                await GetLastFMAlbumAsync(this.SearchAlbum, this.SearchArtist);
        }

        public RelayCommand LastFMClearSearchCommand { get; set; }
        private void lastFMClearSearchHandler()
        {
            this.SearchAlbum = string.Empty;
            this.SearchArtist = string.Empty;
            this.LastFMAlbum = new AlbumModel();
        }

        public RelayCommand LastFMImportCommand { get; set; }
        private async void lastFMImportHandler()
        {
            if (this.LastFMAlbum != null)
            {
                string success = await _dataService.ImportAlbumAsync(this.LastFMAlbum);
                //  now navigate back to the front page
                _navigator.Navigate(typeof(ItemsPage), success);
            }
        }


        //  ***************************  Properties  ***********************************
        //  ***************************  Properties  ***********************************
        //  ***************************  Properties  ***********************************

        //  Holds the search string for the Artist name
        private string _searchArtist;
        /// <summary>
        /// Gets of sets the search string that identifies the Artist to search for
        /// </summary>
        public string SearchArtist
        {
            get { return _searchArtist; }
            set
            {
                if (value != null && _searchArtist != value)
                {
                    _searchArtist = value;
                    RaisePropertyChanged(() => SearchArtist);
                }
            }
        }

        //  Holds the search string for the Album name
        private string _searchAlbum;
        /// <summary>
        /// Gets or sets the search string that identifies the Album to search for.
        /// </summary>
        public string SearchAlbum
        {
            get { return _searchAlbum; }
            set
            {
                if (value != null && _searchAlbum != value)
                {
                    _searchAlbum = value;
                    RaisePropertyChanged(() => SearchAlbum);
                }
            }
        }

        //  Holds the details of the search results, always I album record.
        private AlbumModel _lastFMAlbum;
        /// <summary>
        /// Gets or sets the album returned from the search
        /// </summary>
        public AlbumModel LastFMAlbum
        {
            get { return _lastFMAlbum; }
            set
            {
                if (value != null && _lastFMAlbum != value)
                {
                    _lastFMAlbum = value;
                    RaisePropertyChanged(() => LastFMAlbum);
                }
            }
        }


        //  ***************************  Supporting methods  ***************************
        //  ***************************  Supporting methods  ***************************
        //  ***************************  Supporting methods  ***************************

        /// <summary>
        /// Private method that initiates loading the data context on startup.
        /// This is done asynchronously.
        /// </summary>
        /// <param name="Id">The Id of the Group.</param>
        /// <param name="groupType">The Type of the Group (Artist, Genre or Playlist)</param>
        /// <remarks>
        /// This call is separated to a private method so the call can be 'await'ed.  To call 
        /// this from the mainViewModel constructor would require it to be decorated as 
        /// 'async' which is not possible.
        /// </remarks>
        private async Task GetLastFMAlbumAsync(string album, string artist)
        {
            //  Load the album
            this.LastFMAlbum = await _dataService.GetLastFMAlbumInfoAsync(artist, album);
        }

    }

}