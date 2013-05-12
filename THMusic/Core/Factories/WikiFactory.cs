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
    /// <summary>
    /// This <c>WikiFactory</c> is the factory class used to 
    /// generate instances of the <see cref="Core.Model.Wiki"/> class.
    /// It derives from the base <see cref="Core.Factories.AbstractFactory"/>
    /// class specifying the type of <c>Wiki</c>.
    /// </summary>
    public class WikiFactory : AbstractFactory<Wiki>
    {
        /// <summary>
        /// Create the concrete implementation of the abstract Wiki class
        /// </summary>
        /// <returns>New instance of ConcreteWiki</returns>
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
