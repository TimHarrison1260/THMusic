//***************************************************************************************************
//Name of File:     IPersistenceRepository.cs
//Description:      An interface describing the PersistenceRepository, for the App Load and Save functionality.
//Author:           Tim Harrison
//Date of Creation: Apr/May 2013.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************
using System.Threading.Tasks;

namespace Core.Interfaces
{
    /// <summary>
    /// This <c>IPersistenceRepository</c> interface describes the contract
    /// for the <see cref="Infrastructure.Repositories.PersistenceRepository"/>.
    /// </summary>
    public interface IPersistenceRepository
    {
        /// <summary>
        /// Retrieves the data from the underlying file, in an asynchronous way, 
        /// rebuilds the navigation properties and loads the related collections.
        /// If no data is actually loaded, then it initialises with some static data.
        /// </summary>
        /// <returns>A Task so that it can be 'awaited'.</returns>
        Task LoadAsync();
        /// <summary>
        /// Persists the data in the underlying file, in an asynchronous way.
        /// </summary>
        /// <returns>A Task so that it can be 'awaited'.</returns>
        Task SaveAsync();
    }
}
