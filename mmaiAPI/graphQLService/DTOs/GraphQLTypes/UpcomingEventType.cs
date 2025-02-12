namespace graphQLService.DTOs.GraphQLTypes
{
    public class UpcomingEventType
    {
        public string? EventName { get; set; }
        public string? Location { get; set; }
        public string? Date { get; set; }
        public List<UpcomingFightType> Fights { get; set; }
    }
}
