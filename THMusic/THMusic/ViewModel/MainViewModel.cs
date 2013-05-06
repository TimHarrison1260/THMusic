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
        //private ILastFMService _lastFMService;
        private IArtistRepository _artistRepository;
        private INavigationService _navigator;


        //  ************************  Properties for binding   **************************
        //  ************************  Properties for binding   **************************
        //  ************************  Properties for binding   **************************

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
                if (_selectedGroup != value)
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
            await LoadGroups();
        }


        //  ************************  Commands for binding   ****************************
        //  ************************  Commands for binding   ****************************
        //  ************************  Commands for binding   ****************************
        public RelayCommand ImportLastFMCommand { get; set; }

        private void ImportLastFMHandler()
        {
            //  Navigate away to the Impoft page for LastFM information.
            _navigator.Navigate(typeof(ImportLastFM));
        }

        public RelayCommand ImportMP3Command { get; set; }

        private void ImportMP3Handler()
        {
            //  Navigate away to the Import page for MP3 files

        }
        

        /// <summary>
        /// ctor: Initialised a new instance of the MainViewModel class
        /// </summary>
        /// <param name="ArtistRepository">A reference to the ArtistRepository, injected at runtime</param>
        /// <param name="LastFMService">A reference to the LastFMService, injected at runtime.</param>
        /// <remarks>
        /// This follows the example supplied by the MVVMLight framework with modifications to use
        /// constructor injection via the SimpleIoC container.
        /// </remarks>
        public MainViewModel(IArtistRepository ArtistRepository, INavigationService NavigationService)   //, ILastFMService LastFMService)
        {
            if (ArtistRepository == null)
                throw new ArgumentNullException("ArtistRepository", "No valid Repository supplied");
            _artistRepository = ArtistRepository;
            if (NavigationService == null)
                throw new ArgumentNullException("NavigationService", "No valid Navigation Service supplied");
            _navigator = NavigationService;

            //if (LastFMService == null)
            //    throw new ArgumentNullException("LastFMService", "No valid service supplied");
            //_lastFMService = LastFMService;

            //  Set up the data
            if (IsInDesignMode)
            {
                //  TODO: refactor the MainModelHelper to be an injected reference
                //          This will allow a design time version to be injected that doesn't
                //          use Async calls and have a static version in the UI.Data layer
                //          alongside the DesignTimeRepositories.

                // Code runs in Blend --> create design time data.
                LoadGroups();
//                this.Groups = Helpers.GroupModelHelper.Load(GroupTypeEnum.Artist, _artistRepository);
            }
            else
            {
                // Code runs "for real"
                LoadGroups();
            }


            ImportMP3Command = new RelayCommand(ImportMP3Handler);
            ImportLastFMCommand = new RelayCommand(ImportLastFMHandler);
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
        private async Task LoadGroups()
        {
            this.Groups = await Helpers.GroupModelHelper.LoadAsync(GroupTypeEnum.Artist, _artistRepository);
            if (this.Groups.Count > 0)
                this.SelectedGroup = this.Groups[0];
            else
                this.SelectedGroup = new GroupModel();
        }

    }
}