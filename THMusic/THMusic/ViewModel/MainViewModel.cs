//***************************************************************************************************
//Name of File:     MainViewModel.cs
//Description:      The view model supporting the 1st page, the ItemsPage.
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
using GalaSoft.MvvmLight.Ioc;
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
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private IGroupModelDataService _dataService;
        private INavigationService _navigator;


        /// <summary>
        /// ctor: Initialised a new instance of the MainViewModel class
        /// </summary>
        /// <param name="ArtistRepository">A reference to the ArtistRepository, injected at runtime</param>
        /// <param name="LastFMService">A reference to the LastFMService, injected at runtime.</param>
        /// <remarks>
        /// This follows the example supplied by the MVVMLight framework with modifications to use
        /// constructor injection via the SimpleIoC container.
        /// </remarks>
        public MainViewModel(Data.IGroupModelDataService GroupModelDataService, INavigationService NavigationService)
        {
            if (GroupModelDataService == null)
                throw new ArgumentNullException("GroupModelDataService", "No valid DataService supplied");
            _dataService = GroupModelDataService;
            if (NavigationService == null)
                throw new ArgumentNullException("NavigationService", "No valid Navigation Service supplied");
            _navigator = NavigationService;

            //  Set up the data
            if (IsInDesignMode)
            {
                //  TODO: refactor the MainModelHelper to be an injected reference
                //          This will allow a design time version to be injected that doesn't
                //          use Async calls and have a static version in the UI.Data layer
                //          alongside the DesignTimeRepositories.

                // Code runs in Blend --> create design time data.
                LoadGroupsAsync();
            }
            else
            {
                // Code runs "for real"
                //  No runtime data required here, the model is loaded which the page
                //  is navigated to as it accepts a GroupType, and type to determine
                //  the correct grouping to load.  The options are Artists, Genres or 
                //  Playlists.
                //  AVoiding placing any Async calls within the constructor as this
                //  can cause deadlocks on the UI thread  and various other problems.
            }

            //  Bind the handlers to the commands
            ImportMP3Command = new RelayCommand(ImportMP3Handler);
            ImportLastFMCommand = new RelayCommand(ImportLastFMHandler);
            RefreshGroupingCommand = new RelayCommand(RefreshGroupingHandler);
        }




        //  ************************  Properties for binding   **************************
        //  ************************  Properties for binding   **************************
        //  ************************  Properties for binding   **************************

        //  Select the grouping required to display
        public bool IsGroupArtist
        {
            get { return (_grouping == GroupTypeEnum.Artist) ? true: false; }
            set
            {
                SetGrouping(GroupTypeEnum.Artist);
            }
        }

        public bool IsGroupGenre
        {
            get { return (_grouping == GroupTypeEnum.Genre) ? true: false; }
            set
            {
                SetGrouping(GroupTypeEnum.Genre);
            }
        }

        public bool IsGroupPlaylist
        {
            get { return (_grouping == GroupTypeEnum.Playlist) ? true: false; }
            set
            {
                SetGrouping(GroupTypeEnum.Playlist);
            }
        }

        private GroupTypeEnum _grouping = GroupTypeEnum.Artist;  //    Default is grouped by Artist
        private void SetGrouping(GroupTypeEnum selectedGrouping)
        {
            //  Set the _grouping to that selected
            _grouping = selectedGrouping;
            //  Notify that all three properties have changed
            RaisePropertyChanged(() => IsGroupArtist);
            RaisePropertyChanged(() => IsGroupGenre);
            RaisePropertyChanged(() => IsGroupPlaylist);
            //  Now activate the Refresh button 
            _isRefreshVisible = true;
            RaisePropertyChanged(() => IsRefreshVisible);
        }

        private bool _isRefreshVisible = false;
        public bool IsRefreshVisible
        {
            get { return _isRefreshVisible; }
            set
            {
                _isRefreshVisible = value;
                RaisePropertyChanged(() => IsRefreshVisible);
            }
        }


        public string AppName
        {
            get { return GetLocalisedAppName(); }
        }

        private string GetLocalisedAppName()
        {
            return Helpers.LocalisationHelper.LocaliseAppName(_grouping);
        }

        
        private List<GroupModel> _groups;
        public List<GroupModel> Groups
        {
            get { return _groups; }
            set
            {
                if (value != null && _groups != value)
                {
                    _groups = value;
                    RaisePropertyChanged(() => Groups);
                }
            }
        }

        private GroupModel _selectedGroup;
        public GroupModel SelectedGroup
        {
            get { return _selectedGroup; }
            set
            {
                if (value != null && _selectedGroup != value)
                {
                    _selectedGroup = value;
                    RaisePropertyChanged(() => SelectedGroup);
                    //  Navigate away from this page. to the AlbumsList and Details page (SplitPage)
                    _navigator.Navigate(typeof(SplitPage), _selectedGroup.UniqueId);
                }
            }
        }

        /// <summary>
        /// Public method to allow a refresh of the data model to be initiated
        /// </summary>
        /// <returns>A Task</returns>
        /// <remarks>
        /// A refresh of the model would be required if an Album has been imported
        /// from LastFM, which causes the app to navigate back to the Groups page, 
        /// this one.  This would be initiated from the LoadState method in the
        /// code behind this page.
        /// </remarks>
        public async Task RefreshData()
        {
            await LoadGroupsAsync();
        }


        //  ************************  Commands for binding   ****************************
        //  ************************  Commands for binding   ****************************
        //  ************************  Commands for binding   ****************************
        public RelayCommand ImportLastFMCommand { get; set; }

        private void ImportLastFMHandler()
        {
            //  Navigate away to the Import page for LastFM information.
            _navigator.Navigate(typeof(ImportLastFM));
        }

        public RelayCommand ImportMP3Command { get; set; }

        private void ImportMP3Handler()
        {
            //  Navigate away to the Import page for MP3 files

        }

        public RelayCommand RefreshGroupingCommand { get; set; }

        private async void RefreshGroupingHandler()
        {
            //  Hide the Refresh button
            _isRefreshVisible = false;
            RaisePropertyChanged(() => IsRefreshVisible);

            //  Call into load data with the correct parameters
            await LoadGroupsAsync();

            RaisePropertyChanged(() => AppName);
        }



        /// <summary>
        /// Private method that initiates loading the data context on startup.
        /// This is done asynchronously.
        /// </summary>
        /// <remarks>
        /// This call is separated to a private method so the call can be 'await'ed.  To call 
        /// this from the mainViewModel constructor would require it to be decorated as 
        /// 'async' which is not possible.
        /// </remarks>
        private async Task LoadGroupsAsync()
        {
            this.Groups = await _dataService.LoadAsync(_grouping);
        }

    }
}