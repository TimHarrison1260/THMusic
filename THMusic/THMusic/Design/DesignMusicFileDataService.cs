using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using THMusic.Data;

namespace THMusic.Design
{
    /// <summary>
    /// This <c>DesignMusecFileDataService</c> is a stub to allow the xaml designer to be able
    /// to resolve all references foudn in the design time data.
    /// </summary>
    public class DesignMusicFileDataService : IMusicFileDataService
    {
        /// <summary>
        /// Not imiplemented: stub only
        /// </summary>
        /// <param name="MusicFiles"><files/param>
        /// <param name="Grouping">grouping</param>
        /// <returns>ing</returns>
        public Task<List<int>> ProcessMusicFiles(IReadOnlyList<Windows.Storage.StorageFile> MusicFiles, DataModel.GroupTypeEnum Grouping)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Not implemented: stub only
        /// </summary>
        /// <param name="MusicFile">files</param>
        /// <returns>album</returns>
        public Task<Core.Model.Album> ImportAlbumAsync(Windows.Storage.StorageFile MusicFile)
        {
            throw new NotImplementedException();
        }
    }
}
