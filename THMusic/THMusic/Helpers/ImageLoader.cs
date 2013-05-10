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

namespace THMusic.Helpers
{
    /// <summary>
    /// This <c>ImageLoader</c> class is responsible for loading
    /// an image, as a bitmap, for the supplied path.
    /// </summary>
    /// <remarks>
    /// This class is kept within the UI layer, because it handles loading images from
    /// multiple sources: namely from the Assats folder within the UI project, for design resources; 
    /// via the supplied LastFM url, from imported files, as well as imported media files; 
    /// It is therefore a UI concern only.
    /// </remarks>
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
            var designImage = new BitmapImage();

            //  Temp code for design time stuff
            if (imagePath.StartsWith(@"Assets\"))
            {
                //  Design time image, get it from the Assets folder in the project.  These are desing images only.
                Uri _baseUri = new Uri("ms-appx:///");
                designImage = new BitmapImage(new Uri(_baseUri, imagePath));
                //return designImage;
            }

            //  If the image points to a LastFM url for the image, let it use it.
            if (imagePath.StartsWith(@"http://"))
            {
                designImage = new BitmapImage(new Uri(imagePath));
                //return designImage;
            }

            return designImage;
        }
    }
}
