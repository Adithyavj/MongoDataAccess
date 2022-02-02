using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace MongoDBDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            MongoCRUD db = new MongoCRUD("AddressBook");

            //PersonModel person = new PersonModel()
            //{
            //    FirstName = "Joe",
            //    LastName = "Smith",
            //    PrimaryAddress = new AddressModel
            //    {
            //        StreetAddress = "101 Oak Street",
            //        City = "Scranton",
            //        State = "PA",
            //        ZipCode = "18512"
            //    }
            //};
            //db.InsertRecord("Users", person);
            //var recs = db.LoadRecord<PersonModel>("Users");

            //foreach (var rec in recs)
            //{
            //    Console.WriteLine($"{ rec.Id}: {rec.FirstName} {rec.LastName}");

            //    if (rec.PrimaryAddress != null)
            //    {
            //        Console.WriteLine(rec.PrimaryAddress.City);
            //    }
            //}

            var oneRec = db.LoadRecordById<PersonModel>("Users", new Guid("c9ae7069-e11c-4d2b-b980-bd0e0c643616"));
            
            Console.ReadLine();
        }
    }

    public class PersonModel
    {
        // This is the unique Id of the record/document (_id)
        // MongoDB generates the Id
        [BsonId]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public AddressModel PrimaryAddress { get; set; }
    }

    public class AddressModel
    {
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }

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
        [Obsolete]
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
