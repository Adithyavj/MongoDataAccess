//using MongoDB.Driver;
//using ConsoleUI;

//// Adding DB connection..
//string connectionString = "mongodb://localhost:27017";
//string databaseName = "simple_db";
//string collectionName = "people";

//var client = new MongoClient(connectionString); // connection to MongoDB
//var db = client.GetDatabase(databaseName); // connection to our Database in MongoDB
//var collection = db.GetCollection<PersonModel>(collectionName); // Getting value from collection in the Database

//var person = new PersonModel { FirstName = "Tim", LastName = "Corey" };

//await collection.InsertOneAsync(person); // insert values to db

//var results = await collection.FindAsync(_ => true);

//foreach (var result in results.ToList())
//{
//    Console.WriteLine($"{ result.Id}: {result.FirstName} {result.LastName}");
//}

using MongoDataAccess.DataAccess;
using MongoDataAccess.Models;

ChoreDataAccess db = new ChoreDataAccess();

await db.CreateUser(new UserModel { FirstName = "Tim", LastName = "Corey" });

var users = await db.GetAllUsers();

var chore = new ChoreModel 
{ 
    AssignedTo = users.First(), 
    ChoreText = "Mow the lawn", 
    FrequencyInDays = 7 
};

await db.CreateChore(chore);

var chores = await db.GetAllChores();

var newChore = chores.First();
newChore.LastCompleted = DateTime.UtcNow;

await db.CompleteChore(newChore);