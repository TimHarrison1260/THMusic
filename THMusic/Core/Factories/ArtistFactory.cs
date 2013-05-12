//***************************************************************************************************
//Name of File:     ArtistFactory.cs
//Description:      A concrete Factory: create instances of an Artist class.
//Author:           Tim Harrison
//Date of Creation: Apr/May 2013.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************

using System.Collections.Generic;
using Core.Model;
using Core.Model.ConcreteClasses;

namespace Core.Factories
{
    /// <summary>
    /// This <c>ArtistFactory</c> is the factory class used to 
    /// generate instances of the <see cref="Core.Model.Artist"/> class.
    /// It derives from the base <see cref="Core.Factories.AbstractFactory"/>
    /// class specifying the type of <c>Artist</c>.
    /// </summary>
    public class ArtistFactory : AbstractFactory<Artist>
    {
        /// <summary>
        /// Create the concrete implementation of the abstract Artist class
        /// </summary>
        /// <returns>New instance of ConcreteArtist</returns>
        public override Artist Create()
        {
            var newArtist = new ConcreteArtist()
            {
                Id = 0,                 //  Implies new Artist.
                Name = string.Empty,
                Mbid = string.Empty,
                Url = string.Empty,
                AlbumIds = new List<int>(),
                Albums = new List<Album>()
            };
            return newArtist;
        }
    }
}
