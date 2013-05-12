//***************************************************************************************************
//Name of File:     ILastFMService.cs
//Description:      The ILastFMService interface abstracts the LastFM functionality from the rest of the app.
//Author:           Tim Harrison
//Date of Creation: Apr/May 2013.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace Core.Services
{
    /// <summary>
    /// this <c>ILastFMService</c> interface describes the contract for the 
    /// <see cref="Infrastructure.Services.LastFMProxy"/>
    /// </summary>
    public interface ILastFMService
    {
        /// <summary>
        /// Gets the album information by calling the LastFM Web Service
        /// </summary>
        /// <param name="albumName">The Name of the album to find</param>
        /// <param name="artistName">The name of the Artist to find</param>
        /// <returns>An instance of the LastFMAlbumInfo class</returns>
        Task<LastFMAlbumInfo> GetAlbumInfoAsync(string albumName, string artistName);
    }
}
