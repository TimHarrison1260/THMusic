//***************************************************************************************************
//Name of File:     IGenreRepository.cs
//Description:      An interface describing the GenreRepository.
//                  It inherits from the base interface IGroupRepository.  This gives all classes
//                  that participate in the "Groupin" of album, the same functionality so that
//                  the repositories can be cast and interchanged, due to inheritance
//Author:           Tim Harrison
//Date of Creation: Apr/May 2013.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Model;

namespace Core.Interfaces
{
    /// <summary>
    /// This <c>IGenreRepository</c> interface describes the contract
    /// for the <see cref="Infrastructure.Repositories.GenreRepository"/>.
    /// </summary>
    public interface IGenreRepository : IGroupRepository<Genre>
    {
    }
}
