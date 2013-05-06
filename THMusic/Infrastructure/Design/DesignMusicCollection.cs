using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.Storage;
using System.IO;
using System.Xml.Serialization;

using Core.Interfaces;
using Core.Model;
using Core.Factories;
using Infrastructure.Helpers;

namespace Infrastructure.Design
{
    //  TODO: Get rid of this DesignMusicCollection from here, it's not required and doesn't
    //      fully work as there are too many inderections in the line from the UI in design mode.
    [Obsolete("Do not use this method, provide within the UI layer", true)]
    public class DesignMusicCollection : IUnitOfWork
    {
        private readonly AbstractFactory<Artist> _artistFactory;
        private readonly AbstractFactory<Album> _albumFactory;
        private readonly AbstractFactory<Track> _trackFactory;
        private readonly AbstractFactory<Image> _imageFactory;
        private readonly AbstractFactory<Genre> _genreFactory;
        private readonly AbstractFactory<PlayList> _playlistFactory;
        private readonly AbstractFactory<Wiki> _wikiFactory;


        public DesignMusicCollection(AbstractFactory<Artist> ArtistFactory, 
            AbstractFactory<Album> AlbumFactory, 
            AbstractFactory<Track> TrackFactory, 
            AbstractFactory<Image> ImageFactory,
            AbstractFactory<Genre> GenreFactory,
            AbstractFactory<PlayList> PlaylistFactory,
            AbstractFactory<Wiki> WikiFactory)
        {
            //  Load the dependencies
            if (ArtistFactory == null)
                throw new ArgumentNullException("ArtistFactory", "No valid Factory supplied to create Artist class");
            _artistFactory = ArtistFactory;
            if (AlbumFactory == null)
                throw new ArgumentNullException("AlbumFactory", "No valid Factory supplied to create Album class");
            _albumFactory = AlbumFactory;
            if (TrackFactory == null)
                throw new ArgumentNullException("TrackFactory", "No valid Factory supplied to create Track class");
            _trackFactory = TrackFactory;
            if (_imageFactory == null)
                throw new ArgumentNullException("ImageFactory", "No valid Factory supplied to create Image class");
            _imageFactory = ImageFactory;
            if (GenreFactory == null)
                throw new ArgumentNullException("GenreFactory", "No valid Factory supplied to create Genre class");
            _genreFactory = GenreFactory;
            if (PlaylistFactory == null)
                throw new ArgumentNullException("PlaylistFactory", "No valid Factory supplied to create Playlist class");
            _playlistFactory = PlaylistFactory;
            if (WikiFactory == null)
                throw new ArgumentNullException("WikiFactory", "No valid Factory supplied to create Wiki class");
            _wikiFactory = WikiFactory;

            //  Initialise the properties / collection
            this.Albums = new List<Album>();
            this.Tracks = new List<Track>();
            this.Artists = new List<Artist>();
            this.Genres = new List<Genre>();
            this.PlayLists = new List<PlayList>();

            //Task loadTheData = Task.Factory.StartNew(() => Load());
            //loadTheData.Wait();

            //  Load the data
            //  Load data into the application: TESTING ONLY
            Data.InitialiseMusicCollection.SeedData(this, _artistFactory, _albumFactory, _trackFactory, _imageFactory, _genreFactory, _playlistFactory, _wikiFactory);
        }


        /// <summary>
        /// The collostion of Albums making up the Collection
        /// </summary>
        public List<Album> Albums { get; set; }

        /// <summary>
        /// The Tracks belonging to the Aobums in the Collection
        /// </summary>
        public List<Track> Tracks { get; set; }

        /// <summary>
        /// The Artists that have albums in the collection
        /// </summary>
        public List<Artist> Artists { get; set; }

        /// <summary>
        /// The various Genres associated with album in the collection
        /// </summary>
        public List<Genre> Genres { get; set; }

        /// <summary>
        /// The various playlist containing albums and tracks from the collection
        /// </summary>
        public List<PlayList> PlayLists { get; set; }


        public Task Load()
        {
            throw new NotImplementedException("Load for DesignMusicCollection is not implemented");
        }

        public Task Save()
        {
            throw new NotImplementedException("Save for DesignMusicCollection is not implemented");
        }



        public Task<Album> CreateAlbum(Album newAlbum)
        {
            throw new NotImplementedException();
        }


        public StorageFile PersistenceFile
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
