﻿using Domain.Entity;
using MongoDB.Driver;

namespace Infra.Persistence
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<T> GetCollection<T>()
        {
            return _database.GetCollection<T>(typeof(T).Name); // Nome da coleção é o nome da classe
        }

        public IMongoCollection<Workspace> Workspaces => _database.GetCollection<Workspace>("Workspaces");
        public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
        // Adicione outras coleções conforme necessário
    }
}
