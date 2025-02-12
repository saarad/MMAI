namespace graphQLService.DTOs.GraphQLTypes
{
    public class CompletedEventType
    {
        public string? EventName { get; set; }
        public string? Date { get; set; }
        public List<MatchHistoryType> Fights { get; set; } = new List<MatchHistoryType>();
    }
}
