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
    public class ImageFactory : AbstractFactory<Image>
    {
        public override Image Create()
        {
            var newImage = new ConcreteImage()
            {
                Id = 0,
                Size = ImageSizeEnum.Small,
                Url = string.Empty
            };
            return newImage;
        }
    }
}
