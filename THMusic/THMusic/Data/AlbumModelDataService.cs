//***************************************************************************************************
//Name of File:     AlbumModelDataService.cs
//Description:      The AlbumModelDataService provides loading and mapping functionality for the AlbumsViewModel.
//Author:           Tim Harrison
//Date of Creation: Apr/May 2013.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

using Windows.ApplicationModel.Resources;
using Windows.Globalization.DateTimeFormatting;

using THMusic.DataModel;
using Core.Interfaces;
using Core.Model;

namespace THMusic.Data
{
    /// <summary>
    /// This <c>AlbumModelHelper</c> class is responsible for managing all
    /// access to data within the Domain model, and mapping it to the
    /// AlbumModel of the Ui, which supports the AlbumsViewModel.
    /// The AlbumsViewModel is part of the MVVM pattern implemented within
    /// the UI, and supported by the MVMLight framework.
    /// </summary>
    public class AlbumModelDataService : IAlbumModelDataService
    {
        //  Injected instances of the various repositories (Domain Model)
        private readonly IAlbumRepository _albumRepository;
        private readonly IArtistRepository _artistRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IPlaylistRepository _playlistRepository;

         /// <summary>
        /// Provides access to the resouce files, Localisation, used
        /// when describing the Group Totals.
        /// </summary>
        private static ResourceLoader _loader = new ResourceLoader();

        /// <summary>
        /// ctor: Initialise the AlbumModelDataService
        /// </summary>
        /// <param name="AlbumRepository">Instance of the AlbumRepository</param>
        /// <param name="ArtistRepository">Instance of the ArtistRepository</param>
        /// <param name="GenreRepository">Instance of the GenreRepository</param>
        /// <param name="PlaylistRepository">Instance of the PlaylistRepository</param>
        public AlbumModelDataService(IAlbumRepository AlbumRepository, IArtistRepository ArtistRepository, IGenreRepository GenreRepository, IPlaylistRepository PlaylistRepository)
        {
            if (AlbumRepository == null)
                throw new ArgumentNullException("AlbumRepository", "No valid Repository supplied");
            _albumRepository = AlbumRepository;
            if (ArtistRepository == null)
                throw new ArgumentNullException("ArtistRepository", "No valid Repository supplied");
            _artistRepository = ArtistRepository;
            if (GenreRepository == null)
                throw new ArgumentNullException("GenreRepository", "No valid Repository supplied");
            _genreRepository = GenreRepository;
            if (PlaylistRepository == null)
                throw new ArgumentNullException("PlaylistRepository", "No valid Repository supplied");
            _playlistRepository = PlaylistRepository;
        }


        /// <summary>
        /// Gets the Name of the Group, from the Unique Id, 
        /// which is Group Type and the Id of the grouping
        /// </summary>
        /// <param name="Id">The Id of the Group</param>
        /// <param name="Type">The Type of the Group</param>
        /// <returns>Returns the Group name, Artist, Genre or Playlist</returns>
        public async Task<string> LoadGroupNameAsync(int Id, GroupTypeEnum Type)
        {
            string name = string.Empty;
            switch (Type)
            {
                case GroupTypeEnum.Artist:
                    var artist = await _artistRepository.GetById(Id);
                    name = artist.Name;
                    break;
                case GroupTypeEnum.Genre:
                    var Genre = await _genreRepository.GetById(Id);
                    name = Genre.Name;
                    break;
                case GroupTypeEnum.Playlist:
                    var Playlist = await _playlistRepository.GetById(Id);
                    name = Playlist.Name;
                    break;
            }
            return name;
        }


        /// <summary>
        /// Helper method to load the GroupModel that supports the MainViewModel
        /// with the corresponding group category.
        /// </summary>
        /// <param name="Id">The Id of the Group</param>
        /// <param name="Type">The Type of the Group</param>
        /// <returns>A collection of the AlbumModel used in the page.</returns>
        public async Task<List<AlbumModel>> LoadAlbumsAsync(int Id, GroupTypeEnum Type)
        {
            var albumsViewModel = new List<AlbumModel>();

            //  Call the repository 
            //  Check, this should include the albums, there is no Lazy loading strategy for this.
            IEnumerable<Album> albums = new List<Album>();
            switch (Type)
            {
                case GroupTypeEnum.Artist:
                    albums = await _albumRepository.GetAlbumsForArtist(Id);
                    break;
                case GroupTypeEnum.Genre:
                    albums = await _albumRepository.GetAlbumsForGenre(Id);
                    break;
                case GroupTypeEnum.Playlist:
                    albums = await _albumRepository.GetAlbumsForPlaylist(Id);
                    break;
            }
            //  Map the albums to the albumsViewModel
            albumsViewModel = MapAlbumsToAlbumModels(albums);
            return albumsViewModel;
        }

        /// <summary>
        /// Maps the Album returned from the Domain Model, to the AlbumModel of the UI
        /// </summary>
        /// <param name="albums">Domain Albums collection</param>
        /// <returns>AlbumModel collection</returns>
        private List<AlbumModel> MapAlbumsToAlbumModels(IEnumerable<Album> albums)
        {
            //  The viewmodel collection
            var albumsViewModel = new List<AlbumModel>();

            //  Map the albums to the AlbumViewModel
            foreach (var a in albums)
            {
                var mappedAlbum = new AlbumModel();     //OK to New, UI model only, no injection needed.
                //  Map the album specific data
                mappedAlbum.Id = a.Id.ToString();       //  No localisation needed here, it's won't be actually displayed.
                mappedAlbum.Title = a.Title;
                mappedAlbum .RawReleased = a.Released;  // Update the DateFormate property.

                mappedAlbum.ArtistName = a.Artist.Name;

                //  Load the large and medium images only.
                var largeImage = a.Images.FirstOrDefault(i => i.Size == ImageSizeEnum.large);
                if (largeImage != null)
                    mappedAlbum.SetImageLarge(largeImage.Url);
                else
                    mappedAlbum.SetImageLarge(@"Assets\MediumGray.png");

                var mediumImage = a.Images.FirstOrDefault(i => i.Size == ImageSizeEnum.medium);
                if (mediumImage != null)
                    mappedAlbum.SetImageMedium(mediumImage.Url);
                else
                    mappedAlbum.SetImageMedium(@"Assets\LightGray.png");

                //  Map the LastFM stuff.
                mappedAlbum.LastFMUrl = a.Url;
                mappedAlbum.LastFMMbid = a.Mbid;

                //  Map the Wiki stuff
                mappedAlbum.Wiki = new WikiModel();
                mappedAlbum.Wiki.Summary = a.Wiki.Summary;
                mappedAlbum.Wiki.Content = a.Wiki.Content;

                mappedAlbum.Wiki.RawPublished = a.Wiki.Published;   // Set the DateTime property

                //  Map the Genres.
                mappedAlbum.Genres = new List<GenreModel>();
                foreach (var g in a.Genres)
                {
                    var mappedGenre = new GenreModel()
                    {
                        Id = g.Id.ToString(),
                        Name = g.Name,
                        LastFMUrl = g.Url
                    };
                    mappedAlbum.Genres.Add(mappedGenre);
                }

                //  Map the tracks
                mappedAlbum.Tracks = new List<TrackModel>();
                foreach (var tr in a.Tracks)
                {
                    var mappedTrack = new TrackModel()
                    {
                        Id = tr.Id.ToString(),
                        Number = tr.Number.ToString(),     //  No localisation here, there are unlikely to be more than 99 tracks.
                        Title = tr.Title,                        
                        RawDuration = tr.Duration,  // set the TimeSpan property, not the localised display property
                        LastFMMbid = tr.Mbid,
                        LastFMUrl = tr.Url,
                        MediaFilePath = tr.mediaFilePath
                    };
                    mappedAlbum.Tracks.Add(mappedTrack);
                }

                //  Add to the ViewModel
                albumsViewModel.Add(mappedAlbum);
            }
            return albumsViewModel;
        }

    }
}
