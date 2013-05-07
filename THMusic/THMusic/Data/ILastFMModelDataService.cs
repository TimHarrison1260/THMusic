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
    /// This <c>LastFMModelHelper</c> class is responsible for managing all
    /// access to LastFMService, and mapping the results to the
    /// AlbumModel of the Ui, which supports the LastFMViewModel as well as
    /// the AlbumsViewModel.  
    /// The LastFMViewModel is part of the MVVM pattern implemented within
    /// the UI, and supported by the MVMLight framework.
    /// </summary>
    public interface ILastFMModelDataService
    {
        /// <summary>
        /// Helper method to load the GroupModel that supports the MainViewModel
        /// with the corresponding group category.
        /// </summary>
        /// <returns></returns>
        Task<AlbumModel> GetLastFMAlbumInfoAsync(string ArtistName, string AlbumName);

        /// <summary>
        /// Helper method to create the LastFMalbum as an album in the domain.
        /// </summary>
        /// <param name="lastFMAlbum">the album to create</param>
        /// <returns>The new album</returns>
        Task<string> ImportAlbumAsync(AlbumModel lastFMAlbum);
    }
}
