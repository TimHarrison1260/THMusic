//***************************************************************************************************
//Name of File:     ImageLoader.cs
//Description:      Gets the image file and returns the Bitmap.
//Author:           Tim Harrison
//Date of Creation: Apr/May 2013.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************
using System;
using System.Threading.Tasks;

using Windows.UI.Xaml.Media.Imaging;

using Windows.Storage;
using Windows.Storage.Streams;
using System.Collections.Generic;      //  IRandomAccessStream
using System.Linq;

namespace THMusic.DataModel
{
    /// <summary>
    /// This <c>ImageLoader</c> class is responsible for loading
    /// an image, as a bitmap, for the supplied path.
    /// </summary>
    public static class ImageLoader
    {
        /// <summary>
        /// Accepts the full path to an image file and returns
        /// the image as a BitmapImage.  It is an async method.
        /// </summary>
        /// <param name="imagePath">The string containing the path to the image file</param>
        /// <returns>The BitmapImage of the file.</returns>
        public static async Task<BitmapImage> LoadImageAsync(string imagePath)
        {
            //  TODO: Refactor this yukky code, design time image loading. move main module to layer projects.
            //  Temp code for design time stuff
            if (imagePath.StartsWith(@"Assets\"))
            {
                //  Design time image, get it from the Assets folder in the project.  These are desing images only.
                Uri _baseUri = new Uri("ms-appx:///");
                var designImage = new BitmapImage(new Uri(_baseUri, imagePath));
                return designImage;
            }

            //  If the image points to a LastFM url for the image, let it use it.
            if (imagePath.StartsWith(@"http://"))
            {
                var designImage = new BitmapImage(new Uri(imagePath));
                return designImage;
            }

            //  Get a handle on the User library, pointing to the AlbumCovers sub-folder
            var folder = await KnownFolders.MusicLibrary.GetFolderAsync("AlbumCovers");


            //  Get the files available from the localFolder
            Windows.Storage.Search.StorageFileQueryResult fileResults = folder.CreateFileQuery();

            //  Get the list of files from the query against the localFolder
            IReadOnlyList<StorageFile> fileList = await fileResults.GetFilesAsync();

            //  Look for our file in the results.
            var imageFile = fileList.SingleOrDefault(f => f.Name == imagePath);


            if (imageFile == null) return new BitmapImage();

            //  Get the image file in the folder
            //var imageFile = await folder.GetFileAsync(imagePath);

            
            //  Load in the BitmapImage
            var bitmapimage = await LoadBitMap(imageFile);

            return bitmapimage;
        }

        /// <summary>
        /// Reads the image pointed to by the Storagefile, asynchronously
        /// to load it into the bitmapImage.  the file is located within
        /// the KnownFolders, which must be defined in the capabilities
        /// of the application manifest.
        /// </summary>
        /// <param name="imageFile">The Storagefile object pointing to the image file</param>
        /// <returns>A bitmapImage of the file.</returns>
        private static async Task<BitmapImage> LoadBitMap(StorageFile imageFile)
        {
            //  TODO: refactor this code to use the 'using' construct to ensure the resource is disposed of correctly.
            IRandomAccessStream fs = await imageFile.OpenReadAsync();
            BitmapImage bitmapImage = new BitmapImage();
//            await bitmapImage.SetSourceAsync(fs);
            bitmapImage.SetSource(fs);
            return bitmapImage;
        }
    }
}
