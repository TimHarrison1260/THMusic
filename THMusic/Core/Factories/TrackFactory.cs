//***************************************************************************************************
//Name of File:     TrackFactory.cs
//Description:      A concrete Factory: create instances of an Track class.
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
    public class TrackFactory : AbstractFactory<Track>
    {
        private readonly AbstractFactory<Artist> _artistFactory;
        private readonly AbstractFactory<Album> _albumFactory;

        public TrackFactory(AbstractFactory<Artist> ArtistFactory, AbstractFactory<Album> AlbumFactory)
        {
            if (ArtistFactory == null)
                throw new ArgumentNullException("ArtistFactory", "No valid Artist Factory supplied");
            _artistFactory = ArtistFactory;
            if (AlbumFactory == null)
                throw new ArgumentNullException("AlbumFactory", "No valid Album Factory supplied");
            _albumFactory = AlbumFactory;
        }
        public override Track Create()
        {
            var newTrack = new ConcreteTrack()
            {
                Id = 0,
                Number = 0,
                Title = string.Empty,
                Album = _albumFactory.Create(),
                Artist = _artistFactory.Create(),
                Duration = TimeSpan.MinValue,
                Mbid = string.Empty,
                Url = string.Empty,
                mediaFilePath = string.Empty,
                Playlists = new List<PlayList>()
            };
            return newTrack;
        }
    }
}
