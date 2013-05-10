//***************************************************************************************************
//Name of File:     Track.cs
//Description:      The Track class of the domain model: it is an abstract class.
//Author:           Tim Harrison
//Date of Creation: Apr/May 2013.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************
using System;
using System.Collections.Generic;

using System.Xml.Serialization;
using Core.Model.ConcreteClasses;

namespace Core.Model
{
    /// <summary>
    /// This <c>Track</c> class describes the information available for
    /// a track.  It is an abstract class to aid separation of
    /// concerns thoughout the application.  Instances are created using 
    /// the <see cref="Core.Factories.TrackFactory"/> Create() method.
    /// </summary>
    /// <remarks>
    /// <para>
    /// It is decorated with the XmlInclude() attribute so that the the concrete implementation
    /// is persisted.
    /// </para>
    /// </remarks>
    [XmlInclude(typeof(ConcreteTrack))]
    public abstract class Track
    {
        /// <summary>
        /// Gets or sets the unique Id for the Track
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the track number within the Album
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// Gets or sets the Title of the Track
        /// </summary>
        public string Title { get; set; }

        private TimeSpan _duration;
        /// <summary>
        /// Gets or sets the playing duration of the track
        /// </summary>
        /// <remarks>
        /// <para>
        /// Duration is a <see cref="System.Timespan"/> and XmlSerialisation
        /// will not not serialise the value.  However, by pairing it with
        /// another property that can be serialised, and marking this one
        /// with the XmlIgnore() attribute, allows the timespan value to 
        /// be serialised as Ticks.
        /// </para>
        /// <para>
        /// Both properties internally reference the same Timespan object
        /// and the DurationTicks is decorated with the XmlElement attribute
        /// so the name can be correctly represented in the serialised file.
        /// </para>
        /// </remarks>
        [XmlIgnore()]
        public TimeSpan Duration
        {
            get { return _duration; }
            set { _duration = value; }
        }
        /// <summary>
        /// Gets or sets the duration of a track.  <see cref="Core.Model.Track.Duration"/>.
        /// </summary>
        [XmlElement("Duration")]
        public long DurationTicks
        {
            get { return _duration.Ticks; }
            set { _duration = new TimeSpan(value); }
        }

        /// <summary>
        /// Gets or sets the Artist for the Specific Track
        /// </summary>
        public Artist Artist { get; set; }
        /// <summary>
        /// Gets or sets the LastFM mbid that identifies the track to LastFM
        /// </summary>
        public string Mbid { get; set; }
        /// <summary>
        /// Gets or sets the url that points to the LastFM information page for this track
        /// </summary>
        public string Url { get; set; }

        public string mediaFilePath { get; set; }
        /// <summary>
        /// Gets or sets the navigation reference to the Album this track belongs to.
        /// </summary>
        /// <remarks>
        /// <para>
        /// It is decorated with the XmlIgnore() attribute so that it is not persisted
        /// in the underlying XML file.  This is necessary as the reference to the 
        /// Album the Track belongs to causes a Circular reference and stops the model
        /// being serialised.
        /// </para>
        /// <para>
        /// Combining it with another property which contains only the Id of the 
        /// album the track belongs to, breaks the circular reference, and allows
        /// this information to be persisted correctly.
        /// </para>
        /// <para>
        /// These are navigation properties and are rebuilt as part of the 
        /// deserialisation process.
        /// </para>
        /// </remarks>
        [XmlIgnore()]
        public Album Album { get; set; }
        [XmlElement("Album")]
        public int AlbumId { get; set; }
        //  TODO: include logic to ensure update is successful to both properties setting only one of them.

        /// <summary>
        /// Points to the collection of playlist that this track belongs to.
        /// Navigation property
        /// </summary>
        public List<PlayList> Playlists { get; set; }
    }
}
