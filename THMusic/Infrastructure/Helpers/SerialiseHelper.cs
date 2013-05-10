//***************************************************************************************************
//Name of File:     SerialiseHelper.cs
//Description:      Serialises/Deserialises the persistence XML data file.
//Author:           Tim Harrison
//Date of Creation: Apr / May 2013.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Windows.Storage;
using System.Xml.Serialization;

using Core.Interfaces;
using Core.Model;

namespace Infrastructure.Helpers
{
    /// <summary>
    /// Static class <c>DeserialiseData</c> is responsible for converting
    /// the persisted XML file, containing the application data.
    /// </summary>
    /// <remarks>
    /// </remarks>
    public static class SerialiseHelper
    {
        /// <summary>
        /// Deserialises the XML file contents and loads the instance of the Im-Memory Context
        /// It is called from the <see cref="Infrastructure.Repositories.PersistenceRepository"/>.
        /// </summary>
        /// <param name="UnitOfWork">The instance of the Music Collection being loaded</param>
        /// <param name="datafile">The instance of the XML source file.</param>
        /// <returns>A Task so that the call can be awaited.</returns>
        /// <remarks>
        /// The UnitOfWork which represents the Music Collection is passed by reference so updats
        /// reflect in the supplied instance.  This instance is injected by the IoC container
        /// therefore no new instances of it should be created.
        /// </remarks>
        public static async Task Deserialise(IUnitOfWork UnitOfWork, StorageFile datafile)
        {
            //  Deserialise the data from the file.
            using (StreamReader reader = new StreamReader(await datafile.OpenStreamForReadAsync()))
            {
                //  Check for an empty stream, exceptions will occur if it is.
                if (!reader.EndOfStream)
                {
                    //  Set up the types for deserialising
                    Type[] extraTypes = new Type[11];
                    extraTypes[0] = typeof(List<Album>);
                    extraTypes[1] = typeof(List<Track>);
                    extraTypes[2] = typeof(List<Artist>);
                    extraTypes[3] = typeof(List<Genre>);
                    extraTypes[4] = typeof(List<PlayList>);
                    extraTypes[5] = typeof(Track);
                    extraTypes[6] = typeof(Artist);
                    extraTypes[7] = typeof(Genre);
                    extraTypes[8] = typeof(PlayList);
                    extraTypes[9] = typeof(Wiki);
                    extraTypes[10] = typeof(Image);

                    //  Create the XMLSerialiser instance
                    XmlSerializer serializer = new XmlSerializer(typeof(List<Album>), extraTypes);
                    //  Deserialise the Albums collection, that's the only collection persisted as 
                    //  it contains the complete object graph.

                    UnitOfWork.Albums = (List<Album>)serializer.Deserialize(reader);
                }
            }
        }

        /// <summary>
        /// Serialises the data from the In-Memory context, represented by the 
        /// UnitOfWork, to the underlying XML file.
        /// </summary>
        /// <param name="UnitOfWork">The instance of the Music Collection being persisted</param>
        /// <param name="dataFile">The instance of the XML source file</param>
        /// <returns>A Task so that the call runs asynchronously</returns>
        public static async Task Serialise(IUnitOfWork UnitOfWork, StorageFile dataFile)
        {
            //  Now serialise the MusicCollection Albums only as XML and write to the file.
            using (StreamWriter writer = new StreamWriter(await dataFile.OpenStreamForWriteAsync()))
            {
                XmlSerializer serialiser = new XmlSerializer(typeof(List<Album>));
                serialiser.Serialize(writer, UnitOfWork.Albums);
            }
        }

    }
}
