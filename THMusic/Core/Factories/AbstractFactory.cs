//***************************************************************************************************
//Name of File:     AbstractFactory.cs
//Description:      A generic abstract base class for a Factory@: create instances of model classes.
//Author:           Tim Harrison
//Date of Creation: Apr/May 2013.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************

namespace Core.Factories
{
    public abstract class AbstractFactory<T> where T: class
    {
        public abstract T Create();
    }
}
