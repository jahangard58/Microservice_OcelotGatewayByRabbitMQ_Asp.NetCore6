using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsAPI
{
    public  class ContextMongoDB
    {
        public static string connectionString = "mongodb://localhost:27017";
        protected MongoClient _client { get; set; }
        protected IMongoDatabase _db { get; set; }

        public ContextMongoDB()
        {
            _client = new MongoClient(connectionString);
            _db = _client.GetDatabase($"ProductDB");
        }
    }
}
