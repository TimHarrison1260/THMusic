//***************************************************************************************************
//Name of File:     ILastFMModelHelper.cs
//Description:      The ILastFMModelHelper provides the interface to the LastFMService.
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

namespace THMusic.Helpers
{
    /// <summary>
    /// This <c>LastFMModelHelper</c> class is responsible for managing all
    /// access to LastFMService, and mapping the results to the
    /// AlbumModel of the Ui, which supports the LastFMViewModel as well as
    /// the AlbumsViewModel.  
    /// The LastFMViewModel is part of the MVVM pattern implemented within
    /// the UI, and supported by the MVMLight framework.
    /// </summary>
    public interface ILastFMModelHelper
    {
        /// <summary>
        /// Helper method to load the GroupModel that supports the MainViewModel
        /// with the corresponding group category.
        /// </summary>
        /// <returns></returns>
        Task<AlbumModel> CallLastFMAlbumInfoAsync(string ArtistName, string AlbumName);

        /// <summary>
        /// Helper method to create the LastFMalbum as an album in the domain.
        /// </summary>
        /// <param name="lastFMAlbum">the album to create</param>
        /// <returns>The new album</returns>
        Task<string> ImportAlbumAsync(AlbumModel lastFMAlbum);
    }
}
