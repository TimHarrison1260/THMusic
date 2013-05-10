//***************************************************************************************************
//Name of File:     AlbumFactory.cs
//Description:      A concrete Factory: create instances of an Album class.
//Author:           Tim Harrison
//Date of Creation: Apr/May 2013.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************
using System;
using System.Collections.Generic;

using Core.Model;
using Core.Model.ConcreteClasses;

namespace Core.Factories
{
    public class AlbumFactory : AbstractFactory<Album>
    {
        private readonly AbstractFactory<Artist> _artistFactory;
        private readonly AbstractFactory<Wiki> _wikiFactory;

        public AlbumFactory(AbstractFactory<Artist> ArtistFactory, AbstractFactory<Wiki> WikiFactory)
        {
            if (ArtistFactory == null)
                throw new ArgumentNullException("ArtistFactory", "No valid Artist Factory supplied");
            _artistFactory = ArtistFactory;
            if (WikiFactory == null)
                throw new ArgumentNullException("WikiFactory", "No valid Wiki Factory supplied");
            _wikiFactory = WikiFactory;
        }

        /// <summary>
        /// Create the concrete implementation of the abstract Album class
        /// </summary>
        /// <returns>New instance of ConcreteAlbum</returns>
        public override Album Create()
        {
            var newAlbum = new ConcreteAlbum()
            {
                Id = 0,
                Title = string.Empty,
                Artist = _artistFactory.Create(),
                Tracks = new List<Track>(),
                Images = new List<Image>(),
                Genres = new List<Genre>(),
                Released = DateTime.MinValue,
                Mbid = string.Empty,
                Url = string.Empty,
                Wiki = _wikiFactory.Create()
            };
            return newAlbum;
        }
    }
}
