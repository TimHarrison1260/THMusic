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
    /// <summary>
    /// This <c>TrackFactory</c> is the factory class used to 
    /// generate instances of the <see cref="Core.Model.Track"/> class.
    /// It derives from the base <see cref="Core.Factories.AbstractFactory"/>
    /// class specifying the type of <c>Track</c>.
    /// </summary>
    public class TrackFactory : AbstractFactory<Track>
    {
        private readonly AbstractFactory<Artist> _artistFactory;
        private readonly AbstractFactory<Album> _albumFactory;

        /// <summary>
        /// Constructor for the TrackFactory.
        /// </summary>
        /// <param name="ArtistFactory">Instance of the ArtistFactory</param>
        /// <param name="AlbumFactory">Instance of the AlbumFactory</param>
        public TrackFactory(AbstractFactory<Artist> ArtistFactory, AbstractFactory<Album> AlbumFactory)
        {
            if (ArtistFactory == null)
                throw new ArgumentNullException("ArtistFactory", "No valid Artist Factory supplied");
            _artistFactory = ArtistFactory;
            if (AlbumFactory == null)
                throw new ArgumentNullException("AlbumFactory", "No valid Album Factory supplied");
            _albumFactory = AlbumFactory;
        }


        /// <summary>
        /// Create the concrete implementation of the abstract Track class
        /// </summary>
        /// <returns>New instance of ConcreteTrack</returns>
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
