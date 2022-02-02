using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MongoDBDemo
{
    public class PersonModel
    {
        // This is the unique Id of the record/document (_id)
        // MongoDB generates the Id
        [BsonId]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public AddressModel PrimaryAddress { get; set; }
        [BsonElement("dob")] // Name in database will be dob
        public DateTime DateOfBirth { get; set; }
    }
}
