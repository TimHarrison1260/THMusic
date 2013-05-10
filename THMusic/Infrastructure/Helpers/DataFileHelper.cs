//***************************************************************************************************
//Name of File:     DataFileHelper.cs
//Description:      Gets the file for the MusicCatalogue
//                  NB:  This is based on code from one of my previous courseworks.  
//                          This is code reuse, without re-inventing the wheel.
//                          Already confirmed with Brian MacDonald as OK.
//Author:           Tim Harrison
//Date of Creation: Apr/May 2013.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************

using System.Threading.Tasks;       //  Async stuff.
using Windows.Storage;              //  Storage stuff.

namespace Infrastructure.Helpers
{
    /// <summary>
    /// Static class <c>SearchesFileHelper</c> gets the XML serialised
    /// file that is located within the local folder. 
    /// </summary>
    public static class DataFileHelper
    {
        private const string _dataFileName = "MusicCatalogue.xml";

        /// <summary>
        /// GetDataFile gets the datafile 'SearchesData.xml' from the 
        /// local folder 
        /// </summary>
        /// <returns>Returns a Windows.Storage.StorageFile instance.</returns>
        /// <remarks>
        /// It is an asynchronous static method.
        /// </remarks>
        public static async Task<StorageFile> GetDataFile()
        {
            StorageFile dataFile = null;
            var localFolder = FileIOHelper.GetLocalFolder();
            dataFile = await FileIOHelper.GetFile(_dataFileName, localFolder);
            return dataFile;
        }

        /// <summary>
        /// CreateDataFile creates the datafile 'SearchesData.xml' in the
        /// local folder and returns the instance.
        /// </summary>
        /// <returns>returns a Windows.Storage.StorageFile instance.</returns>
        public static async Task<StorageFile> CreateDataFile()
        {
            StorageFile dataFile = null;
            var localFolder = FileIOHelper.GetLocalFolder();
            dataFile = await FileIOHelper.CreateFile(_dataFileName, localFolder);
            return dataFile;
        }
    }
}
