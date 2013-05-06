////***************************************************************************************************
////Name of File:     DataFileHelper.cs
////Description:      Gets the CSV data file for the Library data.
////Author:           Tim Harrison
////Date of Creation: Dec 2012.
////
////I confirm that the code contained in this file (other than that provided or authorised) is all 
////my own work and has not been submitted elsewhere in fulfilment of this or any other award.
////***************************************************************************************************

//using System.Threading.Tasks;       //  Async stuff.
//using Windows.Storage;              //  Storage stuff.

//namespace THMusic.Data.Helpers
//{
//    /// <summary>
//    /// Static class <c>DataFileHelper</c> gets the comma separated values
//    /// file that is located within the Assets folder of the main
//    /// application installation folder.
//    /// </summary>
//    /// <remarks>
//    /// For a Windows Store App, we have very few places where such 
//    /// a data file can be accessed and still be distributed with 
//    /// the application. (It's very restrictive).
//    /// </remarks>
//    public static class DataFileHelper
//    {
//        private const string _assetsFolder = "Assets";
//        private const string _dataFileName = "LibraryData.txt";

//        /// <summary>
//        /// GetDataFile gets the datafile 'LibraryData.txt' from the 
//        /// Assets folder within the installed location of the 
//        /// application
//        /// </summary>
//        /// <returns>Returns a Windows.Storage.StorageFile instance.</returns>
//        /// <remarks>
//        /// It is an asynchronous static method.
//        /// </remarks>
//        public static async Task<StorageFile> GetDataFile()
//        {
//            StorageFile dataFile = null;
//            var installFolder = FileIOHelper.GetInstallFolder();
//            var assetsFolder = await FileIOHelper.GetSubFolder(_assetsFolder, installFolder);
//            dataFile = await FileIOHelper.GetFile(_dataFileName, assetsFolder);
//            return dataFile;
//        }
//    }
//}
