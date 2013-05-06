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

using GalaSoft.MvvmLight;

using Core.Services;
using Core.Interfaces;
using Core.Model;

using THMusic.DataModel;


namespace THMusic.ViewModel
{
    /// <summary>
    /// This <c>AlcumsViewModel</c> is responsible for managing access to the information required
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
    /// </summary>
    /// <remarks>
    /// This class contains properties that the main View can data bind to.
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// You can also use Blend to data bind with the tool's support.
    /// See http://www.galasoft.ch/mvvm
    /// </remarks>
    public class AlbumsViewModel : ViewModelBase
    {
        private readonly IAlbumRepository _albumRepository;
        private readonly IArtistRepository _artistRepository;

        /// <summary>
        /// ctor: Initialised a new instance of the AlbumViewModel class
        /// </summary>
        /// <param name="ArtistRepository">A reference to the ArtistRepository, injected at runtime</param>
        /// <param name="LastFMService">A reference to the LastFMService, injected at runtime.</param>
        /// <remarks>
        /// This follows the example supplied by the MVVMLight framework with modifications to use
        /// constructor injection via the SimpleIoC container.
        /// </remarks>
        public AlbumsViewModel(IAlbumRepository AlbumRepository, IArtistRepository ArtistRepository)
        {
            if (AlbumRepository == null)
                throw new ArgumentNullException("AlbumRepository", "No valid Repository supplied");
            _albumRepository = AlbumRepository;
            if (ArtistRepository == null)
                throw new ArgumentNullException("ArtistRepository", "No valid Repository supplied");
            _artistRepository = ArtistRepository;


            //  Set up the data
            if (IsInDesignMode)
            {
                //  TODO: refactor the AlbumModelHelper to be an injected reference
                //          This will allow a design time version to be injected that doesn't
                //          use Async calls and have a static version in the UI.Data layer
                //          alongside the DesignTimeRepositories.

                // Code runs in Blend --> create design time data.
                int Id = 1;
                LoadViewModelAsync(Id, GroupTypeEnum.Artist);
                
                //this.Albums = Helpers.AlbumModelHelper.LoadAlbumsAsync(Id, "Artist", _albumRepository);
                //this.CurrentAlbum = Albums[0];
            }
            else
            {
                // Code runs "for real"
//                LoadAlbums(1, GroupTypeEnum.Artist);
                //this.Groups = await Helpers.GroupModelHelper.LoadAsync(_artistRepository);
            }
        }


        //  ***************************  Properties  ***********************************
        //  ***************************  Properties  ***********************************
        //  ***************************  Properties  ***********************************

        //  Holds the id of the group, which includes the groups type
        private GroupId _uniqueId;
        public GroupId UniqueId
        {
            get { return _uniqueId; }
            set
            {
                if (value != null && _uniqueId != value)
                {
                    _uniqueId = value;
                    RaisePropertyChanged(() => UniqueId);

                    //  Now load the data
                    LoadViewModelAsync(_uniqueId.Id, _uniqueId.Type);
                }
            }
        }


        //  Holds the name of the selected group, passed from the MainViewModel
        private string _groupName;
        public string GroupName
        {
            get { return _groupName; }
            set
            {
                if (value != null && _groupName != value)
                {
                    _groupName = value;
                    RaisePropertyChanged(() => GroupName);
                }
            }
        }

        //  Holds the list of albums belonging to the selected Group.
        private List<AlbumModel> _albums;
        public List<AlbumModel> Albums
        {
            get { return _albums; }
            set
            {
                if (value != null && _albums != value)
                {
                    _albums = value;
                    RaisePropertyChanged(() => Albums);
                }
            }
        }

        //  Holds the details of the currently selected Album
        private AlbumModel _currentAlbum;
        public AlbumModel CurrentAlbum
        {
            get { return _currentAlbum; }
            set
            {
                if (value != null && _currentAlbum != value)
                {
                    _currentAlbum = value;
                    RaisePropertyChanged(() => CurrentAlbum);
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
        private async void LoadViewModelAsync(int Id, GroupTypeEnum groupType)
        {
            //  Load the albums
            this.Albums = await Helpers.AlbumModelHelper.LoadAlbumsAsync(Id, groupType, _albumRepository);
            this.CurrentAlbum = Albums[0];
            //  Load the Group Name
            this.GroupName = await Helpers.AlbumModelHelper.LoadGroupNameAsync(Id, groupType, _artistRepository);
        }

    }

}