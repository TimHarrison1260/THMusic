//***************************************************************************************************
//Name of File:     Album.cs
//Description:      The Album class of the domain model: it is an abstract class.
//Author:           Tim Harrison
//Date of Creation: Apr/May 2013.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************

using System;
using System.Collections.Generic;

using System.Xml.Serialization;

namespace Core.Model
{
    /// <summary>
    /// This <c>Album</c> class describes the information held for an album 
    /// in the Domain model.  It is an abstract class to aid separation of
    /// concerns thoughout the application.  Instances are created using 
    /// the <see cref="Core.Factories.AlbumFactory"/> Create() method.
    /// </summary>
    /// <remarks>
    /// <para>
    /// It is decorated with the XmlInclude() attribute so that the the concrete implementation
    /// is persisted.
    /// </para>
    /// </remarks>
    [XmlInclude(typeof(ConcreteClasses.ConcreteAlbum))]
    public abstract class Album
    {
        /// <summary>
        /// Gets or sets the unique Id of the Album
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Gets or Sets the Title of the album
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Gets or sets the Artist responsible for the Album
        /// </summary>
        public Artist Artist { get; set; }
        /// <summary>
        /// Gets or sets the release data of the album
        /// </summary>
        public DateTime Released { get; set; }
        /// <summary>
        /// Gets or sets the collection of tracks that belong to the Album
        /// </summary>
        public List<Track> Tracks { get; set; }
        /// <summary>
        /// Gets or sets the LastFM URL that points to the Album information
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// Gets or sets the LastFM mbid field that identifies the Album
        /// </summary>
        /// <remarks>
        /// This can be used to search for LastFM information 
        /// instead of the albumand Artits name.
        /// </remarks>
        public string Mbid { get; set; }
        /// <summary>
        /// Gets or sets the collection of album cover images of various sizes.
        /// </summary>
        public List<Image> Images { get; set; }
        /// <summary>
        /// Gets or sets the wiki entry related to the Album
        /// </summary>
        public Wiki Wiki { get; set; }
        /// <summary>
        /// Gets or sets a collection of Genres categorizing the Album content
        /// </summary>
        public List<Genre> Genres { get; set; }
    }
}
