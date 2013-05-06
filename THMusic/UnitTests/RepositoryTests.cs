using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;


using Core.Model;
using Core.Interfaces;
using Core.Factories;
using Infrastructure.Design;
using Infrastructure.Repositories;


namespace UnitTests
{
    [TestClass]
    public class RepositoryTests
    {
        [TestMethod]
        public async void GetAllArtists()
        {
            //  Arrange
            var _db = ArrangeDesignData.Arrange_Db();
            var _artistRepository = new ArtistRepository(_db);

            //  Action
            var Artists = await _artistRepository.GetAllArtists();



            //  Assert
            Assert.AreEqual(2, Artists.Count(), "Expected 2 artists to be returned");
        }
    }
}
