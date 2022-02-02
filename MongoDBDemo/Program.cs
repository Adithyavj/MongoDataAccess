using MongoDB.Driver;
using System;

namespace MongoDBDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            MongoCRUD db = new MongoCRUD("AddressBook");
            Console.ReadLine();
        }
    }

    public class MongoCRUD
    {
        // creating a connection to db
        private IMongoDatabase db;

        public MongoCRUD(string database)
        {
            // initialise and connect to the database
            var client = new MongoClient();
            db = client.GetDatabase(database);
        }
    }
}
