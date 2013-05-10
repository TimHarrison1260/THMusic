//***************************************************************************************************
//Name of File:     ILastFMService.cs
//Description:      The ILastFMService interface abstracts the LastFM functionality from the rest of the app.
//Author:           Tim Harrison
//Date of Creation: Apr/May 2013.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************
using System.Threading.Tasks;           //  Async stuff
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage;                  //  File stuff

namespace Core.Services
{
    /// <summary>
    /// This <c>IMusicFileService</c> describes the <see cref="Infrastructure.Services.MusicFileService"/>
    /// which is responsible for extracting the tagged information about a supplied music file. 
    /// </summary>
    /// <remarks>
    /// It makes use of the Taglib-Sharp tagging framework to access the tagged informtion
    /// on the file.  
    /// </remarks>
    public interface IMusicFileService
    {
        /// <summary>
        /// This <c>GetMusicFileInfoAsync</c>  is responsible for extracting the 
        /// tagged information from the supplied file, then mapping the results
        /// to an instance of the <see cref="Core.Services.MusicFileInfo"/> class.
        /// </summary>
        /// <param name="file">The file to be tagged.  This is an instance of 
        /// a <see cref="Windows.Storage.StorageFile"/> .</param>
        /// <returns>The extracted information as an instance of the MusicFileInfo class.</returns>
        Task<MusicFileInfo> GetMusicFileInfoAsync(StorageFile file);
    }
}
