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
using GalaSoft.MvvmLight.Command;

using THMusic.DataModel;
using THMusic.Data;
using THMusic.Navigation;
using Windows.Storage.Pickers;
using Windows.Storage;


namespace THMusic.ViewModel
{
    /// <summary>
    /// This <c>MainViewModel</c> is responsible for managing access to the information required
    /// by the Group summary (main) page.  It is the application main page, and shows the albums
    /// within the collection summarised by Artist (the default) or by Genre or Playlist.
    /// <para>
    /// The data is not loaded from the constructor, rather the load is triggered from the LoadState
    /// method in the code bihind.  this avoid calling the asynchronous load method from the
    /// constructor which is not a good idea as deadlocks can occur especially if the medhod
    /// is being called from the UI, which it would be.
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
    public class MainViewModel : ViewModelBase
    {
        private IGroupModelDataService _dataService;
        private readonly IMusicFileDataService _musicFileDataService;
        private INavigationService _navigator;


        /// <summary>
        /// ctor: Initialised a new instance of the MainViewModel class
        /// </summary>
        /// <param name="GroupModelDataService">Instance of the GroupModelDataService</param>
        /// <param name="MusicFileDataService">Instance of the MusicFileDataService</param>
        /// <param name="NavigationService">Instance of the Navigation service</param>
        /// <remarks>
        /// This follows the example supplied by the MVVMLight framework with modifications to use
        /// constructor injection via the SimpleIoC container.
        /// </remarks>
        public MainViewModel(Data.IGroupModelDataService GroupModelDataService, IMusicFileDataService MusicFileDataService , INavigationService NavigationService)
        {
            if (GroupModelDataService == null)
                throw new ArgumentNullException("GroupModelDataService", "No valid DataService supplied");
            _dataService = GroupModelDataService;
            if (MusicFileDataService == null)
                throw new ArgumentNullException("MusicfileDataService", "No valid MusicFile DataService supplied");
            _musicFileDataService = MusicFileDataService;
            if (NavigationService == null)
                throw new ArgumentNullException("NavigationService", "No valid Navigation Service supplied");
            _navigator = NavigationService;

            //  Set up the data
            if (IsInDesignMode)
            {
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
        /// <summary>
        /// Indicates if the Group currently displayed is an Artist
        /// </summary>
        public bool IsGroupArtist
        {
            get { return (_grouping == GroupTypeEnum.Artist) ? true: false; }
            set
            {
                SetGrouping(GroupTypeEnum.Artist);
            }
        }

        /// <summary>
        /// Indicates if the Group currently displayed is an Genre
        /// </summary>
        public bool IsGroupGenre
        {
            get { return (_grouping == GroupTypeEnum.Genre) ? true: false; }
            set
            {
                SetGrouping(GroupTypeEnum.Genre);
            }
        }

        /// <summary>
        /// Indicates if the Group currently displayed is a Playlist
        /// </summary>
        public bool IsGroupPlaylist
        {
            get { return (_grouping == GroupTypeEnum.Playlist) ? true: false; }
            set
            {
                SetGrouping(GroupTypeEnum.Playlist);
            }
        }

        private GroupTypeEnum _grouping = GroupTypeEnum.Artist;  //    Default is grouped by Artist
        /// <summary>
        /// Sets the current display grouping
        /// </summary>
        /// <param name="selectedGrouping">The selected grouping</param>
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
        /// <summary>
        /// Indicates if the Refresh data button is visible.
        /// </summary>
        public bool IsRefreshVisible
        {
            get { return _isRefreshVisible; }
            set
            {
                _isRefreshVisible = value;
                RaisePropertyChanged(() => IsRefreshVisible);
            }
        }

        /// <summary>
        /// Gets the Application name, which is localised
        /// </summary>
        public string AppName
        {
            get { return GetLocalisedAppName(); }
        }

        //  calls the helper class to localise the application name
        private string GetLocalisedAppName()
        {
            return Helpers.LocalisationHelper.LocaliseAppName(_grouping);
        }

        
        private List<GroupModel> _groups;
        /// <summary>
        /// Gets or sets the collection Groups on the page
        /// </summary>
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

        //  Testing only
        private void UpdateGroup(int idx, GroupModel group)
        {
            _groups[idx] = group;
            //  This does not cause the UI to refresh.
            RaisePropertyChanged(() => Groups);
        }
        private void AddGroup(GroupModel group)
        {
            _groups.Add(group);
            RaisePropertyChanged(() => Groups);
        }


        private GroupModel _selectedGroup;
        /// <summary>
        /// Gets or sets the currently selected group, via the UI
        /// </summary>
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
        /// <summary>
        /// Handles the ImportLastFMCommand, raised by the LastFM button on the bottom appbar
        /// </summary>
        public RelayCommand ImportLastFMCommand { get; set; }
        private void ImportLastFMHandler()
        {
            //  Navigate away to the Import page for LastFM information.
            _navigator.Navigate(typeof(ImportLastFM));
        }

        /// <summary>
        /// Handles the ImportMP3Command, raise by the Import MP3 button on the bottom appvar
        /// </summary>
        public RelayCommand ImportMP3Command { get; set; }
        private async void ImportMP3Handler()
        {
            //  Handle this in the UI, it's a UI element.

            // Set up and launch the Open Picker
            FileOpenPicker fileOpenPicker = new FileOpenPicker();
            fileOpenPicker.ViewMode = PickerViewMode.List;
            fileOpenPicker.FileTypeFilter.Add(".mp3");  //  General
            fileOpenPicker.FileTypeFilter.Add(".m4a");  //  iTunes
            fileOpenPicker.FileTypeFilter.Add(".wma");  //  Windows media
            fileOpenPicker.CommitButtonText = "Import";
            fileOpenPicker.SuggestedStartLocation = PickerLocationId.MusicLibrary;
            IReadOnlyList<StorageFile> files = await fileOpenPicker.PickMultipleFilesAsync();
            if (files.Count > 0)
            {
                //  Process all tracks selected.
                List<int> affectedGroups = await _musicFileDataService.ProcessMusicFiles(files, _grouping);

                //foreach (var groupId in affectedGroups)
                //{
                //    //  Get the group details
                //    var groupSummary = await _dataService.LoadGroupAsync(groupId, _grouping);

                //    //  Update the Groups property, which should refresh only that group
                //    var groupExists = _groups.FirstOrDefault(g => g.UniqueId.Id == groupId);
                //    if (groupExists != null)
                //    {
                //        var idx = _groups.IndexOf(groupExists);
                //        UpdateGroup(idx, groupSummary);
                //    }
                //    else
                //    {
                //        AddGroup(groupSummary);
                //    }
                //}


                await LoadGroupsAsync();
            }

        }


        /// <summary>
        /// Handles the RefreshGroupingCommand raised by the Refresh data button on the top appbar
        /// </summary>
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