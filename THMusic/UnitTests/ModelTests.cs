using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;


using Core.Model;
using Core.Interfaces;
using Core.Factories;
using Infrastructure.Design;


namespace UnitTests
{
    [TestClass]
    public class ModelTests
    {
        [TestMethod]
        public void GetAllAlbums_Returns_2()
        {
            //  Arrange
            var _db = ArrangeDesignData.Arrange_Db();

            //  Action
            var _albums = _db.Albums;

            //  Assert
            Assert.AreEqual(2, _albums.Count(), "Expected 2 albums returned");

        }

        [TestMethod]
        public void GetDarkSideOfTheMoon()
        {
            //  Arrange
            var _db = ArrangeDesignData.Arrange_Db();
//            var _db = Arrange_Db();

            //  Action
            var darkSide = _db.Albums.First(a => a.Id == 1);

            //  Assert
            Assert.AreEqual("The Dark Side of the Moon", darkSide.Title, "Expected 'Dark Side of the Moon'");

        }

        [TestMethod]
        public void GetDarkSideOfTheMoon_Time()
        {
            //  Arrange
            var _db = ArrangeDesignData.Arrange_Db();
//            var _db = Arrange_Db();

            //  Action
            var time = _db.Albums.SelectMany(a => a.Tracks.Where(t => t.Title == "Time")).First();

            //  Assert
            Assert.AreEqual("Time", time.Title, "Expected the title to be 'Time'");
        }



        //private DesignMusicCollection Arrange_Db()
        //{
        //    //  Instantiate the factories for creating the objects
        //    var _artistFactory = new ArtistFactory();
        //    var _albumFactory = new AlbumFactory();
        //    var _trackRactory = new TrackFactory();
        //    //  Instantiate the DesignMusicCollection which contains static data, used at design time, 
        //    return new DesignMusicCollection(_artistFactory, _albumFactory, _trackRactory);
        //}
    }
}
