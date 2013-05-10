//***************************************************************************************************
//Name of File:     ViewModelLocator.cs
//Description:      The viewModelLocator is responsible for initialising the IoC and viewModels
//                  The template is supplied by GalaSoft/MVVMLught.
//Author:           Tim Harrison
//Date of Creation: Apr/May 2013.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************
/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:THMusic"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

using THMusic.Data;

namespace THMusic.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            //  call the IoC project to map the application wide services
//            IoC.Configuration.RegisterServices(SimpleIoc.Default);


            if (ViewModelBase.IsInDesignModeStatic)
            {
                // Create design time view services and models
                SimpleIoc.Default.Register<Core.Interfaces.IArtistRepository, Design.DesignArtistRepository>();

                //SimpleIoc.Default.Register<Core.Interfaces.IGenreRepository, Design.DesignGenrerepository>();

                SimpleIoc.Default.Register<Data.IGroupModelDataService, Design.DesignGroupModelDataService>();
                //IoC.Configuration.ResisterDesignDataSource(SimpleIoc.Default);
            }
            else
            {
                //  call the IoC project to map the application wide services
                IoC.Configuration.RegisterServices(SimpleIoc.Default);
                
                // Create run time view services and models
                //SimpleIoc.Default.Register<IDataService, DataService>();
                IoC.Configuration.RegisterDataSource(SimpleIoc.Default);

                //  Group model data service for main page (ItemPage)
                SimpleIoc.Default.Register<IGroupModelDataService, GroupModelDataService>();
                SimpleIoc.Default.Register<IAlbumModelDataService, AlbumModelDataService>();
                SimpleIoc.Default.Register<ILastFMModelDataService, LastFMModelDataService>();
                SimpleIoc.Default.Register<IMusicFileDataService, MusicFileDataService>();

            }

            //  Navigation service
            SimpleIoc.Default.Register<Navigation.INavigationService, Navigation.NavigationService>();

            //  Register the View Models and other UI Services
            //  MainViewModel, used on the GroupPage: (ItemsPage.xaml)
            SimpleIoc.Default.Register<MainViewModel>();        // registers a concrete implementation
            //  AlbumsViewModel, used on the Albums page: (SplitPage.xaml)
            SimpleIoc.Default.Register<AlbumsViewModel>();
            //  LastFMViewModel, used for importing album information from LastFM.
            SimpleIoc.Default.Register<LastFMViewModel>();

        }

        /// <summary>
        /// Gets the instance of the MainViewModel
        /// </summary>
        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        /// <summary>
        /// Gets the instance of the AlbumsViewmodel
        /// </summary>
        public AlbumsViewModel Albums
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AlbumsViewModel>();
            }
        }

        /// <summary>
        /// Gets the instance of the lastFmViewModel
        /// </summary>
        public LastFMViewModel LastFM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<LastFMViewModel>();
            }
        }

        /// <summary>
        /// A Cleanup method.
        /// </summary>
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}