using MongoDB.Bson;


namespace dataAccess.Models
{
   public class UpcomingFight
    {
        public BsonObjectId _id;
        public string? Event { get; set; }
        public string? Date { get; set; }
        public string? Location { get; set; }
        public string? Name1 { get; set; }
        public string? Name2 { get; set; }
        public string? WeightClass { get; set; }
        public string? RedImageString { get; set; }
        public string? BlueImageString { get; set; }
        public string?[] BlueImage { get; set; }
        public string?[] RedImage { get; set; }
    }
}
