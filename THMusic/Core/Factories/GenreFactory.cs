//***************************************************************************************************
//Name of File:     GenreFactory.cs
//Description:      Concrete implementation of the abstract Factory@: create new instance of Genre
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
    /// <summary>
    /// This <c>GenreFactory</c> is the factory class used to 
    /// generate instances of the <see cref="Core.Model.Genre"/> class.
    /// It derives from the base <see cref="Core.Factories.AbstractFactory"/>
    /// class specifying the type of <c>Genre</c>.
    /// </summary>
    public class GenreFactory : AbstractFactory<Genre>
    {
        /// <summary>
        /// Create the concrete implementation of the abstract Genre class
        /// </summary>
        /// <returns>New instance of ConcreteGenre</returns>
        public override Genre Create()
        {
            var newGenre = new ConcreteGenre()
            {
                Id = 0,
                Name = string.Empty,
                Url = string.Empty,
                AlbumIds = new List<int>(),
                Albums = new List<Album>()
            };
            return newGenre;
        }
    }
}
