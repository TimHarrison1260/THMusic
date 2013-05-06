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
    public interface IPersistenceRepository
    {
        Task LoadAsync();
        Task SaveAsync();
    }
}
