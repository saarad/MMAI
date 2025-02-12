using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;


namespace dataAccess.Models
{
    public class DataUpdated
    {
        public BsonObjectId _id;
        public string? Date { get; set; }
    }
}
