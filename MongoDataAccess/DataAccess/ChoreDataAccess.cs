using MongoDataAccess.Models;
using MongoDB.Driver;

namespace MongoDataAccess.DataAccess;

public class ChoreDataAccess
{
    // declaring connectionstring, dbname, collection names etc
    private const string ConnectionString = "mongodb://localhost:27017";
    private const string DatabaseName = "choredb";
    private const string UserCollection = "users";
    private const string ChoreCollection = "chore_chart";
    private const string ChoreHistoryCollection = "chore_history";

    // Method for getting back collection
    private IMongoCollection<T> ConnectToMongo<T>(in string collection)
    {
        var client = new MongoClient(ConnectionString);
        var db = client.GetDatabase(DatabaseName);
        return db.GetCollection<T>(collection);
    }

    // Read all users
    public async Task<List<UserModel>> GetAllUsers()
    {
        var userCollection = ConnectToMongo<UserModel>(UserCollection);
        var results = await userCollection.FindAsync(_ => true);

        return results.ToList();
    }

    // Read all chores
    public async Task<List<ChoreModel>> GetAllChores()
    {
        var choreCollection = ConnectToMongo<ChoreModel>(ChoreCollection);
        var results = await choreCollection.FindAsync(_ => true);

        return results.ToList();
    }

    // Read all chores of a particular user
    public async Task<List<ChoreModel>> GetAllChoresForAUser(UserModel user)
    {
        var choreCollection = ConnectToMongo<ChoreModel>(ChoreCollection);
        var results = await choreCollection.FindAsync(c => c.AssignedTo.Id == user.Id);

        return results.ToList();
    }

    // Create a user
    public Task CreateUser(UserModel user)
    {
        var userCollection = ConnectToMongo<UserModel>(UserCollection);
        return userCollection.InsertOneAsync(user);
    }

    // Create a chore
    public Task CreateChore(ChoreModel chore)
    {
        var choreCollection = ConnectToMongo<ChoreModel>(ChoreCollection);
        return choreCollection.InsertOneAsync(chore);
    }

    // Update a chore
    public Task UpdateChore(ChoreModel chore)
    {
        var choreCollection = ConnectToMongo<ChoreModel>(ChoreCollection);
        var filter = Builders<ChoreModel>.Filter.Eq("Id", chore.Id);
        return choreCollection.ReplaceOneAsync(filter, chore, new ReplaceOptions { IsUpsert = true });
    }

    // Delete a chore
    public Task DeleteChore(ChoreModel chore)
    {
        var choreCollection = ConnectToMongo<ChoreModel>(ChoreCollection);
        return choreCollection.DeleteOneAsync(c => c.Id == chore.Id);
    }
}
