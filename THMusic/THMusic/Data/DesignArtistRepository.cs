//***************************************************************************************************
//Name of File:     DesignArtistRepository.cs
//Description:      The DesignArtistRepository provides design time data for the mainviewModel 
//                  and therefore the ItemsPage.Xaml.
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

namespace THMusic.Data
{
    public class DesignArtistRepository : Core.Interfaces.IArtistRepository
    {
        private List<Core.Model.Artist> _artists = new List<Core.Model.Artist>();

        public DesignArtistRepository()
        {
            _artists = new List<Core.Model.Artist>() 
            {
                new Core.Model.ConcreteClasses.ConcreteArtist()
                {
                    Id = 1,
                    Name = "Pinkfloyd",
                    Mbid = string.Empty,
                    Url = string.Empty,
                    AlbumIds = new List<int>(),
                    Albums = new List<Core.Model.Album>()
                },
                new Core.Model.ConcreteClasses.ConcreteArtist()
                {
                    Id = 2,
                    Name = "Opeth",
                    Mbid = string.Empty,
                    Url = string.Empty,
                    AlbumIds = new List<int>(),
                    Albums = new List<Core.Model.Album>()
                }
            };
        }


        public async Task<IEnumerable<Core.Model.Artist>> GetAllArtists()
        {
            return _artists;
        }

        public async Task<string> GetArtistAlbumImage(int Id)
        {
            if (Id == 1)
                return @"Assets\DarkSideOfTheMoonLarge.png";
            else
                return @"Assets\DamnationLarge.png";
        }

        public async Task<int> GetArtistAlbums(int Id)
        {
            if (Id == 1)
                return 2;
            else
                return 1;
        }

        public async Task<int> GetArtistTracks(int Id)
        {
            if (Id == 1)
                return 9;
            else
                return 8;
        }

        public async Task<TimeSpan> GetArtistDuration(int Id)
        {
            if (Id == 1)
                return new TimeSpan(0, 43, 15);
            else
                return new TimeSpan(0, 42, 51);
        }

        public async Task<Core.Model.Artist> GetArtistById(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<Core.Model.Artist> GetArtistByName(string Name)
        {
            throw new NotImplementedException();
        }
    }
}
