//***************************************************************************************************
//Name of File:     WikiFactory.cs
//Description:      A concrete Factory: create instances of an Wiki class.
//Author:           Tim Harrison
//Date of Creation: Apr/May 2013.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************
using System;

using Core.Model;
using Core.Model.ConcreteClasses;

namespace Core.Factories
{
    public class WikiFactory : AbstractFactory<Wiki>
    {
        public override Wiki Create()
        {
            var newWiki = new ConcreteWiki()
            {
                Id = 0,
                Summary = string.Empty,
                Content = string.Empty,
                Published = DateTime.MinValue
            };
            return newWiki;
        }
    }
}
