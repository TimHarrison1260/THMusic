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
    public interface ILastFMService
    {
        Task<LastFMAlbumInfo> GetAlbumInfoAsync(string albumName, string artistName);
    }
}
