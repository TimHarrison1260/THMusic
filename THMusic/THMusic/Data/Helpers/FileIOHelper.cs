//***************************************************************************************************
//Name of File:     FileIOHelper.cs
//Description:      General routines providing access to the File system.
//Author:           Tim Harrison
//Date of Creation: Dec 2012.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Windows.Storage;          //  Access to file system

namespace THMusic.Data.Helpers
{
    /// <summary>
    /// Class <c> FileIOHelper</c> is a static class providing static methods
    /// to controll access to the file system and local storage folders
    /// </summary>
    public static class FileIOHelper
    {
        /// <summary>
        /// Returns the storage folder pointing to the installed location of
        /// the application
        /// </summary>
        /// <returns>Instance of StorageFolder.</returns>
        public static StorageFolder GetInstallFolder()
        {
            return Windows.ApplicationModel.Package.Current.InstalledLocation;
        }

        /// <summary>
        /// Returns the storage folder pointing to the location of the local
        /// storage folder for a Windows Store App.
        /// </summary>
        /// <returns>Instance of StorageFolder.</returns>
        public static StorageFolder GetLocalFolder()
        {
            return Windows.Storage.ApplicationData.Current.LocalFolder;
        }

        /// <summary>
        /// Returns the Known storage folder for the Music Library as set 
        /// in the application Capabilities.
        /// </summary>
        /// <returns>Instance of StorageFolder</returns>
        public static StorageFolder GetMusicFolder()
        {
            return Windows.Storage.KnownFolders.MusicLibrary;
        }



        /// <summary>
        /// Returns a Storage folder for the subfolder of the parent folder
        /// </summary>
        /// <param name="folderName">The name of the desired sub-folder.</param>
        /// <param name="ParentFolder">The name of the parent folder, containing the sub-folder</param>
        /// <returns>Instance of StorageFolder.</returns>
        public static async Task<StorageFolder> GetSubFolder(string folderName, StorageFolder ParentFolder)
        {
            //  Set the default parentFolder to the LocalFolder, as most operation will be against this folder.
            var folder = ParentFolder ?? Windows.Storage.ApplicationData.Current.LocalFolder;
            //  Get the subfolder of the parent folder.
            var subFolder = await folder.GetFolderAsync(folderName);      //  Throws exception if not found
            return subFolder;
        }

        /// <summary>
        /// Returns a collection of file names for all files within the local storage folder (Overloaded).
        /// </summary>
        /// <returns>Colliection (List(string)) of File names.</returns>
        public static async Task<IList<string>> GetFileNames()
        {
            //  Set default to LocalFolder
            return await GetFileNames(ApplicationData.Current.LocalFolder);
        }

        /// <summary>
        /// Returns a collection of file names for all files within the specified storage folder (Overloaded).
        /// </summary>
        /// <param name="Folder">The instance of StorageFolder containing the list of files.</param>
        /// <returns>Colliection (List(string)) of File names.</returns>
        public static async Task<IList<string>> GetFileNames(StorageFolder Folder)
        {
            //  Set the default parentFolder to the LocalFolder, if not defilned.
            var dataFolder = Folder ?? Windows.Storage.ApplicationData.Current.LocalFolder;
            //  Set up list for filenames
            IList<string> fileNames = new List<string>();
            //  Get the files and add them to the collection.
            foreach (var file in await dataFolder.GetFilesAsync())
            {
                fileNames.Add(file.Name);
            }
            return fileNames;
        }

        /// <summary>
        /// Returns an instance of the StorageFile for the specified filename in the local storage folder (overloaded).
        /// </summary>
        /// <param name="filename">The name of the file.</param>
        /// <returns>Instance of StorageFile.</returns>
        public static async Task<StorageFile> GetFile(string filename)
        {
            //  Set default to LocalFolder
            return await GetFile(filename, ApplicationData.Current.LocalFolder);
        }

        /// <summary>
        /// Returns an instance of the StorageFile for the specified filename in the specified storage folder (overloaded).
        /// </summary>
        /// <param name="filename">The instance of the StorageFolder.</param>
        /// <param name="Folder">The name of the file.</param>
        /// <returns>Instance of StorageFile</returns>
        public static async Task<StorageFile> GetFile(string filename, StorageFolder Folder)
        {
            StorageFile file = null;
            //  Set the default parentFolder to the LocalFolder, if not defilned.
            var dataFolder = Folder ?? Windows.Storage.ApplicationData.Current.LocalFolder;

            //  Get the files available from the localFolder
            Windows.Storage.Search.StorageFileQueryResult fileResults = dataFolder.CreateFileQuery();

            //  Get the list of files from the query against the localFolder
            IReadOnlyList<StorageFile> fileList = await fileResults.GetFilesAsync();

            //  Look for our file in the results.
            file = fileList.SingleOrDefault(f => f.Name == filename);

            return file;
        }

        /// <summary>
        /// Creates and returns the instance of StorageFile for the specified flename within the local storage folder (overloaded).
        /// </summary>
        /// <param name="filename">The name of the file to create.</param>
        /// <returns>Instance of StorageFile</returns>
        public static async Task<StorageFile> CreateFile(string filename)
        {
            //  Set default to LocalFolder
            return await CreateFile(filename, ApplicationData.Current.LocalFolder);
        }

        /// <summary>
        /// Creates and returns the instance of StorageFile for the specified flename within the specified storage folder (overloaded).
        /// </summary>
        /// <param name="filename">The name of the file to create.</param>
        /// <param name="Folder">Instance of StorageFolder where the file is to be created.</param>
        /// <returns>Instance of StorageFile.</returns>
        public static async Task<StorageFile> CreateFile(string filename, StorageFolder Folder)
        {
            StorageFile file = null;
            //  Set the default parentFolder to the LocalFolder, if not defilned.
            var dataFolder = Folder ?? Windows.Storage.ApplicationData.Current.LocalFolder;

            //  Create the file in the local folder
            file = await dataFolder.CreateFileAsync(filename);

            return file;
        }

    }
}
