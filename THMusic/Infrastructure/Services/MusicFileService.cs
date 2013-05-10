//***************************************************************************************************
//Name of File:     MusicFileService.cs
//Description:      The MusicFileService provides the taglib functionality, abstracting it from the 
//                  resst of the applicatinon.
//Author:           Tim Harrison
//Date of Creation: Apr/May 2013.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************
using System.Threading.Tasks;

using Windows.Storage;          //  Windows store app file api
//  Media file tag extractor
//  File IO stuff
//  File stream stuff

using Core.Services;
using Infrastructure.Helpers;

namespace Infrastructure.Services
{
    /// <summary>
    /// This <c>MusicFileService</c> class is responsible for extracting the tagged
    /// information about a supplied music file. 
    /// </summary>
    /// <remarks>
    /// It makes use of the Taglib-Sharp tagging framework to access the tagged informtion
    /// on the file.  
    /// </remarks>
    public class MusicFileService : IMusicFileService
    {
        /// <summary>
        /// This <c>GetMusicFileInfoAsync</c>  is responsible for extracting the 
        /// tagged information from the supplied file, then mapping the results
        /// to an instance of the <see cref="Core.Services.MusicFileInfo"/> class.
        /// </summary>
        /// <param name="file">The file to be tagged.  This is an instance of 
        /// a <see cref="Windows.Storage.StorageFile"/> .</param>
        /// <returns>The extracted information as an instance of the MusicFileInfo class.</returns>
        public async Task<MusicFileInfo> GetMusicFileInfoAsync(StorageFile file)
        {
            // call the helper to retrieve the taglib information
            TagLib.File taggedInfo = await MusicFileHelper.GetMusicFileInfoAsync(file);

            //  construct the MusicFileInfo from the output, adding in the necessary information
            //  from the actual file object.
            var musicFileInfo = MusicFileMapper.MapTaglibFileToMusicFileInfo(taggedInfo);

            //  Return the populated object.
            return musicFileInfo;
        }
    }
}
