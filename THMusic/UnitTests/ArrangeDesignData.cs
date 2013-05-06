using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Core.Model;
//using Core.Interfaces;
using Core.Factories;
//using Infrastructure.Design;
using Infrastructure.Data;

namespace UnitTests
{
    public static class ArrangeDesignData
    {
        /// <summary>
        /// Arrange the Design instance of the Music Collection
        /// for use as the unit test context.
        /// </summary>
        /// <returns>An instance of the Design time Music Collection</returns>
        internal static MusicCollection Arrange_Db()
        {
            //  Instantiate the factories for creating the objects
            var _artistFactory = new ArtistFactory();
            var _wikiFactory = new WikiFactory();
            var _albumFactory = new AlbumFactory(_artistFactory, _wikiFactory);
            var _trackFactory = new TrackFactory();
            var _imageFactory = new ImageFactory();

            //  Instantiate the DesignMusicCollection which contains static data, used at design time, 
            //return new DesignMusicCollection(_artistFactory, _albumFactory, _trackFactory, _imageFactory);

            return null;
        }
    }
}
