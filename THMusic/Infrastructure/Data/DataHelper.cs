//***************************************************************************************************
//Name of File:     DataHelper.cs
//Description:      It copntains the helper methods used to update and reload the Domain Model
//Author:           Tim Harrison
//Date of Creation: Apr/May 2013.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************
using System.Collections.Generic;
using System.Linq;

using Core.Interfaces;
using Core.Model;


namespace Infrastructure.Data
{
    /// <summary>
    /// This <c>DataHelper</c> class provides methods that separate the
    /// operations necessary to add and update entities to the In-Memory 
    /// context
    /// </summary>
    /// <remarks>
    /// It is used by the deserialise process as well as the Import
    /// albums facilities.  It contains many methods that support the 
    /// reloading of the Domain Model as well as the logic required
    /// to completely update the model correctly when adding a new
    /// album.
    /// <para>
    /// This is necessitated by the decision to use an In-Memory domain model
    /// that is simply persisted as an XML file when the application suspencds
    /// of shutsdown, and is reloaded when it starts.
    /// </para>
    /// <para>
    /// The domain model includes navigation properties to make the use
    /// of Linq simpler, however, this drastically complicates the adding
    /// of new albums, because multiple references must be updated and kept
    /// in sync, as well as being added to the other collections, like the 
    /// Tracks and Genres.
    /// </para>
    /// <para>
    /// This might not have been the best decision in hindsight, but it does
    /// remove any requirement on a database or other means of persistance.
    /// I guess I've been getting too used to using Entity Framework which 
    /// does all of this stuff for you automatically.  Entity Framework and
    /// Windows 8 Store App, what's the chances of those two working together?
    /// </para>
    /// </remarks>
    public class DataHelper
    {
        #region Generate Unique id

        /// <summary>
        /// Generate the next Unique Id for an album
        /// </summary>
        /// <returns>New Unique Id</returns>
        public int GenerateAlbumId(IUnitOfWork UnitOfWork)
        {
            var lastIdUsed = UnitOfWork.Albums.Max(a => a.Id);
            return ++lastIdUsed;
        }

        /// <summary>
        /// Generate the next unique Id for a Track
        /// </summary>
        /// <returns>New unique Id</returns>
        public int GenerateTrackId(IUnitOfWork UnitOfWork)
        {
            var lastIdUsed = UnitOfWork.Tracks.Max(a => a.Id);
            return ++lastIdUsed;
        }

        /// <summary>
        /// Generate the next unique Id for an Artist
        /// </summary>
        /// <returns>New unique Id</returns>
        public int GenerateArtistId(IUnitOfWork UnitOfWork)
        {
            var lastIdUsed = UnitOfWork.Artists.Max(a => a.Id);
            return ++lastIdUsed;
        }

        /// <summary>
        /// Generate the next unique Id for a Genre
        /// </summary>
        /// <returns>New unique Id</returns>
        public int GenerateGenreId(IUnitOfWork UnitOfWork)
        {
            var lastIdUsed = UnitOfWork.Genres.Max(a => a.Id);
            return ++lastIdUsed;
        }

        /// <summary>
        /// Generate the next unique Id for a Playlist
        /// </summary>
        /// <returns>New Unique Id</returns>
        public int GeneratePlaylistId(IUnitOfWork UnitOfWork)
        {
            var lastidUsed = UnitOfWork.PlayLists.Max(a => a.Id);
            return ++lastidUsed;
        }


        #endregion


        #region Track update Routines

        /// <summary>
        /// Updates the Track: New Id and Album, and adds to the Tracks Collection
        /// </summary>
        /// <param name="Track">The track to be added</param>
        /// <param name="Album">The album the track belongs to</param>
        /// <returns>The updated track</returns>
        public List<Track> AddTracksToContext(IUnitOfWork UnitOfWork, List<Track> Tracks, Album Album)
        {
            var updatedTracks = new List<Track>();
            foreach (var tr in Tracks)
            {
                //  Generate the Track Id and update the track, if there isn't one already
                if (tr.Id == 0)
                    tr.Id = GenerateTrackId(UnitOfWork);
                //  Add the album reference to the track
                tr.Album = Album;
                tr.AlbumId = Album.Id;

                //  Process any playlist that might exist (none for a new album import).
                if (tr.Playlists.Count > 0)
                {
                    var updatedPlaylists = AddOrUpdatePlaylists(UnitOfWork, tr.Playlists, tr);
                    tr.Playlists = updatedPlaylists;
                }

                //  Add the track to the Tracks collection
                UnitOfWork.Tracks.Add(tr);
                //  Add the updated track to the returned list;
                updatedTracks.Add(tr);
            }
            return updatedTracks;
        }

        #endregion


        #region Genre Update Routines


        /// <summary>
        /// Add the Genres to the In-Memory context.  The complete collection of update
        /// genres is returned to update the Album
        /// </summary>
        /// <param name="UnitOfWork">In-Memory context</param>
        /// <param name="Genres">The genres to be updated</param>
        /// <param name="album">The album reference</param>
        /// <returns>The updated genres collection</returns>
        public List<Genre> AddGenresToContext(IUnitOfWork UnitOfWork, List<Genre> Genres, Album album)
        {
            var updatedGenres = new List<Genre>();
            //  3   Add each genre to the Genres collection
            foreach (var gn in Genres)
            {
                //      3a  If exists, Add album reference to the Genre
                if (GenreExists(UnitOfWork, gn.Name))
                {
                    var updateGenre = UpdateGenre(UnitOfWork, gn, album);
                    updatedGenres.Add(updateGenre);
                }
                else
                {
                    //      3b  If doesn't exist, generate Genre Id
                    var newGenre = AddGenre(UnitOfWork, gn, album);
                    //          iv. Replace Genre to Album.Genres collection
                    updatedGenres.Add(newGenre);
                }
            }
            return updatedGenres;
        }


        /// <summary>
        /// Determines if the Genre exists in the Genres collection, by Name
        /// </summary>
        /// <param name="UnitOfWork">In-Memory context</param>
        /// <param name="GenreName">The Genre Name</param>
        /// <returns>returns True if it is, otherwise False</returns>
        private bool GenreExists(IUnitOfWork UnitOfWork, string GenreName)
        {
            var result = UnitOfWork.Genres.FirstOrDefault(g => g.Name == GenreName);
            return (result != null) ? true : false;
        }

        /// <summary>
        /// Adds a  new Genre to the Genres collection of the Domain model.  It 
        /// returns an instance of the Genre added so that it can be used elsewhere.
        /// This is standard coding for creating a new object.
        /// </summary>
        /// <param name="UnitOfWork">The In-Memory context</param>
        /// <param name="Genre">the new Genre</param>
        /// <param name="Album">The album the Genre relates to</param>
        /// <returns>The updated instance of the new Genre</returns>
        /// <remarks>
        /// Only add an AlbumId if to the AlbumIds collection if it's not already there.
        /// It won't be if the its is a New Album being created, but there will be when 
        /// reloading the persisted data.
        /// </remarks>
        private Genre AddGenre(IUnitOfWork UnitOfWork, Genre Genre, Album Album)
        {
            Genre.Albums = new List<Album>();
            //          i.  Generate GenreId, if there isn't one, will be if reloading persisted data.
            if (Genre.Id == 0)
                Genre.Id = GenerateGenreId(UnitOfWork);
            //          ii. Add album ref to Genre
            AddAlbumToGenre(Genre, Album);
            //          iii.Add Genre to Genres Collection
            UnitOfWork.Genres.Add(Genre);

            return Genre;
        }

        /// <summary>
        /// Updates the Genre in the Genres collection.
        /// </summary>
        /// <param name="UnitOfWork">The In-Memory context</param>
        /// <param name="Genre">The Genre to be updated</param>
        /// <param name="Album">The album</param>
        /// <returns>The Updated Genre</returns>
        /// <remarks>
        /// <para>
        /// The update genre is returned so that it can replace the 
        /// genre in the album, which means the navigation properties
        /// are up to date.
        /// </para>
        /// <para>
        /// Only add an AlbumId if to the AlbumIds collection if it's not already there.
        /// It won't be if the its is a New Album being created, but there will be when 
        /// reloading the persisted data.
        /// </para>
        /// </remarks>
        private Genre UpdateGenre(IUnitOfWork UnitOfWork, Genre Genre, Album Album)
        {
            //  TODO: Refactor this to use the concurrent collection.

            //  Get the index of the Genre
            var idx = UnitOfWork.Genres.FindIndex(g => g.Name == Genre.Name);
            //  Get the Genre
            var updatedGenre = UnitOfWork.Genres[idx];

            //  If Album exists in the retrieved Genre.
            var al = updatedGenre.Albums.FirstOrDefault(a => a.Id == Album.Id);
            if (al != null)
            {
                //  The Album is found, so replace it
                var alIdx = updatedGenre.Albums.IndexOf(al);
                updatedGenre.Albums[alIdx] = Album;
            }
            else
            {
                //  Track dosn't exist in the Playlist.Tracks collection, so just add it.
                AddAlbumToGenre(updatedGenre, Album);
            }

            //  Replace the Genre in the Genres collection
            UnitOfWork.Genres[idx] = updatedGenre;

            //  return the updated Genre
            return UnitOfWork.Genres[idx];
        }


        /// <summary>
        /// Adds the Album and, if necessary, the Album Id to the Genre
        /// </summary>
        /// <param name="Genre">The Genre being updated</param>
        /// <param name="Album">The album updating the Genre</param>
        /// <remarks>
        /// <para>
        /// Only add an AlbumId if to the AlbumIds collection if it's not already there.
        /// It won't be if the its is a New Album being created, but there will be when 
        /// reloading the persisted data.
        /// </para>
        /// </remarks>
        private void AddAlbumToGenre(Genre Genre, Album Album)
        {
            //  Add the Album to the albums collection
            Genre.Albums.Add(Album);

            //  Only add an AlbumId if to the AlbumIds collection if it's not already there.
            //  It won't be if the its is a New Album being created,
            //  but there will be when reloading the persisted data.
            if (!Genre.AlbumIds.Contains(Album.Id))
                Genre.AlbumIds.Add(Album.Id);
        }

        #endregion


        #region Artist Update routines

        /// <summary>
        /// Add the Artist to the In-Memory context.  The updated 
        /// Artist is returned to update the Album
        /// </summary>
        /// <param name="UnitOfWork">In-Memory context</param>
        /// <param name="Artist">The Artist to be updated</param>
        /// <param name="album">The album reference</param>
        /// <returns>The updated Artist</returns>
        /// <remarks>
        /// <para>
        /// Only add an AlbumId if to the AlbumIds collection if it's not already there.
        /// It won't be if the its is a New Album being created, but there will be when 
        /// reloading the persisted data.
        /// </para>
        /// </remarks>
        public Artist AddArtistToContext(IUnitOfWork UnitOfWork, Artist Artist, Album Album)
        {
            //      4a  If exists, Add album reference to the Artist
            if (ArtistExists(UnitOfWork, Artist.Name))
            {
                var updatedArtist = UpdateArtist(UnitOfWork, Artist, Album);
                return updatedArtist;
            }
            else
            {
                //      4b  If doesn't exist, generate Artist Id
                var newArtist = AddArtist(UnitOfWork, Artist, Album);
                //          iv. Replace Artist to Album.Artist
                return Artist;
            }
        }


        /// <summary>
        /// Determines if the Artist exists in the Artists collection, by Name
        /// </summary>
        /// <param name="UnitOfWork">In-Memory context</param>
        /// <param name="ArtistName">The Artist name</param>
        /// <returns>returns True if it is, otherwise False</returns>
        private bool ArtistExists(IUnitOfWork UnitOfWork, string ArtistName)
        {
            var result = UnitOfWork.Artists.FirstOrDefault(a => a.Name == ArtistName);
            return (result != null) ? true : false;
        }

        /// <summary>
        /// Adds a new Artist to the Artists collection and updates the albums referenced
        /// </summary>
        /// <param name="UnitOfWork">In-Memory context</param>
        /// <param name="Artist">the New artist</param>
        /// <param name="Album">The Album it relates to</param>
        /// <returns>The New Artist</returns>
        /// <remarks>
        /// <para>
        /// Only add an AlbumId if to the AlbumIds collection if it's not already there.
        /// It won't be if the its is a New Album being created, but there will be when 
        /// reloading the persisted data.
        /// </para>
        /// </remarks>
        private Artist AddArtist(IUnitOfWork UnitOfWork, Artist Artist, Album Album)
        {
            Artist.Albums = new List<Album>();

            //          i.  Generate ArtistId, if there isn'[t one already, will be reloading persisted data.
            if (Artist.Id == 0)
                Artist.Id = GenerateArtistId(UnitOfWork);
            //          ii. Add album ref to Artist
            AddAlbumToArtist(Artist, Album);
            //          iii.Add Artist to Artists Collection
            UnitOfWork.Artists.Add(Artist);

            return Artist;
        }


        /// <summary>
        /// Updates the Artist in the Artists collection.
        /// </summary>
        /// <param name="UnitOfWork">The In-Memory context</param>
        /// <param name="Artist">The artist to be updated</param>
        /// <param name="Album">The album</param>
        /// <returns>The Updated Artist</returns>
        /// <remarks>
        /// The update artist is returned so that it can replace the 
        /// Artist in the album, which means the navigation properties
        /// are up to date.
        /// </remarks>
        private Artist UpdateArtist(IUnitOfWork UnitOfWork, Artist Artist, Album Album)
        {
            //  TODO: Refactor this to use the concurrent collection.

            //  Get the index of the Artist
            var idx = UnitOfWork.Artists.FindIndex(a => a.Name == Artist.Name);
            //  Get the Artist
            var updatedArtist = UnitOfWork.Artists[idx];

            //  If Album exists in the retrieved Genre.
            var al = updatedArtist.Albums.FirstOrDefault(a => a.Id == Album.Id);
            if (al != null)
            {
                //  The Album is found, so replace it
                var alIdx = updatedArtist.Albums.IndexOf(al);
                updatedArtist.Albums[alIdx] = Album;
            }
            else
            {
                //  Track dosn't exist in the Playlist.Tracks collection, so just add it.
                AddAlbumToArtist(updatedArtist, Album);
            }

            //  Replace the Artist in the Genres collection
            UnitOfWork.Artists[idx] = updatedArtist;
            //  Replace the Artist back in the collection

            //  return the updated Artist
            return updatedArtist;
        }

        /// <summary>
        /// Add the album to the Artist, and if necessary, to the AlbumIds as well.
        /// </summary>
        /// <param name="Artist">The Artist</param>
        /// <param name="Album">The album being added</param>
        /// <remarks>
        /// Only add an AlbumId if to the AlbumIds collection if it's not already there.
        /// It won't be if the its is a New Album being created, but there will be when 
        /// reloading the persisted data.
        /// </remarks>
        private void AddAlbumToArtist(Artist Artist, Album Album)
        {
            //  Add the Album to the albums collection
            Artist.Albums.Add(Album);

            //  Only add an AlbumId if to the AlbumIds collection if it's not already there.
            //  It won't be if the its is a New Album being created,
            //  but there will be when reloading the persisted data.
            if (!Artist.AlbumIds.Contains(Album.Id))
                Artist.AlbumIds.Add(Album.Id);
        }


        #endregion


        #region Playlist UPdate Routines

        /// <summary>
        /// Adds or Updates the playlist within the Playlist collection of the domain model.  
        /// It returns a collection of updated playlists, for updating within the Tracks, so 
        /// that the navigation properties are fully updated.
        /// </summary>
        /// <param name="UnitOfWork">In-Memory Context</param>
        /// <param name="Playlists">The Playlists being updated</param>
        /// <param name="Track">The Track belonging to the playlists</param>
        /// <returns>Update playlists collection</returns>
        /// <remarks>
        /// Only add an AlbumId if to the AlbumIds collection if it's not already there.
        /// It won't be if the its is a New Album being created, but there will be when 
        /// reloading the persisted data.
        /// </remarks>
        private List<PlayList> AddOrUpdatePlaylists(IUnitOfWork UnitOfWork, List<PlayList> Playlists, Track Track)
        {
            var updatedPlaylists = new List<PlayList>();

            foreach (var p in Playlists)
            {
                if (PlaylistExists(UnitOfWork, p.Id))
                {
                    var updatedPlaylist = UpdatePlaylist(UnitOfWork, p, Track);
                    updatedPlaylists.Add(updatedPlaylist);
                }
                else
                {
                    var newPlaylist = AddPlaylist(UnitOfWork, p, Track);
                    updatedPlaylists.Add(newPlaylist);
                }
            }
            return updatedPlaylists;
        }

        /// <summary>
        /// Determines if the Playlist exists in the Playlists collection, by Id
        /// </summary>
        /// <param name="UnitOfWork">In-Memory context</param>
        /// <param name="PlaylistId">The Id of the playlist</param>
        /// <returns>returns True if it is, otherwise False</returns>
        private bool PlaylistExists(IUnitOfWork UnitOfWork, int PlaylistId)
        {
            var result = UnitOfWork.PlayLists.FirstOrDefault(p => p.Id == PlaylistId);
            return (result != null) ? true : false;
        }

        /// <summary>
        /// Adds a new playlist to the Playlists collection of the domain model.  It also
        /// return the new playlist, fully updated.
        /// </summary>
        /// <param name="UnitOfWork">In-Memory Context</param>
        /// <param name="Playlist">The playlist being added</param>
        /// <param name="Track">The track associated with the playlist</param>
        /// <returns>The new playlist.</returns>
        /// <remarks>
        /// Only add an AlbumId if to the AlbumIds collection if it's not already there.
        /// It won't be if the its is a New Album being created, but there will be when 
        /// reloading the persisted data.
        /// </remarks>
        private PlayList AddPlaylist(IUnitOfWork UnitOfWork, PlayList Playlist, Track Track)
        {
            //          i.  Generate PlaylistId, if not one already (will be reloading persisted data).
            Playlist.Tracks = new List<Track>();

            if (Playlist.Id == 0)
                Playlist.Id = GeneratePlaylistId(UnitOfWork);
            //          ii. Add Track reference to the Playlist
            AddTrackToPlaylist(Playlist, Track);
            //          iii.Add Playlist to Pl;aylists Collection
            UnitOfWork.PlayLists.Add(Playlist);

            return Playlist;
        }

        /// <summary>
        /// Updates an existling playlist, but adding the tracks to it.
        /// </summary>
        /// <param name="UnitOfWork">In-Memory context</param>
        /// <param name="Playlist">The playlist being updated</param>
        /// <param name="Track">The track associated with the playlist</param>
        /// <returns>The updated playlist.</returns>
        /// <remarks>
        /// Only add an AlbumId if to the AlbumIds collection if it's not already there.
        /// It won't be if the its is a New Album being created, but there will be when 
        /// reloading the persisted data.
        /// </remarks>
        private PlayList UpdatePlaylist(IUnitOfWork UnitOfWork, PlayList Playlist, Track Track)
        {
            //  TODO: Refactor UpdatePlaylist to use the concurrent collection.

            //  Get the index of the Playlist
            var idx = UnitOfWork.PlayLists.FindIndex(p => p.Id == Playlist.Id);
            //  Get the Playlist
            var updatedPlaylist = UnitOfWork.PlayLists[idx];

            //  If track exists in the playlist retrieved playlist.
            var tr = updatedPlaylist.Tracks.FirstOrDefault(a => a.Id == Track.Id);
            if (tr != null)
            {
                //  The track is found, so replace
                var trIdx = updatedPlaylist.Tracks.IndexOf(tr);
                updatedPlaylist.Tracks[trIdx] = Track;
            }
            else
            {
                //  Track dosn't exist in the Playlist.Tracks collection, so just add it.
                AddTrackToPlaylist(updatedPlaylist, Track);
            }

            //  Replace the Playlist in the context
            UnitOfWork.PlayLists[idx] = updatedPlaylist;

            //  return the updated Genre
            return updatedPlaylist;
        }

        /// <summary>
        /// Adds the track referenced to the playlist Tracks colleciton.  It 
        /// optionally adds it to the TrackIs collection too.
        /// </summary>
        /// <param name="Playlist">The playlist being updated</param>
        /// <param name="Track">The Track being added.</param>
        /// <remarks>
        /// Only add an AlbumId if to the AlbumIds collection if it's not already there.
        /// It won't be if the its is a New Album being created, but there will be when 
        /// reloading the persisted data.
        /// </remarks>
        private void AddTrackToPlaylist(PlayList Playlist, Track Track)
        {
            //  Add the Track to the Tracks collection
            Playlist.Tracks.Add(Track);

            //  Only add a TrackId if to the TrackIds collection if it's not already there.
            //  It won't be if the its is a New Album being created,
            //  but there will be when reloading the persistent data.
            if (!Playlist.TrackIds.Contains(Track.Id))
                Playlist.TrackIds.Add(Track.Id);
        }



        #endregion

    }
}
