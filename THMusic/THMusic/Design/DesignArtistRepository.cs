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

namespace THMusic.Design
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


        public async Task<IEnumerable<Core.Model.Artist>> GetAll()
        {
            return _artists;
        }

        public async Task<string> GetFirstAlbumImage(int Id)
        {
            if (Id == 1)
                return @"Assets\DarkSideOfTheMoonLarge.png";
            else
                return @"Assets\DamnationLarge.png";
        }

        public async Task<int> GetAlbums(int Id)
        {
            if (Id == 1)
                return 2;
            else
                return 1;
        }

        public async Task<int> GetTracks(int Id)
        {
            if (Id == 1)
                return 9;
            else
                return 8;
        }

        public async Task<TimeSpan> GetDuration(int Id)
        {
            if (Id == 1)
                return new TimeSpan(0, 43, 15);
            else
                return new TimeSpan(0, 42, 51);
        }

        public async Task<Core.Model.Artist> GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<Core.Model.Artist> GetByName(string Name)
        {
            throw new NotImplementedException();
        }
    }
}
