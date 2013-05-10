
namespace Infrastructure.Data
{
    /// <summary>
    /// The Namespace <c>Infrastructure.Data</c> is part of the <c>Infrastructure</c> project and
    /// contains the classes responsible for accessing
    /// and furnishing the data to the business model.  The contents of each class are loaded
    /// at application startup and hold all data in memory until the application closes.
    /// The <c>MusicCollection</c> class holds the information relating to the Albums and associated
    /// information.  
    /// <para>
    /// Concurrency
    /// </para>
    /// <para>
    /// Within this namespace the routines must be made thread-safe as they have the sole responsibility
    /// for maintaining the data within the In-Memory context of the <see cref="Infrastructure.Data.MusicCollection"/>
    /// class.  
    /// </para>
    /// <para>
    /// The routines within the collection and the supporting <see cref="Infrastructure.Data.DataHelper"/>
    /// class are the only places within the application where such updates can occur.  Individual 
    /// monitoring locks are provided to ensure that only one instance the relevant routine can update
    /// any of the collections contained within the Music Collection itself.
    /// </para>
    /// <para>
    /// Considerations for adopting the monitoring lock for concurrency include the concurrent collections
    /// introduce in the .Net Framework 4.0.  These provide good support for multi-threaded applications
    /// but are geared to be able to cope with ones of a much larger applications.  That's not to say
    /// they shouldn't work in small apps, they will.
    /// </para>
    /// <para>
    /// However, the ConcurrentBag(T), ConcurrentQueue(T) and ConcurrentStack(T) collections do not 
    /// allow for specific items to be retrieved, so they are not considered here.  the ConcurrentDictionary(T)
    /// does allow for specific retrieval, however the application would require significant change
    /// and regression testing to facilitate using the ConcurrentDictionary.  therefore the use of manual
    /// locking was chosen, specifically the monitoring lock as this is a simple way to address 
    /// concurrency issues and is appropriate here since the application is single user, and the chances 
    /// of much in the way of parallel processing and conflicting updates is negligable.
    /// </para>
    /// </summary>
    /// <remarks>
    /// <para>
    /// The <c>Infrastructure</c> namespace contains all elements of the
    /// Application design model, referred to as the 'Onion Model', considered
    /// to be part of the application dealing with cross cutting concerns.
    /// see <a href="http://jeffreypalermo.com/blog/the-onion-architecture-part-1/.">jeffreypalermo.com.</a>
    /// 
    /// This is a separate project within the Application, which therefore holds
    /// all objects in a separate assembly, thus improving the Separation of 
    /// Concerns required for reusable and unit testable code.
    /// </para>
    /// </remarks>
    [System.Runtime.CompilerServices.CompilerGenerated()]
    class NamespaceDoc
    {
    }
}
