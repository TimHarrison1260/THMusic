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
    /// <summary>
    /// This <c>DesignArtistRepository</c> sources the design time data for the main page
    /// </summary>
    public class DesignArtistRepository : Core.Interfaces.IArtistRepository
    {
        private List<Core.Model.Artist> _artists = new List<Core.Model.Artist>();

        /// <summary>
        /// ctor:
        /// </summary>
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

        /// <summary>
        /// Get both design time artist
        /// </summary>
        /// <returns>The artists</returns>
        public async Task<IEnumerable<Core.Model.Artist>> GetAll()
        {
            return _artists;
        }

        /// <summary>
        /// Get the image for either artist
        /// </summary>
        /// <param name="Id">the artist Id</param>
        /// <returns>the image url</returns>
        public async Task<string> GetFirstAlbumImage(int Id)
        {
            if (Id == 1)
                return @"Assets\DarkSideOfTheMoonLarge.png";
            else
                return @"Assets\DamnationLarge.png";
        }

        /// <summary>
        /// Get the number of albums for the artist
        /// </summary>
        /// <param name="Id">the id of the artist</param>
        /// <returns>the number of albums</returns>
        public async Task<int> GetAlbums(int Id)
        {
            if (Id == 1)
                return 2;
            else
                return 1;
        }

        /// <summary>
        /// Gets the number of tracks for the artist
        /// </summary>
        /// <param name="Id">The artist id</param>
        /// <returns>The number of tracks</returns>
        public async Task<int> GetTracks(int Id)
        {
            if (Id == 1)
                return 9;
            else
                return 8;
        }

        /// <summary>
        /// Gets the duration of the albums for the artist
        /// </summary>
        /// <param name="Id">the artist id</param>
        /// <returns>the total duration</returns>
        public async Task<TimeSpan> GetDuration(int Id)
        {
            if (Id == 1)
                return new TimeSpan(0, 43, 15);
            else
                return new TimeSpan(0, 42, 51);
        }

        /// <summary>
        /// Not mplemented
        /// </summary>
        /// <param name="Id">the id</param>
        /// <returns>Task</returns>
        public async Task<Core.Model.Artist> GetById(int Id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Not implemented
        /// </summary>
        /// <param name="Name">name</param>
        /// <returns>Task</returns>
        public async Task<Core.Model.Artist> GetByName(string Name)
        {
            throw new NotImplementedException();
        }
    }
}
