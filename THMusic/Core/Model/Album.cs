//***************************************************************************************************
//Name of File:     Album.cs
//Description:      The Album class of the domain model: it is an abstract class.
//Author:           Tim Harrison
//Date of Creation: Apr/May 2013.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************

using System;
using System.Linq;
using System.Collections.Generic;

using System.Xml.Serialization;

namespace Core.Model
{
    /// <summary>
    /// This <c>Album</c> class describes the information held for an album 
    /// in the Domain model.  It is an abstract class to aid separation of
    /// concerns thoughout the application.  Instances are created using 
    /// the <see cref="Core.Factories.AlbumFactory"/> Create() method.
    /// </summary>
    /// <remarks>
    /// <para>
    /// It is decorated with the XmlInclude() attribute so that the the concrete implementation
    /// is persisted.
    /// </para>
    /// </remarks>
    [XmlInclude(typeof(ConcreteClasses.ConcreteAlbum))]
    public abstract class Album 
    {
        /// <summary>
        /// Gets or sets the unique Id of the Album
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Gets or Sets the Title of the album
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Gets or sets the Artist responsible for the Album
        /// </summary>
        public Artist Artist { get; set; }
        /// <summary>
        /// Gets or sets the release data of the album
        /// </summary>
        public DateTime Released { get; set; }
        /// <summary>
        /// Gets or sets the collection of tracks that belong to the Album
        /// </summary>
        public List<Track> Tracks { get; set; }
        /// <summary>
        /// Gets or sets the LastFM URL that points to the Album information
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// Gets or sets the LastFM mbid field that identifies the Album
        /// </summary>
        /// <remarks>
        /// This can be used to search for LastFM information 
        /// instead of the albumand Artits name.
        /// </remarks>
        public string Mbid { get; set; }
        /// <summary>
        /// Gets or sets the collection of album cover images of various sizes.
        /// </summary>
        public List<Image> Images { get; set; }
        /// <summary>
        /// Gets or sets the wiki entry related to the Album
        /// </summary>
        public Wiki Wiki { get; set; }
        /// <summary>
        /// Gets or sets a collection of Genres categorizing the Album content
        /// </summary>
        public List<Genre> Genres { get; set; }

        /// <summary>
        /// Updates or adds the Image for this instance of Album
        /// </summary>
        /// <param name="size">The siae of the image being added or updated</param>
        /// <param name="Url">The url to the image being added or updated</param>
        public void UpdateImage(string size, string Url)
        {
            var imageFactory = new Factories.ImageFactory();
            var img = imageFactory.Create();
            img.Size = (ImageSizeEnum)Enum.Parse(typeof(ImageSizeEnum), size);
            img.Url = Url;
            UpdateImage(img);
        }

        /// <summary>
        /// Updates or adds the Image for this instance of Album
        /// </summary>
        /// <param name="Image">The Image to be addd or updated</param>
        public void UpdateImage(Image Image)
        {
            //  See if the image is there already
            var img = this.Images.FirstOrDefault(i => i.Size == Image.Size);
            if (img != null)
            {
                //  yes => update it
                var idx = this.Images.IndexOf(img);
                this.Images[idx].Url = Image.Url;
            }
            else
            {
                //  no => add it
                this.Images.Add(Image);
            }
        }


        /// <summary>
        /// Makes a Shallow clone of this Album.
        /// <para>
        /// It creates a new instance, but copies the references only for the
        /// child classes, all except the Tracks.  The tracks are included as
        /// a new initilised List, with no actual tracks in it.
        /// </para>
        /// <para>
        /// This is to allow for the tracks for the album that belong to a 
        /// Playlist to be modified, without modifying the domain model instance
        /// itself.  It is only the tracks that must be changed, so the other
        /// child classes are safe to be a reference copy only.
        /// </para>
        /// </summary>
        /// <returns>A shallow copy of this instance of Album</returns>
        public Album Clone()
        {
            //  Shallow copy with no Tracks.
            var clonedAlbum = new ConcreteClasses.ConcreteAlbum();
            clonedAlbum.Id = this.Id;
            clonedAlbum.Title = this.Title;
            clonedAlbum.Released = this.Released;
            clonedAlbum.Url = this.Url;
            clonedAlbum.Mbid = this.Mbid;
            clonedAlbum.Tracks = new List<Track>();
            clonedAlbum.Images = new List<Image>();
            clonedAlbum.Images = this.Images;
            clonedAlbum.Artist = new ConcreteClasses.ConcreteArtist();
            clonedAlbum.Artist = this.Artist;
            clonedAlbum.Wiki = new ConcreteClasses.ConcreteWiki();
            clonedAlbum.Wiki = this.Wiki;
            clonedAlbum.Genres = new List<Genre>();
            clonedAlbum.Genres = this.Genres;
            return clonedAlbum;
        }

    }    
}
