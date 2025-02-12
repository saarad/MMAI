using AutoMapper.Configuration.Annotations;

namespace graphQLService.DTOs.GraphQLTypes
{
    public class MatchHistoryType
    {
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
            public string? StrikesRed { get; set; }
            public string? StrikesBlue { get; set; }
            public string? TotalRed { get; set; }
            public string? TotalBlue { get; set; }
            public string? TdRed { get; set; }
            public string? TdBlue { get; set; }
            public string? SubRed { get; set; }
            public string? SubBlue { get; set; }
            public string? PassRed { get; set; }
            public string? PassBlue { get; set; }
            public string? RevRed { get; set; }
            public string? RevBlue { get; set; }
            public string? HeadRed { get; set; }
            public string? HeadBlue { get; set; }
            public string? BodyRed { get; set; }
            public string? BodyBlue { get; set; }
            public string? LegRed { get; set; }
            public string? LegBlue { get; set; }
            public string? DistanceRed { get; set; }
            public string? DistanceBlue { get; set; }
            public string? ClinchRed { get; set; }
            public string? ClinchBlue { get; set; }
            public string? GroundRed { get; set; }
            public string? GroundBlue { get; set; }
            public string? Winner { get; set; }
            public string? RedImage { get; set; }
            public string? BlueImage { get; set; }
            public string? Date { get; set; }
    }
}
