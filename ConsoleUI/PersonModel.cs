using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ConsoleUI;

public class PersonModel
{
    // Data is stored in MongoDB as Bson (Binary Javascript Object Notation)
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
