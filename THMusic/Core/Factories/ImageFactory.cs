//***************************************************************************************************
//Name of File:     ImageFactory.cs
//Description:      A concrete Factory: create instances of an Image class.
//Author:           Tim Harrison
//Date of Creation: Apr/May 2013.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************

using Core.Model;
using Core.Model.ConcreteClasses;

namespace Core.Factories
{
    /// <summary>
    /// This <c>ImageFactory</c> is the factory class used to 
    /// generate instances of the <see cref="Core.Model.Image"/> class.
    /// It derives from the base <see cref="Core.Factories.AbstractFactory"/>
    /// class specifying the type of <c>Album</c>.
    /// </summary>    
    public class ImageFactory : AbstractFactory<Image>
    {
        /// <summary>
        /// Create the concrete implementation of the abstract Image class
        /// </summary>
        /// <returns>New instance of ConcreteImage</returns>
        public override Image Create()
        {
            var newImage = new ConcreteImage()
            {
                Id = 0,
                Size = ImageSizeEnum.small,
                Url = string.Empty
            };
            return newImage;
        }
    }
}
