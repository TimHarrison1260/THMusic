//***************************************************************************************************
//Name of File:     GroupModelDataService.cs
//Description:      The GroupModelDataService provides loading and mapping functionality for the GroupViewModel.
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
using THMusic.Helpers;
using Core.Interfaces;
using Core.Model;

namespace THMusic.Data
{
    /// <summary>
    /// This <c>GroupModelDataService</c> class is responsible for managing all
    /// access to data within the Domain model, and mapping it to the
    /// GroupModel of the Ui, which supports the GroupsViewModel (MainViewModel).
    /// The AlbumsViewModel is part of the MVVM pattern implemented within
    /// the UI, and supported by the MVMLight framework.
    /// </summary>
    public class GroupModelDataService : IGroupModelDataService
    {
        //  Injected instances of the various repositories (Domain model)
        private readonly IArtistRepository _artistRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IPlaylistRepository _playlistRepository;

         /// <summary>
        /// Provides access to the resouce files, Localisation, used
        /// when describing the Group Totals.
        /// </summary>
        private  ResourceLoader _loader = new ResourceLoader();

        /// <summary>
        /// ctor: Initialise the GroupModelDataService
        /// </summary>
        /// <param name="ArtistRepository">Instance of the ArtistRepository</param>
        /// <param name="GenreRepository">Instance of the GenreRepository</param>
        /// <param name="PlaylistRepository">Instance of the PlaylistRepository</param>
        public GroupModelDataService(IArtistRepository ArtistRepository, IGenreRepository GenreRepository, IPlaylistRepository PlaylistRepository)
        {
            if (ArtistRepository == null)
                throw new ArgumentNullException("ArtistRepository", "No valid Repository supplied");
            _artistRepository = ArtistRepository;
            if (GenreRepository == null)
                throw new ArgumentNullException("GenreRepository", "No valid Repository supplied");
            _genreRepository = GenreRepository;
            if (PlaylistRepository == null)
                throw new ArgumentNullException("PlaylistRepository", "No valid Repository supplied");
            _playlistRepository = PlaylistRepository;
        }


        /// <summary>
        /// Gets the Group summary information for all albums in the collection
        /// </summary>
        /// <param name="groupType">The Type of grouping. <see cref="THMusic.DataModel.GroupTypeEnum"/>.</param>
        /// <returns>The collection of Group summaries</returns>
        public async Task<List<GroupModel>> LoadAsync(GroupTypeEnum groupType = GroupTypeEnum.Artist)
        {
            //  Call the repository 
            var groups = new List<GroupModel>();

            switch (groupType)
            {
                case GroupTypeEnum.Artist:
                    {
                        IEnumerable<Artist> artists = await _artistRepository.GetAll();
                        var groupRepository = _artistRepository as IGroupRepository<Artist>;
                        groups = await LoadGroupsAsync<Artist>(artists, groupType, groupRepository);
                        break;
                    }
                case GroupTypeEnum.Genre:
                    {
                        IEnumerable<Genre> genres = await _genreRepository.GetAll();
                        var groupRepository = _genreRepository as IGroupRepository<Genre>;
                        groups = await LoadGroupsAsync<Genre>(genres, groupType, groupRepository);
                        break;
                    }
                case GroupTypeEnum.Playlist:
                    {
                        IEnumerable<PlayList> playlists = await _playlistRepository.GetAll();
                        var groupRepository = _playlistRepository as IGroupRepository<PlayList>;
                        groups = await LoadGroupsAsync<PlayList>(playlists, groupType, groupRepository);
                        break;
                    }
            }
            return groups;
        }

        /// <summary>
        /// Get the Group model for a specific group id
        /// </summary>
        /// <param name="Id">The Id of the Group</param>
        /// <param name="groupType">The type of the group, Artist, Genre or Playlist</param>
        /// <returns>The populated group model</returns>
        public async Task<GroupModel> LoadGroupAsync(int Id, GroupTypeEnum groupType)
        {
            //  Call the repository 
            var groups = new List<GroupModel>();

            switch (groupType)
            {
                case GroupTypeEnum.Artist:
                    {
                        var artists = new List<Artist>();
                        artists.Add(await _artistRepository.GetById(Id));
                        var groupRepository = _artistRepository as IGroupRepository<Artist>;
                        groups = await LoadGroupsAsync<Artist>(artists, groupType, groupRepository);
                        break;
                    }
                case GroupTypeEnum.Genre:
                    {
                        var genres = new List<Genre>();
                        genres.Add(await _genreRepository.GetById(Id));
                        var groupRepository = _genreRepository as IGroupRepository<Genre>;
                        groups = await LoadGroupsAsync<Genre>(genres, groupType, groupRepository);
                        break;
                    }
            }
            return groups[0];
        }


        /// <summary>
        /// Performs the loading of the group, depending on the group type supplied. 
        /// <see cref="THMusic.DataModel.GroupTypeEnum"/>.
        /// </summary>
        /// <typeparam name="T">The generic Type that determines then actual Group</typeparam>
        /// <param name="groups">The groups</param>
        /// <param name="groupType">The type of the groups</param>
        /// <param name="repository">The instance of the GroupRepository</param>
        /// <returns>A collection of the Group Summaries</returns>
        /// <remarks>
        /// This has the repository passed as a parameter because it can be any of
        /// the Artist, Genre or Playlist repositories.  The calling method cast
        /// the specific repository to an instance of the GroupRepository which has
        /// the necessary functionality to support getting the information for any 
        /// of the group types.  
        /// </remarks>
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

    }
}
