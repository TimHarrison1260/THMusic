//***************************************************************************************************
//Name of File:     IAlbumModelDataService.cs
//Description:      The IAlbumModelDataService interface provides loading and mapping functionality 
//                  for the AlbumViewModel.
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

using THMusic.DataModel;

namespace THMusic.Data
{
    public interface IAlbumModelDataService
    {
        /// <summary>
        /// Gets the Name of the Group, from the Unique Id, 
        /// which is Group Type and the Id of the grouping
        /// </summary>
        /// <param name="Id">The Id of the Group</param>
        /// <param name="Type">The Type of the Group</param>
        /// <returns></returns>
        Task<string> LoadGroupNameAsync(int Id, GroupTypeEnum Type);
        /// <summary>
        /// Helper method to load the GroupModel that supports the MainViewModel
        /// with the corresponding group category.
        /// </summary>
        /// <param name="Id">The Id of the Group</param>
        /// <param name="Type">The Type of the Group</param>
        /// <returns>thje name of the group</returns>
        Task<List<AlbumModel>> LoadAlbumsAsync(int Id, GroupTypeEnum Type);

    }
}
