//***************************************************************************************************
//Name of File:     LastFMModelDataService.cs
//Description:      The LastFMModelDataService provides loading and mapping functionality for the LastFMViewModel.
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
using Core.Factories;
using Core.Services;

namespace THMusic.Data
{
    /// <summary>
    /// This <c>LastFMModelDataService</c> class is responsible for managing all
    /// access to LastFMService, and mapping the results to the
    /// AlbumModel of the Ui, which supports the LastFMViewModel as well as
    /// the AlbumsViewModel.  
    /// The LastFMViewModel is part of the MVVM pattern implemented within
    /// the UI, and supported by the MVMLight framework.
    /// </summary>
    public class LastFMModelDataService : ILastFMModelDataService
    {
         /// <summary>
        /// Provides access to the resouce files, Localisation, used
        /// when describing the Group Totals.
        /// </summary>
        private  ResourceLoader _loader = new ResourceLoader();

        private ILastFMService _lastFMService;
        private IAlbumRepository _albumRepository;
        private AbstractFactory<Album> _albumFactory;
        private AbstractFactory<Image> _imageFactory;
        private AbstractFactory<Track> _trackFactory;
        private AbstractFactory<Artist> _artistFactory;
        private AbstractFactory<Genre> _genreFactory;
        private AbstractFactory<Wiki> _wikiFactory;

        /// <summary>
        /// ctor: Accepts the injected instance of the dependencies
        /// </summary>
        /// <param name="AlbumRepository">The injected AlbumRepository instance</param>
        /// <param name="LastFMService">The injected LastFMService instance</param>
        public LastFMModelDataService(IAlbumRepository AlbumRepository, 
            AbstractFactory<Album> AlbumFactory, 
            AbstractFactory<Image> ImageFactory,
            AbstractFactory<Track> TrackFactory,
            AbstractFactory<Artist> ArtistFactory,
            AbstractFactory<Genre> GenreFactory,
            AbstractFactory<Wiki> WikiFactory,
            ILastFMService LastFMService)
        {
            if (AlbumRepository == null)
                throw new ArgumentNullException("AlbumRepository", "No valid Album Repository supplied");
            _albumRepository = AlbumRepository;
            if (AlbumFactory == null)
                throw new ArgumentNullException("AlbumFactory", "No valid Album factory supplied");
            _albumFactory = AlbumFactory;
            if (ImageFactory == null)
                throw new ArgumentNullException("ImageFactory", "No valid Image factory supplied");
            _imageFactory = ImageFactory;
            if (TrackFactory == null)
                throw new ArgumentNullException("TrackFactory", "No valid Track factory supplied");
            _trackFactory = TrackFactory;
            if (ImageFactory == null)
                throw new ArgumentNullException("ArtistFactory", "No valid Artist factory supplied");
            _artistFactory = ArtistFactory;
            if (ImageFactory == null)
                throw new ArgumentNullException("GenreFactory", "No valid Genre factory supplied");
            _genreFactory = GenreFactory;
            if (WikiFactory == null)
                throw new ArgumentNullException("WikiFactory", "No valid Wiki factory supplied");
            _wikiFactory = WikiFactory;
            if (LastFMService == null)
                throw new ArgumentNullException("LastFMService", "No valid Service for LastFM supplied");
            _lastFMService = LastFMService;
        }

        /// <summary>
        /// Helper method to load the GroupModel that supports the MainViewModel
        /// with the corresponding group category.
        /// </summary>
        /// <returns></returns>
        public  async Task<AlbumModel> GetLastFMAlbumInfoAsync(string ArtistName, string AlbumName)  //, ILastFMService lastFMService)
        {
            //  Call the LastFMService 
            //  Check, this will include the returned information for the Artist/Album combination.
            //  It won't necessarily have all the information in it.  Search again is the way round 
            //  that.
            var lfmAlbum = await _lastFMService.GetAlbumInfoAsync(AlbumName, ArtistName);

            //  Map the album to the LastFMViewModel
            var mappedAlbum = new AlbumModel();     //OK to New, UI model only, no injection needed.

            //  Map the album specific data
            mappedAlbum.Id = string.Empty;     //  No value for this yet.
            mappedAlbum.Title = lfmAlbum.name;

            //  Release date
            DateTime lfmDate = new DateTime();
            DateTime.TryParse(lfmAlbum.releasedDate, out lfmDate);

            mappedAlbum.RawReleased = lfmDate;
//            mappedAlbum.Released = LocalisationHelper.LocalisedDate(lfmDate);

            //  Artist stuff
            mappedAlbum.ArtistName = lfmAlbum.artist.name;
            mappedAlbum.ArtistUrl = lfmAlbum.artist.url;
            mappedAlbum.ArtistMbid = lfmAlbum.artist.mbid;

            //  Load the large and medium images only.
            var largeImage = lfmAlbum.images.FirstOrDefault(i => i.size == "large");
            if (largeImage != null)
                mappedAlbum.SetImageLarge(largeImage.imageUrl);
            else
                mappedAlbum.SetImageLarge(@"Assets\MediumGray.png");

            var mediumImage = lfmAlbum.images.FirstOrDefault(i => i.size == "medium");
            if (mediumImage != null)
                mappedAlbum.SetImageMedium(mediumImage.imageUrl);
            else
                mappedAlbum.SetImageMedium(@"Assets\LightGray.png");

            //  Map the LastFM stuff.
            mappedAlbum.LastFMUrl = lfmAlbum.url;
            mappedAlbum.LastFMMbid = lfmAlbum.mbid;

            //  Map the Wiki stuff
            mappedAlbum.Wiki = new WikiModel();
            mappedAlbum.Wiki.Summary = lfmAlbum.wiki.summary;
            mappedAlbum.Wiki.Content = lfmAlbum.wiki.content;

            DateTime wikiDate = new DateTime();
            DateTime.TryParse(lfmAlbum.wiki.published, out wikiDate);
            mappedAlbum.Wiki.RawPublished = wikiDate;
            //mappedAlbum.Wiki.Published = LocalisationHelper.LocalisedDate(wikiDate);

            //  Map the Genres.
            mappedAlbum.Genres = new List<GenreModel>();
            foreach (var g in lfmAlbum.tags)
            {
                var mappedGenre = new GenreModel()
                {
                    Id = string.Empty,
                    Name = g.name,
                    LastFMUrl = g.url
                };
                mappedAlbum.Genres.Add(mappedGenre);
            }

            //  Map the tracks
            mappedAlbum.Tracks = new List<TrackModel>();
            foreach (var tr in lfmAlbum.tracks)
            {
                var mappedTrack = new TrackModel()
                {
                    Id = string.Empty,
                    Number = tr.rank,   
                    Title = tr.name,
                    LastFMMbid = tr.mbid,
                    LastFMUrl = tr.url
                };

                TimeSpan trackDuration = new TimeSpan();
                int secs = 0;
                if (int.TryParse(tr.duration, out secs))
                {
                    trackDuration = TimeSpan.FromSeconds(secs);
                }
                //mappedTrack.Duration = LocalisationHelper.LocaliseDuration(trackDuration);
                mappedTrack.RawDuration = trackDuration;

                mappedAlbum.Tracks.Add(mappedTrack);
            }

            return mappedAlbum;
        }

        /// <summary>
        /// Taks the LastFMAlbum and calls the albumRepository to create it in the domain
        /// </summary>
        /// <param name="lastFMAlbum">The LastFMAlbum to be created</param>
        /// <returns>The async Task</returns>
        public async Task<string> ImportAlbumAsync(AlbumModel lastFMAlbum)
        {
            //  Map the AlbumModel (UI layer) to the Album (domain).
            var newAlbum = MapAlbumModelToAlbum(lastFMAlbum);

            //  Call the repository to Create the new album in the domain.
            var returnedAlbum = await _albumRepository.CreateAsync(newAlbum);
            return (returnedAlbum != null) ? "AlbumImportedOK" : "AlbumImportFailed";
        }

        /// <summary>
        /// Maps the LastFMAlbum (AlbumModel> class to the Domain album class
        /// </summary>
        /// <param name="lastFMAlbum">The AlbumModel to be mapped</param>
        /// <returns>The mapped Doamin Album</returns>
        private Album MapAlbumModelToAlbum(AlbumModel lastFMAlbum)
        {
            var newAlbum = _albumFactory.Create();

            //  Copy the Album specific stuff
            newAlbum.Id = 0;
            newAlbum.Title = lastFMAlbum.Title;

            newAlbum.Released = lastFMAlbum.RawReleased;

            newAlbum.Mbid = lastFMAlbum.LastFMMbid;
            newAlbum.Url = lastFMAlbum.LastFMUrl;

            //  Copy the Images
            newAlbum.Images = new List<Image>();
            Image mediumImage = _imageFactory.Create();
            mediumImage.Size = ImageSizeEnum.Medium;
            mediumImage.Url = lastFMAlbum.ImagePathMedium;
            newAlbum.Images.Add(mediumImage);
            Image largeImage = _imageFactory.Create();
            largeImage.Size = ImageSizeEnum.Large;
            largeImage.Url = lastFMAlbum.ImagePathLarge;
            newAlbum.Images.Add(largeImage);

            //  Copy the Artist
            newAlbum.Artist = _artistFactory.Create();
            newAlbum.Artist.Name = lastFMAlbum.ArtistName;
            newAlbum.Artist.Mbid = lastFMAlbum.ArtistMbid;
            newAlbum.Artist.Url = lastFMAlbum.ArtistUrl;

            //  Copy the Tracks
            newAlbum.Tracks = new List<Track>();
            foreach (var tr in lastFMAlbum.Tracks)
            {
                Track newtrack = _trackFactory.Create();
                newtrack.Title = tr.Title;

                int no;
                if (int.TryParse(tr.Number, out no))
                    newtrack.Number = no;
                newtrack.Mbid = tr.LastFMMbid;
                newtrack.Url = tr.LastFMUrl;

                newtrack.Duration = tr.RawDuration;

                newtrack.Artist = newAlbum.Artist;

                newtrack.Playlists = new List<PlayList>();
                newAlbum.Tracks.Add(newtrack);
            }

            //  Copy the Genres
            newAlbum.Genres = new List<Genre>();
            foreach (var genre in lastFMAlbum.Genres)
            {
                Genre newGenre = _genreFactory.Create();
                newGenre.Name = genre.Name;
                newGenre.Url = genre.LastFMUrl;
                newAlbum.Genres.Add(newGenre);
            }


            //  Copy the Wiki's
            newAlbum.Wiki = _wikiFactory.Create();
            newAlbum.Wiki.Summary = lastFMAlbum.Wiki.Summary;
            newAlbum.Wiki.Content = lastFMAlbum.Wiki.Content;

            newAlbum.Wiki.Published = lastFMAlbum.Wiki.RawPublished;

            return newAlbum;
        }
    }
}
