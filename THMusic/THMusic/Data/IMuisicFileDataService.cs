//***************************************************************************************************
//Name of File:     IMusicFileModelDataService.cs
//Description:      The IMusicfileModelDataService provides the interface to the MusicFileDataService.
//Author:           Tim Harrison
//Date of Creation: Apr/May 2013.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.Concurrent;
using Windows.Storage;
using THMusic.DataModel;
using Core.Services;
using Core.Model;       // only now

namespace THMusic.Data
{
    /// <summary>
    /// This <c>IMusicFileDataService</c> interface describes the <see cref="THMusic.DataMusicFileDataService"/>
    /// data servuce which is responsible for managing all access to MusicFileService and 
    /// mapping the results to the Album and adding the file to the Domain model.
    /// It supports the MainViewModel by providing the Import MP3 functionality
    /// The MainViewModel is part of the MVVM pattern implemented within
    /// the UI, and supported by the MVMLight framework.
    /// </summary>
    public interface IMusicFileDataService
    {

        Task<List<int>> ProcessMusicFiles(IReadOnlyList<StorageFile> MusicFiles, GroupTypeEnum Grouping);

        /// <summary>
        /// Imports the tagged information of the specified file, adding it to the 
        /// Music collection
        /// </summary>
        /// <param name="MusicFile">The music file to be imported</param>
        /// <returns>The id of the Artist the music file was added to.</returns>
        /// <remarks>
        /// The id of the artist is returned, so that the Artist Group summary information
        /// can be refreshed on the UI.  The MusicFileDataService supports the MainViewModel
        /// of the group page (ItemPage) only.
        /// </remarks>
        Task<Album> ImportAlbumAsync(StorageFile MusicFile);    // was int
    }
}
