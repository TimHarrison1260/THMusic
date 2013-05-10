//***************************************************************************************************
//Name of File:     DesignGroupModelDataSource.cs
//Description:      The DesignGroupModelDataSource provides design time data for the GroupViewModel.
//Author:           Tim Harrison
//Date of Creation: Apr/May 2013.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Windows.ApplicationModel.Resources;

using THMusic.DataModel;
using THMusic.Helpers;
using Core.Interfaces;
using Core.Model;

using THMusic.Data;

namespace THMusic.Design
{
    /// <summary>
    /// This <c>DesignGroupModelDataService</c> class provides design time data for the 
    /// the ItemsPage and MainViewModel.
    /// </summary>
    public class DesignGroupModelDataService : IGroupModelDataService
    {

        //  Injected instances of the various repositories (Domain model)
        private readonly IArtistRepository _artistRepository;
        //private readonly IGenreRepository _genreRepository;
        //private readonly IPlaylistRepository _playlistRepository;

         /// <summary>
        /// Provides access to the resouce files, Localisation, used
        /// when describing the Group Totals.
        /// </summary>
        private  ResourceLoader _loader = new ResourceLoader();

        /// <summary>
        /// ctor: Initialise the GroupModelDataService
        /// </summary>
        /// <param name="AlbumRepository"></param>
        public DesignGroupModelDataService(IArtistRepository ArtistRepository) //, IGenreRepository GenreRepository, IPlaylistRepository PlaylistRepository)
        {
            if (ArtistRepository == null)
                throw new ArgumentNullException("ArtistRepository", "No valid Repository supplied");
            _artistRepository = ArtistRepository;
            //if (GenreRepository == null)
            //    throw new ArgumentNullException("GenreRepository", "No valid Repository supplied");
            //_genreRepository = GenreRepository;
            //if (PlaylistRepository == null)
            //    throw new ArgumentNullException("PlaylistRepository", "No valid Repository supplied");
            //_playlistRepository = PlaylistRepository;
        }


        /// <summary>
        /// Helper method to load the GroupModel that supports the MainViewModel
        /// with the corresponding group category.
        /// </summary>
        /// <returns></returns>
        public async Task<List<GroupModel>> LoadAsync(GroupTypeEnum groupType = GroupTypeEnum.Artist)
        {
            //  TODO: convert this to accept the Group Type parameter.  Only use Artist for now.
            //  Call the repository 
            var groups = new List<GroupModel>();


            //switch (groupType)
            //{
            //    case GroupTypeEnum.Artist:
            //        {
                        IEnumerable<Artist> artists = await _artistRepository.GetAll();
                        var groupRepository = _artistRepository as IGroupRepository<Artist>;
                        groups = await LoadGroupsAsync<Artist>(artists, groupType, groupRepository);
            //            break;
            //        }
            //    case GroupTypeEnum.Genre:
            //        {
            //            IEnumerable<Genre> genres = await _genreRepository.GetAll();
            //            var groupRepository = _genreRepository as IGroupRepository<Genre>;
            //            groups = await LoadGroupsAsync<Genre>(genres, groupType, groupRepository);
            //            break;
            //        }
            //    case GroupTypeEnum.Playlist:
            //        {
            //            IEnumerable<PlayList> playlists = await _playlistRepository.GetAll();
            //            var groupRepository = _playlistRepository as IGroupRepository<PlayList>;
            //            groups = await LoadGroupsAsync<PlayList>(playlists, groupType, groupRepository);
            //            break;
            //        }
            //}
            return groups;
        }

        private async Task<List<GroupModel>> LoadGroupsAsync<T>(IEnumerable<T> groups, GroupTypeEnum groupType, IGroupRepository<T> repository) where T : Core.Model.Group
        {
            //var artists = await _artistRepository.GetAllArtists() as List<Artist>;
            //var artists = await _artistRepository.GetAll() as List<Artist>;

            var groupModels = new List<GroupModel>();


            foreach (var a in groups)
            {
                string image = await repository.GetFirstAlbumImage(a.Id); // .GetArtistAlbumImage(a.Id);

                int totalAlbums = await repository.GetAlbums(a.Id);
                int totalTracks = await repository.GetTracks(a.Id);
                TimeSpan totalDuration = await repository.GetDuration(a.Id);

                string description = LocalisationHelper.LocaliseDescription(totalAlbums, totalDuration, groupType);

                //  Add to the GroupItems after this.
                var groupmodel = new GroupModel()
                {
                    UniqueId = new GroupId() {
                        Id = a.Id,
                        Type = groupType
                    },
                    Name = a.Name,
                    Description = description
                };
                groupmodel.SetImage(image);
                groupModels.Add(groupmodel);
            }
            return groupModels;
        }






        public Task<GroupModel> LoadGroupAsync(int Id, GroupTypeEnum groupType)
        {
            throw new NotImplementedException();
        }
    }
}
