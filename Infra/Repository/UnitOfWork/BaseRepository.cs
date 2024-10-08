using Infra.Persistence;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Infra.Repository.UnitOfWork
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly MongoDbContext _context;

        public BaseRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<T> Create(T command)
        {
            await _context.GetCollection<T>().InsertOneAsync(command);
            return command;
        }

        public Task Delete(T entity)
        {
            return _context.GetCollection<T>().DeleteOneAsync(Builders<T>.Filter.Eq("Id", entity.GetType().GetProperty("Id")?.GetValue(entity)));
        }

        public Task DeleteRange(List<T> range)
        {
            var ids = range.Select(entity => entity.GetType().GetProperty("Id")?.GetValue(entity)).ToList();
            return _context.GetCollection<T>().DeleteManyAsync(Builders<T>.Filter.In("Id", ids));
        }

        public async Task<T?> Get(Expression<Func<T, bool>> expression)
        {
            return await _context.GetCollection<T>().Find(expression).FirstOrDefaultAsync();
        }

        public IEnumerable<T> GetAll()
        {
            return _context.GetCollection<T>().Find(_ => true).ToList();
        }

        public async Task<T> Update(T commandUpdate)
        {
            var id = commandUpdate.GetType().GetProperty("Id")?.GetValue(commandUpdate);
            await _context.GetCollection<T>().ReplaceOneAsync(Builders<T>.Filter.Eq("Id", id), commandUpdate);
            return commandUpdate;
        }
    }
}
