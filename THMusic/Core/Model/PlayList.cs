//***************************************************************************************************
//Name of File:     PlayList.cs
//Description:      The Playlist class of the domain model: it is an abstract class.
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
    /// This <c>Playlist</c> class describes information about user defined 
    /// playlist, that contain tracks selected from the collection.
    /// It is an abstract class to aid separation of
    /// concerns thoughout the application.  Instances are created using 
    /// the <see cref="Core.Factories.PlaylistFactory"/> Create() method.
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
    [XmlInclude(typeof(ConcretePlaylist))]
    public abstract class PlayList : Group
    {
        ///// <summary>
        ///// Gets or sets the Unique Id of the Playlist
        ///// </summary>
        //public int Id { get; set; }
        ///// <summary>
        ///// Gets or sets the Name of the playlist
        ///// </summary>
        //public string Name { get; set; }
        /// <summary>
        /// Gets or sets the collection of Tracks in the PlayList
        /// </summary>
        /// <remarks>
        /// <para>
        /// It is decorated with the XmlIgnore() attribute so that it is not persisted
        /// in the underlying XML file.  This is necessary as the reference to the 
        /// Trackss belonging to a Playlist cause a Circular reference and stops the model
        /// being serialised.
        /// </para>
        /// <para>
        /// Combining it with another property which contains only the Id's of the 
        /// Tracks belonging to the Playlist, breaks the circular reference, and allows
        /// this information to be persisted correctly.
        /// </para>
        /// <para>
        /// These are navigation properties and are rebuilt as part of the 
        /// deserialisation process.
        /// </para>
        /// </remarks>
        [XmlIgnore()]
        public List<Track> Tracks { get; set; }
        public List<int> TrackIds { get; set; }
        //  TODO: include logic to ensure update is successful to both properties setting only one of them.
    }
}
