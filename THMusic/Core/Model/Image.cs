//***************************************************************************************************
//Name of File:     Image.cs
//Description:      The Image class of the domain model: it is an abstract class.
//Author:           Tim Harrison
//Date of Creation: Apr/May 2013.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************

using System.Xml.Serialization;
using Core.Model.ConcreteClasses;

namespace Core.Model
{
    /// <summary>
    /// This <c>Image</c> class contains the paths to the various
    /// images or different sizes, for displaying with an Album. 
    /// It is an abstract class to aid separation of
    /// concerns thoughout the application.  Instances are created using 
    /// the <see cref="Core.Factories.ImageFactory"/> Create() method.
    /// </summary>
    /// <remarks>
    /// <para>
    /// It is decorated with the XmlInclude() attribute so that the the concrete implementation
    /// is persisted.
    /// </para>
    /// </remarks>
    [XmlInclude(typeof(ConcreteImage))]
    public abstract class Image
    {
        /// <summary>
        /// Gets or sets the unique Id of the Image
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the Size of the image. <see cref="Core.Model.ImageSizeEnum"/>.
        /// </summary>
        public ImageSizeEnum Size { get; set; }
        /// <summary>
        /// Gets or sets the LastFM Url, pointing to the Image on LastFM.
        /// </summary>
        public string Url { get; set; }
    }
}
