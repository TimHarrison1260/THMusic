//***************************************************************************************************
//Name of File:     WikiModel.cs
//Description:      The WikiModel, that supports the view model of the SplitPage page.
//Author:           Tim Harrison
//Date of Creation: Apr/May 2013.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************

using System;
using GalaSoft.MvvmLight;

namespace THMusic.DataModel
{
    /// <summary>
    /// This <c>WikiModel</c> is used to support the AlbumsViewModel
    /// </summary>
    public class WikiModel : ObservableObject
    {
        private string _summary;
        private string _content;
        private DateTime _published;

        /// <summary>
        /// ctor: Initialises the WikiModel.
        /// </summary>
        public WikiModel()
        {
        }

        /// <summary>
        /// Gets or sets the Summary of the Wiki.
        /// </summary>
        public string Summary
        {
            get { return _summary; }
            set
            {
                if (_summary != value)
                {
                    _summary = value;
                    RaisePropertyChanged(() => Summary);
                }
            }
        }

        /// <summary>
        /// Gets or sets the Content of the Wiki, the details
        /// </summary>
        public string Content
        {
            get { return _content; }
            set
            {
                if (_content != value)
                {
                    _content = value;
                    RaisePropertyChanged(() => Content);
                }
            }
        }

        /// <summary>
        /// Gets the localised Date the Wiki was published
        /// </summary>  
        public string Published
        {
            get { return Helpers.LocalisationHelper.LocalisedDate(_published); }
        }
        /// <summary>
        /// Gets or sets the Datetime representation of the Wiki publsihed date
        /// </summary>
        public DateTime RawPublished
        {
            get { return _published;}
            set
            {
                if (_published != value)
                {
                    _published = value;
                    RaisePropertyChanged(() => Published);
                }
            }
        }

    }
}
