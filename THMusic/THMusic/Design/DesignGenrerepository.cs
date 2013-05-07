using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace THMusic.Design
{
    public class DesignGenrerepository : Core.Interfaces.IGenreRepository
    {

        public Task<IEnumerable<Core.Model.Genre>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<string> GetFirstAlbumImage(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetAlbums(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetTracks(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<TimeSpan> GetDuration(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<Core.Model.Genre> GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<Core.Model.Genre> GetByName(string Name)
        {
            throw new NotImplementedException();
        }
    }
}
