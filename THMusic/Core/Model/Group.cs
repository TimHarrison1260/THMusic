//***************************************************************************************************
//Name of File:     Group.cs
//Description:      The Group class is abstract and acts as a base class for Artist, Genres and Playlists
//                  which are the classes that "group" albums.
//Author:           Tim Harrison
//Date of Creation: Apr/May 2013.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    /// <summary>
    /// This <c>Group</c> abstract class serves as a base class for the 
    /// classes that are used to group albums.  This allows them to be
    /// treated similar when used for grouping purposes.
    /// </summary>
    public abstract class Group
    {
        /// <summary>
        /// Gets or sets the unique id of the Group
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the Name of the Group
        /// </summary>
        public String Name { get; set; }
    }
}
