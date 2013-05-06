//***************************************************************************************************
//Name of File:     GroupModelHelper.cs
//Description:      The GroupModelHelper provides loading and mapping functionality for the GroupViewModel.
//Author:           Tim Harrison
//Date of Creation: Apr/May 2013.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Windows.ApplicationModel.Resources;

using THMusic.DataModel;
using Core.Interfaces;
using Core.Model;

namespace THMusic.Helpers
{
    /// <summary>
    /// This <c>GroupModelHelper</c> class is responsible for managing all
    /// access to data within the Domain model, and mapping it to the
    /// GroupModel of the Ui, which supports the GroupsViewModel (MainViewModel).
    /// The AlbumsViewModel is part of the MVVM pattern implemented within
    /// the UI, and supported by the MVMLight framework.
    /// </summary>
    public static class GroupModelHelper
    {
         /// <summary>
        /// Provides access to the resouce files, Localisation, used
        /// when describing the Group Totals.
        /// </summary>
        private static ResourceLoader _loader = new ResourceLoader();

        /// <summary>
        /// Design time routine only.  For the minute
        /// </summary>
        /// <param name="ArtistRepository"></param>
        /// <returns></returns>
        public static async Task<List<GroupModel>> Load(GroupTypeEnum groupType, IArtistRepository ArtistRepository)
        {
            //  Call the repository 
            var groups = new List<GroupModel>();

            var artists = await ArtistRepository.GetAllArtists() as List<Artist>;
            foreach (var a in artists)
            {
                string image = await ArtistRepository.GetArtistAlbumImage(a.Id);

                int totalAlbums = await ArtistRepository.GetArtistAlbums(a.Id);
                int totalTracks = await ArtistRepository.GetArtistTracks(a.Id);
                TimeSpan totalDuration = await ArtistRepository.GetArtistDuration(a.Id);
                string description = LocalisationHelper.LocaliseDescription(totalAlbums, totalDuration);

                //  Add to the GroupItems after this.
                var groupmodel = new GroupModel()
                {
                    Name = a.Name,
                    Description = description
                };
                groupmodel.SetImage(image);
                groups.Add(groupmodel);
            }
            return groups;
        }


        /// <summary>
        /// Helper method to load the GroupModel that supports the MainViewModel
        /// with the corresponding group category.
        /// </summary>
        /// <returns></returns>
        public static async Task<List<GroupModel>> LoadAsync(GroupTypeEnum groupType, IArtistRepository ArtistRepository)
        {
            //  TODO: convert this to accept the Group Type parameter.  Only use Artist for now.
            //  Call the repository 
            var groups = new List<GroupModel>();

            var artists = await ArtistRepository.GetAllArtists() as List<Artist>;
            foreach (var a in artists)
            {


                string image = await ArtistRepository.GetArtistAlbumImage(a.Id);

                int totalAlbums = await ArtistRepository.GetArtistAlbums(a.Id);
                int totalTracks = await ArtistRepository.GetArtistTracks(a.Id);
                TimeSpan totalDuration = await ArtistRepository.GetArtistDuration(a.Id);
                string description = LocalisationHelper.LocaliseDescription(totalAlbums, totalDuration);

                //  Add to the GroupItems after this.
                var groupmodel = new GroupModel()
                {
                    UniqueId = new GroupId() {
                        Id = a.Id,
                        Type = GroupTypeEnum.Artist
                    },
                    Name = a.Name,
                    Description = description
                };
                groupmodel.SetImage(image);
                groups.Add(groupmodel);
            }
            return groups;
        }

    }
}
