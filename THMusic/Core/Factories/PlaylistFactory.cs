//***************************************************************************************************
//Name of File:     PlaylistFactory.cs
//Description:      Concrete implementation of the abstract Factory@: create new instance of Playlist
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
    public class PlaylistFactory : AbstractFactory<PlayList>
    {
        public override PlayList Create()
        {
            var newPlaylist = new ConcretePlaylist()
            {
                Id = 0,
                Name = string.Empty,
                TrackIds = new List<int>(),
                Tracks = new List<Track>()
            };
            return newPlaylist;
        }
    }
}
