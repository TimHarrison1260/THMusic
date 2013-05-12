//***************************************************************************************************
//Name of File:     GroupModel.cs
//Description:      The GroupModel, that supports the view model of the Items page.
//Author:           Tim Harrison
//Date of Creation: Apr/May 2013.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************

using System;

using GalaSoft.MvvmLight;
using Windows.UI.Xaml.Media;

using THMusic.Helpers;

namespace THMusic.DataModel
{
    /// <summary>
    /// This <c>GroupModel</c> is used to support the MainViewNodel
    /// as this can be sourced by 4 different Group categories:
    /// Artist, Genre, Playlist and Album.  Therefore the ViewModel
    /// must be able to swap with all the field names matching.
    /// </summary>
    public class GroupModel : ObservableObject
    {
        private GroupId _uniqueId;
        private string _imagePath;
        private ImageSource _image;
        private string _name;
        private string _description;

        /// <summary>
        /// ctor: Initialises the GroupModel.
        /// </summary>
        public GroupModel()
        {
        }


        /// <summary>
        /// Gets or sets the Name of the Group
        /// </summary>
        public GroupId UniqueId
        {
            get { return _uniqueId; }
            set
            {
                if (_uniqueId != value)
                {
                    _uniqueId = value;
                    RaisePropertyChanged(() => UniqueId);
                }
            }
        }

        /// <summary>
        /// This <c>ImageSource</c> property provides the image for the
        /// group.  It loads the 1st Album cover image found for the group
        /// and is loaded asynchronously
        /// </summary>
        public ImageSource Image
        {
            get 
            {
                if (_image == null && _imagePath != null)
                {
                    //  Call the LoadImageAsync routine, to load the
                    //  image as a bitmap from the source file help
                    //  in the _imagePath prvate property.
                    LoadImageAsync();
                }
                return _image;
            }
            set
            {
                _imagePath = null;
                _image = value;
                RaisePropertyChanged(() => Image);
            }
        }

        /// <summary>
        /// Method used to set the path for the impage of the group
        /// </summary>
        /// <param name="path">The string containing the full Path to the image file</param>
        public void SetImage(String path)
        {
            this._image = null;
            this._imagePath = path;
            RaisePropertyChanged(() => Image);
        }

        /// <summary>
        /// Asynchronously loads the image pointed to by the _imagePath property
        /// </summary>
        private async void LoadImageAsync()
        {
            //  Load the file as a bitMapImage.
            _image = await ImageLoader.LoadImageAsync(_imagePath);
            //  Tell all the property has changed
            RaisePropertyChanged(() => Image);                                  
        }


        /// <summary>
        /// Gets or sets the Name of the Group
        /// </summary>
        public string Name 
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    RaisePropertyChanged(() => Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the description for the group.
        /// </summary>
        public string Description
        {
            get { return _description;}
            set
            {
                if (_description != value)
                {
                    _description = value;
                    RaisePropertyChanged(() => Description);
                }
            }
        }

    }

    /// <summary>
    /// This <c>GroupId</c> class defines the UniqueId of a particular Group
    /// The Group can be Artist, Genre or Playlist.  It consists of the Id
    /// of the Group, and the <see cref="THMusic.DataModel.GroupTypeEnum"/>
    /// which specifies the type of the Group.
    /// </summary>
    public class GroupId
    {
        /// <summary>
        /// Gets or sets the type of the Group, Artist, Genre or Playlist
        /// </summary>
        public GroupTypeEnum Type { get; set; }
        /// <summary>
        /// Gets or sets the Id of the Group
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Gets the string representation of the Groupid
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}{1)", this.Type, this.Id.ToString());
        }
    }


}
