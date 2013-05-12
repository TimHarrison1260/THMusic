//***************************************************************************************************
//Name of File:     Configuration.cs
//Description:      Provides the configuration for the SimpleIoC used for DI, part of the MVVMLight framework.
//Author:           Tim Harrison
//Date of Creation: Apr/May 2013.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************

using GalaSoft.MvvmLight.Ioc;

using Core.Services;
using Core.Factories;
using Core.Model;
using Core.Interfaces;
using Infrastructure.Services;
using Infrastructure.Data;
using Infrastructure.Repositories;

namespace IoC
{
    /// <summary>
    /// This <c>Configuration</c> class is responsible for configuring the SimpleIoC
    /// </summary>
    public static class Configuration
    {
        /// <summary>
        /// Register the IoC kernel and the common dependencies
        /// </summary>
        /// <param name="iocKernel">The instance of the SimpleIoC container</param>
        public static void RegisterServices(SimpleIoc iocKernel)
        {
            //  Register the Repositories
            iocKernel.Register<IArtistRepository, ArtistRepository>();
            iocKernel.Register<IGenreRepository, GenreRepository>();
            iocKernel.Register<IPlaylistRepository, PlaylistRepository>();
            iocKernel.Register<IAlbumRepository, AlbumRepository>();

            //  A special repository to expose the persistence methods so
            //  the Save method can be called when the application is suspending
            iocKernel.Register<IPersistenceRepository, PersistenceRepository>();

            //  Register the Services
            iocKernel.Register<ILastFMService, LastFmProxy>();
            iocKernel.Register<IMusicFileService, MusicFileService>();

            //  Register the factories
            iocKernel.Register<AbstractFactory<Artist>, ArtistFactory>();
            iocKernel.Register<AbstractFactory<Wiki>, WikiFactory>();
            iocKernel.Register<AbstractFactory<Image>, ImageFactory>();
            iocKernel.Register<AbstractFactory<Genre>, GenreFactory>();
            iocKernel.Register<AbstractFactory<PlayList>, PlaylistFactory>();
            //  register here, it is dependent on the Artist and Wiki factories.
            iocKernel.Register<AbstractFactory<Album>, AlbumFactory>();
            //  regist after album, it is dependent on Album and Artist factories.
            iocKernel.Register<AbstractFactory<Track>, TrackFactory>();
        }

        /// <summary>
        /// Register the Live instance of the datasource with the SimpleIoC
        /// </summary>
        /// <param name="iocKernel">the instance of the SimpleIoC container</param>
        public static void RegisterDataSource(SimpleIoc iocKernel)
        {
            iocKernel.Register<IUnitOfWork, MusicCollection>(true);
        }
    }
}
