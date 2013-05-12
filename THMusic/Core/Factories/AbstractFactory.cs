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
    /// <summary>
    /// This <c>AbstractFactory</c> class is the abstract base
    /// class for the Factiroes used to create instance
    /// of the abstract classes
    /// </summary>
    /// <typeparam name="T">The type of class the derived factory creates</typeparam>    
    public abstract class AbstractFactory<T> where T: class
    {
        /// <summary>
        /// The Create methods, which creates a instance of the
        /// class determined by the type T.
        /// </summary>
        /// <typeparam name="T">The type of class the derived factory creates</typeparam>    
        /// <returns>The instantiated class</returns>
        public abstract T Create();
    }
}
