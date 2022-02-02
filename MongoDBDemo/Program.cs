using System;

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
            var recs = db.LoadRecord<PersonModel>("Users");

            foreach (var rec in recs)
            {
                Console.WriteLine($"{ rec.Id}: {rec.FirstName} {rec.LastName}");

                if (rec.PrimaryAddress != null)
                {
                    Console.WriteLine(rec.PrimaryAddress.City);
                }
            }

            //var recs = db.LoadRecord<NameModel>("Users");

            //foreach (var rec in recs)
            //{
            //    Console.WriteLine($"{rec.FirstName} {rec.LastName}");
            //}

            //var oneRec = db.LoadRecordById<PersonModel>("Users", new Guid("c9ae7069-e11c-4d2b-b980-bd0e0c643616"));

            //oneRec.DateOfBirth = new DateTime(1982, 10, 31, 0, 0, 0, DateTimeKind.Utc);
            //db.UpsertRecord("Users", oneRec.Id, oneRec);
            //db.DeleteRecord<PersonModel>("Users", oneRec.Id);
            Console.ReadLine();
        }
    }
}
