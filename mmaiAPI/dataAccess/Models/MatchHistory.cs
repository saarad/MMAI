
using MongoDB.Bson;

namespace dataAccess.Models
{
    public class MatchHistory
    {
        public BsonObjectId _id;
        public string? EventName { get; set; }
        public string? RedName { get; set; }
        public string? RedResult { get; set; }
        public string? BlueName { get; set; }
        public string? BlueResult { get; set; }
        public string? WeightClass { get; set; }
        public string? Method { get; set; }
        public string? Round { get; set; }
        public string? Time { get; set; }
        public string? TimeFormat { get; set; }
        public string? Referee { get; set; }
        public string? Details { get; set; }
        public string? SigStrikesRed { get; set; }
        public string? SigStrikesBlue { get; set; }
        public string? TotalStrikesRed { get; set; }
        public string? TotalStrikesBlue { get; set; }
        public string? TakeDownRed { get; set; }
        public string? TakeDownBlue { get; set; }
        public string? SubAttemptRed { get; set; }
        public string? SubAttemptBlue { get; set; }
        public string? PassRed { get; set; }
        public string? PassBlue { get; set; }
        public string? RevRed { get; set; }
        public string? RevBlue { get; set; }
        public string? SigHeadRed { get; set; }
        public string? SigHeadBlue { get; set; }
        public string? SigBodyRed { get; set; }
        public string? SigBodyBlue { get; set; }
        public string? SigLegRed { get; set; }
        public string? SigLegBlue { get; set; }
        public string? SigDistanceRed { get; set; }
        public string? SigDistanceBlue { get; set; }
        public string? ClinchRed { get; set; }
        public string? ClinchBlue { get; set; }
        public string? GroundRed { get; set; }
        public string? GroundBlue { get; set; }
        public string? Winner { get; set; }
        public string?[] RedImage { get; set; }
        public string?[] BlueImage { get; set; }
        public string? RedImageString { get; set; }
        public string? BlueImageString { get; set; }
        public string? Date { get; set; }

        public MatchHistory()
        {
            Winner = "";
        }
    }
}
