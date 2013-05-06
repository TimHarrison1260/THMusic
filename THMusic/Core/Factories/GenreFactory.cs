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
    public class GenreFactory : AbstractFactory<Genre>
    {
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
