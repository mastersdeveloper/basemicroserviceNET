﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BASE.MICRONET.History.Models
{
    public class HistoryTransaction
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public int IdTransaction { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; }
        public string CreationDate { get; set; }
        public int AccountId { get; set; }
    }
}
