﻿//***************************************************************************************************
//Name of File:     TrackModel.cs
//Description:      The TrackModel, that supports the view model of the Items page.
//Author:           Tim Harrison
//Date of Creation: Apr/May 2013.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************

using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Windows.UI.Xaml.Media;

namespace THMusic.DataModel
{
    /// <summary>
    /// This <c>TrackModel</c> is used to support the AlbumsViewNodel
    /// </summary>
    public class TrackModel : ObservableObject
    {
        private string _id;
        private string _number;
        private string _title;
        private TimeSpan _duration;
        private string _url;
        private string _mbid;
        private string _mediaFilePath;

        /// <summary>
        /// ctor: Initialises the GroupModel.
        /// </summary>
        public TrackModel()
        {
            PlayTrackCommand = new RelayCommand(PlayTrackHandler);
        }

        public RelayCommand PlayTrackCommand { get; set; }
        private void PlayTrackHandler()
        {
            //  This acts as a toggle, showing or hiding the MusicPlayer control
            if (_playMe)
                _playMe = false;
            else
                _playMe = true;
            RaisePropertyChanged(() => PlayMe);
        }


        /// <summary>
        /// Gets or sets the Name of the Group
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
        /// Gets or sets the Number of the track on the Album
        /// </summary> 
        public string Number
        {
            get { return _number;}
            set
            {
                if (_number != value)
                {
                    _number = value;
                    RaisePropertyChanged(() => Number);
                }
            }
        }

        /// <summary>
        /// Gets or sets the Title of the track
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
        /// Gets the localised duration of the Track.
        /// </summary> 
        public string Duration
        {
            get { return Helpers.LocalisationHelper.LocaliseDuration(_duration); }
        }
        /// <summary>
        /// Gets or sets the TimeSpan representation of the Track Duration
        /// </summary>
        public TimeSpan RawDuration
        {
            get { return _duration; }
            set
            {
                if (_duration != value)
                {
                    _duration = value;
                    RaisePropertyChanged(() => Duration);
                }
            }
        }

        /// <summary>
        /// Gets or sets the LastFM Url for the Track information.
        /// 
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
        /// Gets or sets the LastFM Mbid for the track.
        /// </summary>
        public string LastFMMbid
        {
            get { return _mbid; }
            set
            {
                if (_mbid != value && value != null)
                {
                    _mbid = value;
                    RaisePropertyChanged(() => LastFMMbid);
                }
            }
        }

        public string MediaFilePath
        {
            get 
            {
                return _mediaFilePath;
            }
            set
            {
                if (_mediaFilePath != value && value != null)
                {
                    _mediaFilePath = value;
                    RaisePropertyChanged(() => MediaFilePath);
                }
            }
        }

        public bool IsPlayable
        {
            get 
            {
                var result = (_mediaFilePath != null && _mediaFilePath != string.Empty) ? true : false;
                return result;
            }
        }

        private bool _playMe = false;
        public bool PlayMe
        {
            get { return _playMe; }
        }

    }
}
