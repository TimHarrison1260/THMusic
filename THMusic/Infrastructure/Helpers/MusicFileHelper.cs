//***************************************************************************************************
//Name of File:     MusicFileHelper.cs
//Description:      Provides methods to access the music files being processed by the MusicFileService
//Author:           Tim Harrison
//Date of Creation: Apr/May 2013.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.Storage;          //  Windows store app file api
using TagLib;                   //  Media file tag extractor
using System.IO;                //  File IO stuff
using Windows.Storage.Streams;  //  File stream stuff
using Core.Services;

namespace Infrastructure.Helpers
{
    /// <summary>
    /// This <c>MusicFileHelper</c> class is responsible for handling the 
    /// access to the audio file with the Taglib# library
    /// </summary>
    public static class MusicFileHelper
    {
        /// <summary>
        /// Gets the music info from the supplied filename and subfolder name.
        /// The main folder is defaulted to the User's Music Library as a 
        /// Known folder
        /// </summary>
        /// <param name="musicFile">The instance of the music file to be read.</param>
        /// <returns>An instance of the music file, includeing Tag information</returns>
        public static async Task<TagLib.File> GetMusicFileInfoAsync(StorageFile musicFile)
        {
            MusicFileInfo musicFileInformation = new MusicFileInfo();

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

            return taglibMusicFile;
        }

        /// <summary>
        /// See comments for creation of the Taglib File for reading the file tag information.
        /// </summary>
        private class StreamFileAbstraction : File.IFileAbstraction
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
