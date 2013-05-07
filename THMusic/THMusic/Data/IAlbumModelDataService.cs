﻿using System;
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
        /// <param name="ArtistRepository">Instance of the ArtistRepository</param>
        /// <returns></returns>
        Task<string> LoadGroupNameAsync(int Id, GroupTypeEnum Type);
        /// <summary>
        /// Helper method to load the GroupModel that supports the MainViewModel
        /// with the corresponding group category.
        /// </summary>
        /// <returns></returns>
        Task<List<AlbumModel>> LoadAlbumsAsync(int Id, GroupTypeEnum Type);

    }
}
