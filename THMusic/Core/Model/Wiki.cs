//***************************************************************************************************
//Name of File:     Wiki.cs
//Description:      The Wiki class of the domain model: it is an abstract class.
//Author:           Tim Harrison
//Date of Creation: Apr/May 2013.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************
using System;

using System.Xml.Serialization;
using Core.Model.ConcreteClasses;

namespace Core.Model
{
    /// <summary>
    /// This <c>Wiki</c> class describes Wiki related information about the album.
    /// It is an abstract class to aid separation of
    /// concerns thoughout the application.  Instances are created using 
    /// the <see cref="Core.Factories.WikiFactory"/> Create() method.
    /// </summary>
    /// <remarks>
    /// <para>
    /// It is decorated with the XmlInclude() attribute so that the the concrete implementation
    /// is persisted.
    /// </para>
    /// </remarks>
    [XmlInclude(typeof(ConcreteWiki))]
    public abstract class Wiki
    {
        /// <summary>
        /// Gets or sets the unique Id of the Wiki entry
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets a summary of the Wiki Entry
        /// </summary>
        public string Summary { get; set; }
        /// <summary>
        /// Gets or sets the main content of the Wiki entry
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// Gets or sets the Date the wiki was published.
        /// </summary>
        public DateTime Published { get; set; }
    }
}
