using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.Storage;          //  Windows store app file api
using TagLib;                   //  Media file tag extractor
using System.IO;                //  File IO stuff
using Windows.Storage.Streams;  //  File stream stuff

namespace THMusic.Data.Helpers
{
    public static class MusicFileHelper
    {
        /// <summary>
        /// Gets the music info from the supplied filename and subfolder name.
        /// The main folder is defaulted to the User's Music Library as a 
        /// Known folder
        /// </summary>
        /// <param name="fileName">The name of the music file, no path information</param>
        /// <param name="subFolder">The subfolder within the Music Library, often represents the Artist</param>
        /// <returns>An instance of the music file, includeing Tag information</returns>
        public static async Task<IMusicInfo> GetMusicFile(string fileName, string subFolder)
        {
            var musicFolder = FileIOHelper.GetMusicFolder();
            return await GetMusicFile(fileName, musicFolder, subFolder);
        }

        public static async Task<IMusicInfo> GetMusicFile(string fileName, StorageFolder folderName, string subFolderName)
        {
            if (fileName != null)
            {
                StorageFile musicFile = null;
                StorageFolder musicFolder = folderName;

                if (subFolderName != null)
                {
                    musicFolder = await FileIOHelper.GetSubFolder(subFolderName, folderName);
                }

                musicFile = await FileIOHelper.GetFile(fileName, musicFolder);

                //  Get the tag information and construct a Music Info Class.

                IMusicInfo musicFileInformation = new MusicInfo();      //TODO: refactor this to a factory for a concrete implementation

                //  Use Taglib Sharp to get the tag information.
                //  1.  Create a file abstraction to avoid using banned libraries
                //  http://stackoverflow.com/questions/13381494/c-sharp-windows-store-app-granting-capabilities-to-used-dll-s
                //  Explanation is as follows
                //  You can use TagLibSharp to load tags by creating a StreamFileAbstraction and passing that to File.Create. 
                //  This won't use any banned APIs.
                //  
                //  The StreamFileAbstraction class is defined below.
                //  I claim no credit for this, just that it works nicely.  The resource is disposed of
                //  as the class inherits the Taglib.file.IFileAbstraction class.
                IRandomAccessStreamWithContentType f = await musicFile.OpenReadAsync();
                TagLib.File taglibMusicFile = TagLib.File.Create(new StreamFileAbstraction(musicFile.Name, f.AsStream()));

                musicFileInformation.AlbumTitle = taglibMusicFile.Tag.Album;
                return musicFileInformation;
            }
            return null;
        }

        /// <summary>
        /// See comments for creation of the Taglib File for reading the file tag information.
        /// </summary>
        public class StreamFileAbstraction : File.IFileAbstraction
        {
            public StreamFileAbstraction(string name, Stream stream)
            {
                Name = name;
                ReadStream = stream;
                WriteStream = stream;
            }

            public void CloseStream(Stream stream)
            {
                stream.Flush();
            }

            public string Name { get; private set; }
            public Stream ReadStream { get; private set; }
            public Stream WriteStream { get; private set; }
        }



    }
}
