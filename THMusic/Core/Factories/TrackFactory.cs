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

using Core.Model;
using Core.Model.ConcreteClasses;

namespace Core.Factories
{
    public class TrackFactory : AbstractFactory<Track>
    {
        public override Track Create()
        {
            var newTrack = new ConcreteTrack()
            {
                Id = 0,
                Number = 0,
                Title = string.Empty,
                Album = new ConcreteAlbum(),      //  BAD BAD BAD, use the AlbumFactory
                //Album = 0,
                Artist = new ConcreteArtist(),  //  ditto
                Duration = TimeSpan.MinValue,
                Mbid = string.Empty,
                Url = string.Empty
            };
            return newTrack;
        }
    }
}
