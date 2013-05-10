//***************************************************************************************************
//Name of File:     IGroupModelDataService.cs
//Description:      The IGroupModelDataService interface provides loading and mapping functionality 
//                  for the GroupViewModel.
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
    /// <summary>
    /// This <c>IGroupModelDataService</c> interface describes the functionality
    /// of the <see cref="THMusic.Data.GroupModelDataService"/>, which is responsible 
    /// for managing all 
    /// access to data within the Domain model, and mapping it to the
    /// GroupModel of the Ui, which supports the GroupsViewModel (MainViewModel).
    /// The AlbumsViewModel is part of the MVVM pattern implemented within
    /// the UI, and supported by the MVMLight framework.
    /// </summary>
    public interface IGroupModelDataService
    {
        /// <summary>
        /// Gets the Group summary information for all albums in the collection
        /// </summary>
        /// <param name="groupType">The Type of grouping. <see cref="THMusic.DataModel.GroupTypeEnum"/>.</param>
        /// <returns>The collection of Group summaries</returns>
        Task<List<GroupModel>> LoadAsync(GroupTypeEnum groupType);

        Task<GroupModel> LoadGroupAsync(int Id, GroupTypeEnum groupType);

    }
}
