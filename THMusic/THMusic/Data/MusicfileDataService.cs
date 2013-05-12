//***************************************************************************************************
//Name of File:     MusicFileDataService.cs
//Description:      The MusicFileDataService provides controls access to the MusicFileService.
//Author:           Tim Harrison
//Date of Creation: Apr/May 2013.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

using Core.Services;
using Core.Interfaces;
using Core.Model;
using Core.Factories;
using THMusic.DataModel;
using System.Collections.Concurrent;

namespace THMusic.Data
{
    /// <summary>
    /// This <c>MusicFileDataService</c> class is responsible for managing all access 
    /// to MusicFileService and  mapping the results to the Album and adding the file 
    /// to the Domain model.
    /// It supports the MainViewModel by providing the Import MP3 functionality
    /// The MainViewModel is part of the MVVM pattern implemented within
    /// the UI, and supported by the MVMLight framework.
    /// </summary>
    public class MusicFileDataService : IMusicFileDataService
    {
        private readonly IMusicFileService _musicFileService;
        private readonly ILastFMService _lastFMService;

        private readonly IAlbumRepository _albumRepository;
        private readonly AbstractFactory<Album> _albumFactory;
        private readonly AbstractFactory<Track> _trackFactory;
        private readonly AbstractFactory<Artist> _artistFactory;
        private readonly AbstractFactory<Genre> _genreFactory;

        /// <summary>
        /// Crot:
        /// </summary>
        /// <param name="MusicFileService">Instance of the MusicFileService</param>
        /// <param name="LastFMService">Instance of the LastFMService</param>
        /// <param name="AlbumRepository">Instance of the AlbumRepository</param>
        /// <param name="AlbumFactory">Instance of the AlbumFactory</param>
        /// <param name="TrackFactory">Instance of the TrackFactory</param>
        /// <param name="ArtistFactory">Instance of the ArtistFactory</param>
        /// <param name="GenreFactory">Instance of the GenreFactory</param>
        public MusicFileDataService(IMusicFileService MusicFileService,
                                    ILastFMService LastFMService,
                                    IAlbumRepository AlbumRepository, 
                                    AbstractFactory<Album> AlbumFactory,
                                    AbstractFactory<Track> TrackFactory,
                                    AbstractFactory<Artist> ArtistFactory,
                                    AbstractFactory<Genre> GenreFactory)
        {
            if (MusicFileService == null)
                throw new ArgumentNullException("MusicFileService", "No valid MusicFile service supplied");
            _musicFileService = MusicFileService;
            if (LastFMService == null)
                throw new ArgumentNullException("LastFMService", "No valid LastFM service supplied");
            _lastFMService = LastFMService;
            if (AlbumRepository == null)
                throw new ArgumentNullException("AlbumRepository", "No valid Album Repository supplied");
            _albumRepository = AlbumRepository;
            if (AlbumFactory == null)
                throw new ArgumentNullException("AlbumFactory", "No valid Album Factory supplied");
            _albumFactory = AlbumFactory;
            if (TrackFactory == null)
                throw new ArgumentNullException("TrackFactory", "No valid Track Factory supplied");
            _trackFactory = TrackFactory;
            if (ArtistFactory == null)
                throw new ArgumentNullException("ArtistFactory", "No valid Artist Factory supplied");
            _artistFactory = ArtistFactory;
            if (GenreFactory == null)
                throw new ArgumentNullException("GenreFactory", "No valid Genre Factory supplied");
            _genreFactory = GenreFactory;
        }


        //  Take in the files and the current grouping
        //  Return the list of Id's for the current grouping
        /// <summary>
        /// Takes the selected audio files and processed them by retrieving the 
        /// metadata describing the track,  It calls the LastFMService to retrieve
        /// the image information and then calls the AlbumRepository to add the
        /// track to the album
        /// </summary>
        /// <param name="MusicFiles">The collection of audio files</param>
        /// <param name="Grouping">The grouping type currently showing on the main page</param>
        /// <returns>A list containing the id of the groups affected by the addition of the tracks</returns>
        public async Task<List<int>> ProcessMusicFiles(IReadOnlyList<StorageFile> MusicFiles, GroupTypeEnum Grouping)
        {
            //List<int> affectedGroups = new List<int>();
            var affectedGroups = new List<int>();

            //  Process all track selected.
            foreach (StorageFile musicFile in MusicFiles)
            {
                //  Import the music file and get the album class, it will be null if the music files
                //  has already been added.
                var album = await ImportAlbumAsync(musicFile);
                if (album != null)
                {
                    //  Add the Id of the relevent group to the affected groups
                    //      Ignore Playlists, as no new track would belong to a playlist
                    switch (Grouping)
                    {
                        case GroupTypeEnum.Artist:
                            //  Get the Artist Id, if it's not already been added
                            if (!affectedGroups.Contains(album.Artist.Id))
                                affectedGroups.Add(album.Artist.Id);
                            break;
                        case GroupTypeEnum.Genre:
                            //  Get the Ids of all genres and add them if theyre not already there.
                            foreach (var g in album.Genres)
                            {
                                if (!affectedGroups.Contains(g.Id))
                                    affectedGroups.Add(g.Id);
                            }
                            break;
                    }
                }
            }
            return affectedGroups;
        }


        /// <summary>
        /// Imports the tagged information of the specified file, adding it to the 
        /// Music collection
        /// </summary>
        /// <param name="MusicFile">The music file to be imported</param>
        /// <returns>The id of the Artist the music file was added to.</returns>
        /// <remarks>
        /// The id of the artist is returned, so that the Artist Group summary information
        /// can be refreshed on the UI.  The MusicFileDataService supports the MainViewModel
        /// of the group page (ItemPage) only.
        /// </remarks>
        public async Task<Album> ImportAlbumAsync(StorageFile MusicFile)
        {
            Album returnAlbum = null;
            //  Check the file has not already beein imported, return 0 if it has;
            if (await _albumRepository.IsMediaFileImported(MusicFile.Path))
                return returnAlbum;

            //  Import the file information
            var musicfileInfo = await _musicFileService.GetMusicFileInfoAsync(MusicFile);

            //  Map the information to an Album object of the domain model
            var mappedAlbum = MapMusicFileInfoToAlbum(musicfileInfo, MusicFile.Path);


            //  Add the information to the Domain Model
            var existingAlbum = await _albumRepository.IsAlbumAlreadyImported(mappedAlbum.Artist.Name, mappedAlbum.Title);
            if (existingAlbum != null)
            {
                //  Album already exists, update by just adding the track
                await _albumRepository.AddTrackToAlbum(mappedAlbum);

                returnAlbum = existingAlbum;
            }
            else
            {
                //  It's a completely new album, create it.

                //  Call LastFMService to get the Artwork and Url's, only needed for a new album
                var lastFMInfo = await _lastFMService.GetAlbumInfoAsync(mappedAlbum.Title, mappedAlbum.Artist.Name);

                //  Add the Artwork to the newAlbum
                //  This is a method on the Albums class => Album.UpdateImage, adds or updates the image;
                if (lastFMInfo.images.Count == 0)
                {
                    //  set the default Images
                    var enumValues = Enum.GetNames(typeof(Core.Model.ImageSizeEnum));
                    foreach (var size in enumValues)
                    {
                        mappedAlbum.UpdateImage(size , @"Assets\LightGray.png");
                    }
                }
                else
                {
                    foreach (var lFMImage in lastFMInfo.images)
                    {
                        mappedAlbum.UpdateImage(lFMImage.size, lFMImage.imageUrl);
                    }
                }
                
                Album newAlbum = await _albumRepository.CreateAsync(mappedAlbum);

                returnAlbum = newAlbum;
            }

            //  Return the artist id, so the UI can be refreshed.       NOT THIS, NO GOOD AT ALL.
            return returnAlbum;
        }


        /// <summary>
        /// Maps the MusicFileInfo to an Album model of the Domain.
        /// </summary>
        /// <param name="MusicFile">The imported music file information</param>
        /// <returns>The mapped Album class</returns>
        private Album MapMusicFileInfoToAlbum(MusicFileInfo MusicFile, string filePath)
        {
            var newAlbum = _albumFactory.Create();

            //  Do the mapping here.
            newAlbum.Title = MusicFile.AlbumTitle;
            newAlbum.Released = MusicFile.AlbumReleased;
            newAlbum.Mbid = MusicFile.AlbumMbid;

            var newArtist = _artistFactory.Create();
            newArtist.Name = MusicFile.ArtistName;
            newArtist.Mbid = MusicFile.ArtistMbid;
            newAlbum.Artist = newArtist;

            newAlbum.Tracks = new List<Track>();
            var newTrack = _trackFactory.Create();
            newTrack.Number = MusicFile.TrackNumber;
            newTrack.Title = MusicFile.TrackName;
            newTrack.Duration = MusicFile.TrackDuration;
            newTrack.Mbid = MusicFile.TrackMbid;
            newTrack.Artist = newArtist;
            newTrack.mediaFilePath = filePath;

            newAlbum.Tracks.Add(newTrack);

            foreach (var genre in MusicFile.Genres)
            {
                var newGenre = _genreFactory.Create();
                newGenre.Name = genre;
                newAlbum.Genres.Add(newGenre);
            }

            return newAlbum;
        }


    }
}
