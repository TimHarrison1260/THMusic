//***************************************************************************************************
//Name of File:     AlbumModelHelper.cs
//Description:      The AlbumModelHelper provides loading and mapping functionality for the AlbumsViewModel.
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

namespace THMusic.Helpers
{
    /// <summary>
    /// This <c>AlbumModelHelper</c> class is responsible for managing all
    /// access to data within the Domain model, and mapping it to the
    /// AlbumModel of the Ui, which supports the AlbumsViewModel.
    /// The AlbumsViewModel is part of the MVVM pattern implemented within
    /// the UI, and supported by the MVMLight framework.
    /// </summary>
    public static class AlbumModelHelper
    {
         /// <summary>
        /// Provides access to the resouce files, Localisation, used
        /// when describing the Group Totals.
        /// </summary>
        private static ResourceLoader _loader = new ResourceLoader();


        /// <summary>
        /// Gets the Name of the Group, from the Unique Id, 
        /// which is Group Type and the Id of the grouping
        /// </summary>
        /// <param name="Id">The Id of the Group</param>
        /// <param name="Type">The Type of the Group</param>
        /// <param name="ArtistRepository">Instance of the ArtistRepository</param>
        /// <returns></returns>
        public static async Task<string> LoadGroupNameAsync(int Id, GroupTypeEnum Type, IArtistRepository ArtistRepository)
        {
            string name = string.Empty;
            switch (Type)
            {
                case GroupTypeEnum.Artist:
                    var artist = await ArtistRepository.GetArtistById(Id);
                    name = artist.Name;
                    break;
                case GroupTypeEnum.Genre:
                    //var Genre = await ArtistRepository.GetArtistById(Id);
                    //name = Genre.Name;
                    break;
                case GroupTypeEnum.Playlist:
                    //var Playlist = await ArtistRepository.GetArtistById(Id);
                    //name = Playlist.Name;
                    break;
            }
            return name;
        }


        /// <summary>
        /// Helper method to load the GroupModel that supports the MainViewModel
        /// with the corresponding group category.
        /// </summary>
        /// <returns></returns>
        public static async Task<List<AlbumModel>> LoadAlbumsAsync(int Id, GroupTypeEnum Type, IAlbumRepository AlbumRepository)
        {
            //  TODO: convert this to accept the Group Type parameter.  Only use Artist for now.
            var albumsViewModel = new List<AlbumModel>();

            //  Call the repository 
            //  Check, this should include the albums, there is no Lazy loading strategy for this.
            var albums = await AlbumRepository.GetAlbumsForArtist(Id);

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
                var largeImage = a.Images.FirstOrDefault(i => i.Size == ImageSizeEnum.Large);
                if (largeImage != null)
                    mappedAlbum.SetImageLarge(largeImage.Url);
                else
                    mappedAlbum.SetImageLarge(@"Assets\MediumGray.png");

                var mediumImage = a.Images.FirstOrDefault(i => i.Size == ImageSizeEnum.Medium);
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
                        LastFMUrl = tr.Url
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
