﻿//***************************************************************************************************
//Name of File:     PersistenceRepository.cs
//Description:      The repository uised to control the Windwos 8 Store style of loading and persisting
//                      when the application starts and stops.
//Author:           Tim Harrison
//Date of Creation: Apr/May 2013.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************
using System;
using System.Threading.Tasks;

using Core.Interfaces;
using Core.Factories;
using Core.Model;
using Infrastructure.Data;
using Infrastructure.Helpers;

namespace Infrastructure.Repositories
{
    /// <summary>
    /// This <c>PersistenceRepository</c> class is intended to support the retrieval and 
    /// persistence of the In-Memory context when the Windows 8 Store application starts
    /// and stops or Shutsdown.  This is a standard model for a Windows 8 Store app, so
    /// it makes sence to abstract this sort of functionality away from the other repositories
    /// but provide access to the Load and Save routines though the control of a repository
    /// </summary>
    public class PersistenceRepository : IPersistenceRepository
    {
        /// <summary>
        /// Holds the instance of the in-memory context
        /// </summary>
        private IUnitOfWork _unitOfWork;        // Not readonly, it needs to be modified once the data is loaded.

        private BuildNavigationProperties _navigationProperyBuilder;

        private readonly AbstractFactory<Artist> _artistFactory;
        private readonly AbstractFactory<Album> _albumFactory;
        private readonly AbstractFactory<Track> _trackFactory;
        private readonly AbstractFactory<Image> _imageFactory;
        private readonly AbstractFactory<Genre> _genreFactory;
        private readonly AbstractFactory<PlayList> _playlistFactory;
        private readonly AbstractFactory<Wiki> _wikiFactory;

        /// <summary>
        /// ctor: accepts the injected instance of the in-memory context
        /// </summary>
        /// <param name="UnitOfWork">The instance of the context</param>
        /// <param name="ArtistFactory">Instance of the ArtistFactory</param>
        /// <param name="AlbumFactory">Instance of the albumFactory</param>
        /// <param name="TrackFactory">Instance of the TrackFactory</param>
        /// <param name="ImageFactory">Instance of the ImageFactory</param>
        /// <param name="GenreFactory">Instance of the GenreFactory</param>
        /// <param name="PlaylistFactory">Instance of the PlaylistFactory</param>
        /// <param name="WikiFactory">Instance of the WikiFactory</param>
        public PersistenceRepository(IUnitOfWork UnitOfWork,
            AbstractFactory<Artist> ArtistFactory,
            AbstractFactory<Album> AlbumFactory,
            AbstractFactory<Track> TrackFactory,
            AbstractFactory<Image> ImageFactory,
            AbstractFactory<Genre> GenreFactory,
            AbstractFactory<PlayList> PlaylistFactory,
            AbstractFactory<Wiki> WikiFactory            
            )
        {
            if (UnitOfWork == null)
                throw new ArgumentNullException("UnitOfWork", "No valid Unit of Work supplied");
            _unitOfWork = UnitOfWork;
            if (ArtistFactory == null)
                throw new ArgumentNullException("ArtistFactory", "No valid Factory supplied to create Artist class");
            _artistFactory = ArtistFactory;
            if (AlbumFactory == null)
                throw new ArgumentNullException("AlbumFactory", "No valid Factory supplied to create Album class");
            _albumFactory = AlbumFactory;
            if (TrackFactory == null)
                throw new ArgumentNullException("TrackFactory", "No valid Factory supplied to create Track class");
            _trackFactory = TrackFactory;
            if (ImageFactory == null)
                throw new ArgumentNullException("ImageFactory", "No valid Factory supplied to create Image class");
            _imageFactory = ImageFactory;
            if (GenreFactory == null)
                throw new ArgumentNullException("GenreFactory", "No valid Factory supplied to create Genre class");
            _genreFactory = GenreFactory;
            if (PlaylistFactory == null)
                throw new ArgumentNullException("PlaylistFactory", "No valid Factory supplied to create Playlist class");
            _playlistFactory = PlaylistFactory;
            if (WikiFactory == null)
                throw new ArgumentNullException("WikiFactory", "No valid Factory supplied to create Wiki class");
            _wikiFactory = WikiFactory;

            //  Create an instance of the Navigation Properties Builder, for the Load
            _navigationProperyBuilder = new BuildNavigationProperties();

        }

        /// <summary>
        /// Retrieves the data from the underlying file, in an asynchronous way, 
        /// rebuilds the navigation properties and loads the related collections.
        /// If no data is actually loaded, then it initialises with some static data.
        /// </summary>
        /// <returns>An instance of Task, so the method is awaitable</returns>
        public async Task LoadAsync()
        {
            //  Get the XML file if it exists.
            var fileHandle = await Helpers.DataFileHelper.GetDataFile();

            //  If the file exists, load it from the XML file.
            if (fileHandle != null)
            {
                //  Store the handle to the search data file for use when the data is to be persisted
                //  during the application close
                _unitOfWork.PersistenceFile = fileHandle;
                //  deserialise the Data
                await SerialiseHelper.Deserialise(_unitOfWork, fileHandle);
                //  Now Rebuild the navigation indexes and load related collections.
                await _navigationProperyBuilder.Build(_unitOfWork);

            }
            else
            {
                //  Create an empty file and store the handle to the file.
                //  It will be needed during the application shutsdown so the 
                //  the data can be persisted in the XML file.
                _unitOfWork.PersistenceFile = await Helpers.DataFileHelper.CreateDataFile();
            }
            //  Now check the datamodel to see if anything has been loaded, and call 
            //  the initialiseMusicCollection if there's no data.
            if (_unitOfWork.Albums.Count == 0)
                InitialiseMusicCollection.SeedData(_unitOfWork, _artistFactory, _albumFactory, _trackFactory, _imageFactory, _genreFactory, _playlistFactory, _wikiFactory);
        }

        /// <summary>
        /// Persists the data in the underlying file, in an asynchronous way.
        /// </summary>
        /// <returns>An instance of Task, so the method is awaitable</returns>
        public async Task SaveAsync()
        {
            //  get the handle to the XML data file.
            var dataFile = _unitOfWork.PersistenceFile;
            //  Serialise the data.
            await SerialiseHelper.Serialise(_unitOfWork, dataFile);
        }
    }
}
