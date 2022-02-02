using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace MongoDBDemo
{
    public class MongoCRUD
    {
        // creating a connection to db
        private IMongoDatabase db;

        public MongoCRUD(string database)
        {
            // initialise and connect to the database
            var client = new MongoClient(); // connects to localhost server
            db = client.GetDatabase(database);
        }

        // Insert data
        public void InsertRecord<T>(string table, T record)
        {
            var collection = db.GetCollection<T>(table); // get the collection
            collection.InsertOne(record);
        }

        // Reading data
        public List<T> LoadRecord<T>(string table)
        {
            var collection = db.GetCollection<T>(table);
            return collection.Find(new BsonDocument()).ToList();
        }

        // Read by Id
        public T LoadRecordById<T>(string table, Guid id)
        {
            var collection = db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("Id", id); // filter using Id

            return collection.Find(filter).First();
        }

        // Update/Insert depending on what is needed.
        public void UpsertRecord<T>(string table, Guid id, T record)
        {
            var collection = db.GetCollection<T>(table);
            var result = collection.ReplaceOne(
                new BsonDocument("_id", id),
                record,
                new UpdateOptions { IsUpsert = true });
        }

        // Delete a record
        public void DeleteRecord<T>(string table, Guid id)
        {
            var collection = db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("Id", id);
            collection.DeleteOne(filter);
        }            
    }
}
