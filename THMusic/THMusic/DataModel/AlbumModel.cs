//***************************************************************************************************
//Name of File:     AlbumModel.cs
//Description:      The AlbumModel, that supports the view model of the SplitPage.
//Author:           Tim Harrison
//Date of Creation: Apr/May 2013.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************

using System;
using System.Collections.Generic;

using GalaSoft.MvvmLight;
using Windows.UI.Xaml.Media;

using THMusic.Helpers;

namespace THMusic.DataModel
{
    /// <summary>
    /// This <c>AlbumModel</c> is used to support the AlbumsViewNodel
    /// </summary>
    public class AlbumModel : ObservableObject
    {
        private string _id { get; set; }

        private string _imagePathLarge;
        private string _imagePathMedium;
        private ImageSource _imageLarge;
        private ImageSource _imageMedium;
        private string _title;

        private DateTime _released;

        private string _url;
        private string _mbid;

        private string _artistName;
        private string _artistMbid;
        private string _artistUrl;

        private WikiModel _wiki;

        private List<TrackModel> _tracks;
        private List<GenreModel> _genres;

        /// <summary>
        /// ctor: Initialises the AlbumModel.
        /// </summary>
        public AlbumModel()
        {
        }


        /// <summary>
        /// Gets or sets the id of the album.
        /// </summary>
        public string Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    RaisePropertyChanged(() => Id);
                }
            }
        }

        /// <summary>
        /// This <c>ImageSourceLarge</c> property provides the image for the
        /// group.  It loads the 1st Album cover image found for the group
        /// and is loaded asynchronously
        /// </summary>
        public ImageSource ImageLarge
        {
            get 
            {
                if (_imageLarge == null && _imagePathLarge != null)
                {
                    //  Call the LoadImageAsync routine, to load the
                    //  image as a bitmap from the source file help
                    //  in the _imagePath prvate property.
                    LoadImageLargeAsync();
                    RaisePropertyChanged(() => ImageLarge);
                }
                return _imageLarge;
            }
            set
            {
                _imagePathLarge = null;
                _imageLarge = value;
                RaisePropertyChanged(() => ImageLarge);
            }
        }

        /// <summary>
        /// Method used to set the path for the impage of the group
        /// </summary>
        /// <param name="path">The string containing the full Path to the image file</param>
        public void SetImageLarge(String path)
        {
            this._imageLarge = null;
            this._imagePathLarge = path;
            RaisePropertyChanged(() => ImageLarge);
        }

        /// <summary>
        /// Gets the path for the Large Image
        /// </summary>
        public string ImagePathLarge
        {
            get { return _imagePathLarge; }
        }

        /// <summary>
        /// Asynchronously loads the image pointed to by the _imagePath property
        /// </summary>
        private async void LoadImageLargeAsync()
        {
            //  Load the file as a bitMapImage.
            _imageLarge = await ImageLoader.LoadImageAsync(_imagePathLarge);
        }


        /// <summary>
        /// This <c>ImageSourceMedium</c> property provides the image for the
        /// group.  It loads the 1st Album cover image found for the group
        /// and is loaded asynchronously
        /// </summary>
        public ImageSource ImageMedium
        {
            get
            {
                if (_imageMedium == null && _imagePathMedium != null)
                {
                    LoadImageMediumAsync();
                    RaisePropertyChanged(() => ImageMedium);
                }
                return _imageMedium;
            }
            set
            {
                _imagePathMedium = null;
                _imageMedium = value;
                RaisePropertyChanged(() => ImageMedium);
            }
        }

        /// <summary>
        /// Method used to set the path for the Medium image of the album
        /// </summary>
        /// <param name="path">The string containing the full Path to the image file</param>
        public void SetImageMedium(String path)
        {
            this._imageMedium = null;
            this._imagePathMedium = path;
            RaisePropertyChanged(() => ImageMedium);
        }

        /// <summary>
        /// Gets the path for the meduim Image
        /// </summary>
        public string ImagePathMedium
        {
            get { return _imagePathMedium; }
        }

        private async void LoadImageMediumAsync()
        {
            _imageMedium = await ImageLoader.LoadImageAsync(_imagePathMedium);
        }


        /// <summary>
        /// Gets or sets the Title of the Album
        /// </summary>
        public string Title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    RaisePropertyChanged(() => Title);
                }
            }
        }

        /// <summary>
        /// Gets the localised released date of the Album.
        /// </summary>
        public string Released
        {
            get { return Helpers.LocalisationHelper.LocalisedDate(_released);}
        }
        /// <summary>
        /// Gets or sets the DateTime representation of the release date of an album
        /// </summary>
        public DateTime RawReleased
        {
            get { return _released; }
            set
            {
                if (_released != value && value != null)
                {
                    _released = value;
                    RaisePropertyChanged(() => Released);
                }
            }
        }


        /// <summary>
        /// Gets or sets the name of the Artist of the Album
        /// </summary>
        public string ArtistName
        {
            get { return _artistName; }
            set
            {
                if (_artistName != value)
                {
                    _artistName = value;
                    RaisePropertyChanged(() => ArtistName);
                }
            }
        }

        /// <summary>
        /// Gets or sets the Artist  LastFM mbid
        /// </summary>
        public string ArtistMbid
        {
            get { return _artistMbid; }
            set
            {
                if (_artistMbid != value)
                {
                    _artistMbid = value;
                    RaisePropertyChanged(() => ArtistMbid);
                }
            }
        }

        /// <summary>
        /// Gets of sets the LastFM Url for the Artist.
        /// </summary>
        public string ArtistUrl
        {
            get { return _artistUrl; }
            set
            {
                if (_artistUrl != value)
                {
                    _artistUrl = value;
                    RaisePropertyChanged(() => ArtistUrl);
                }
            }
        }


        /// <summary>
        /// Gets or sets the LastFM url for information about the album.
        /// </summary>
        public string LastFMUrl
        {
            get { return _url; }
            set
            {
                if (_url != value)
                {
                    _url = value;
                    RaisePropertyChanged(() => LastFMUrl);
                }
            }
        }

        /// <summary>
        /// Gets or sets the LastFM Mbid.
        /// </summary>
        public string LastFMMbid
        {
            get { return _mbid; }
            set
            {
                if (_mbid != value)
                {
                    _mbid = value;
                    RaisePropertyChanged(() => LastFMMbid);
                }
            }
        }

        /// <summary>
        /// Gets or sets the Wiki information for the Album
        /// </summary>
        public WikiModel Wiki
        {
            get { return _wiki; }
            set
            {
                if (_wiki != value)
                {
                    _wiki = value;
                    RaisePropertyChanged(() => Wiki);
                }
            }
        }

        /// <summary>
        /// Gets of sets the collection of Tracks belonging to the Album
        /// </summary>
        public List<TrackModel> Tracks
        {
            get { return _tracks; }
            set
            {
                if (_tracks != value)
                {
                    _tracks = value;
                    RaisePropertyChanged(() => Tracks);
                }
            }
        }

        private TrackModel _selectedTrack;
        public TrackModel SelectedTrack
        {
            get 
            {
                return _selectedTrack; 
            }
            set
            {
                if (_selectedTrack != value && value != null)
                {
                    _selectedTrack = value;
                    RaisePropertyChanged(() => SelectedTrack);
                }
            }
        }



        /// <summary>
        /// Gets or sets the collection of genres the album is tagged with
        /// </summary>
        public List<GenreModel> Genres
        {
            get { return _genres; }
            set
            {
                if (_genres != value)
                {
                    _genres = value;
                    RaisePropertyChanged(() => Genres);
                }
            }
        }

    }
}
