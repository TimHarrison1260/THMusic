//***************************************************************************************************
//Name of File:     GenreModel.cs
//Description:      The GenreModel, that supports the view model of the SplitPage page.
//Author:           Tim Harrison
//Date of Creation: Apr/May 2013.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************

using GalaSoft.MvvmLight;

namespace THMusic.DataModel
{
    /// <summary>
    /// This <c>GenreModel</c> is used to support the AlbumModel
    /// as it contains a collection of Genres.
    /// </summary>
    public class GenreModel : ObservableObject
    {
        private string _id;
        private string _name;
        private string _url;

        /// <summary>
        /// ctor: Initialises the GroupModel.
        /// </summary>
        public GenreModel()
        {
        }

        /// <summary>
        /// Gets or sets the unique id of the Genre
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
        /// Gets or sets the Name of the Genre
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
        /// Gets or sets the LastFM url for Genre Information.
        /// </summary>
        public string LastFMUrl
        {
            get { return _url;}
            set
            {
                if (_url != value)
                {
                    _url = value;
                    RaisePropertyChanged(() => LastFMUrl);
                }
            }
        }

    }
}
