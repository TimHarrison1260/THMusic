//***************************************************************************************************
//Name of File:     Artist.cs
//Description:      The Artist class of the domain model: it is an abstract class.
//Author:           Tim Harrison
//Date of Creation: Apr/May 2013.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************
using System.Collections.Generic;

using System.Xml.Serialization;
using Core.Model.ConcreteClasses;

namespace Core.Model
{
    /// <summary>
    /// This <c>Artist</c> class describes the information about the Artsts
    /// who have albums in the collection.  It is an abstract class to aid separation of
    /// concerns thoughout the application.  Instances are created using 
    /// the <see cref="Core.Factories.ArtistFactory"/> Create() method.
    /// </summary>
    /// <remarks>
    /// <para>
    /// It inherits from the base class <see cref="Core.Model.Group"/> so that
    /// there is common properties for all classes that are used to group Albums
    /// </para>
    /// <para>
    /// It is decorated with the XmlInclude() attribute so that the the concrete implementation
    /// is persisted.
    /// </para>
    /// </remarks>
    [XmlInclude(typeof(ConcreteArtist))]
    public abstract class Artist : Group
    {
        ///// <summary>
        ///// Gets or sets the unique id of the Artist
        ///// </summary>
        //public override int Id { get; set; }
        ///// <summary>
        ///// Gets or sets the Name of the Artist
        ///// </summary>
        //public override string Name { get; set; }
        /// <summary>
        /// Gets or sets the LastFM Url, that points to information about the Artist
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// Gets or sets the LastFM unique identifier.  Use to locate information about the Artist.
        /// </summary>
        public string Mbid { get; set; }
        /// <summary>
        /// Gets or sets the collection of albums recorded by the Artist.
        /// </summary>
        /// <remarks>
        /// <para>
        /// It is decorated with the XmlIgnore() attribute so that it is not persisted
        /// in the underlying XML file.  This is necessary as the reference to the 
        /// Albums belonging to an Artist cause a Circular reference and stops the model
        /// being serialised.
        /// </para>
        /// <para>
        /// Combining it with another property which contains only the Id's of the 
        /// albums belonging to the Artist, breaks the circular reference, and allows
        /// this information to be persisted correctly.
        /// </para>
        /// <para>
        /// These are navigation properties and are rebuilt as part of the 
        /// deserialisation process.
        /// </para>
        /// </remarks>
        [XmlIgnore()]
        public List<Album> Albums { get; set; }
        /// <summary>
        /// Gets or sets the collection of albumIds corresponding to the Albums
        /// </summary>
        public List<int> AlbumIds { get; set; }
        //  TODO: include logic to ensure update is successful to both properties setting only one of them.
    }
}
