//***************************************************************************************************
//Name of File:     BaseRepository.cs
//Description:      The generic base repository from which all repositories are derived.
//Author:           Tim Harrison
//Date of Creation: Apr/May 2013.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    /// <summary>
    /// This <c>BaseRepository</c> class is the abstract base class from which all
    /// repositories should be derived.  It is generic allowing the standard 
    /// CRUD methods to be specified within it.  It is restricted to only be 
    /// inherited by classes.
    /// </summary>
    /// <typeparam name="T">The Class the derived repository is to process.</typeparam>
    public abstract class BaseRepository<T> where T: class
    {
        /// <summary>
        /// Creates a new instance of the specified entity in the in-Memory Context
        /// </summary>
        /// <param name="entity">The Entity to be created</param>
        /// <returns>The instance of the newly created entithy</returns>
        public virtual async Task<T> CreateAsync(T entity)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Updates the instance of the specified entity in the in-memory context
        /// </summary>
        /// <param name="entity">the entity to be updated</param>
        public virtual async Task UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Deletes the instance of the entity fromt he in-memory context
        /// </summary>
        /// <param name="entity">the entity to be deleted</param>
        public virtual async Task DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }

        //        void SaveChanges();
    }
}
