//***************************************************************************************************
//Name of File:     ILastFMModelDataService.cs
//Description:      The ILastFMModelDataService provides the interface to the LastFMModelDataService.
//Author:           Tim Harrison
//Date of Creation: Apr/May 2013.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************
using System.Threading.Tasks;
using THMusic.DataModel;

namespace THMusic.Data
{
    /// <summary>
    /// This <c>ILastFMModelDataService</c> interface describes the functionality
    /// of the <see cref="THMusic.Data.LastFMModelDataService"/>, which is responsible 
    /// for managing all access to LastFMService, and mapping the results to the
    /// AlbumModel of the Ui, which supports the LastFMViewModel as well as
    /// the AlbumsViewModel.  
    /// The LastFMViewModel is part of the MVVM pattern implemented within
    /// the UI, and supported by the MVMLight framework.
    /// </summary>
    public interface ILastFMModelDataService
    {
        /// <summary>
        /// Gets the album information from the LastFM album.getInfo service
        /// </summary>
        /// <param name="ArtistName">The Artist to search for</param>
        /// <param name="AlbumName">The Album to search for</param>
        /// <returns>The Album information as an AlbumModel class.</returns>
        Task<AlbumModel> GetLastFMAlbumInfoAsync(string ArtistName, string AlbumName);

        /// <summary>
        /// Adds the Album to the Domain Model.
        /// </summary>
        /// <param name="lastFMAlbum">The album to be added</param>
        /// <returns>A success or failure indicator</returns>
        Task<string> ImportAlbumAsync(AlbumModel lastFMAlbum);
    }
}
