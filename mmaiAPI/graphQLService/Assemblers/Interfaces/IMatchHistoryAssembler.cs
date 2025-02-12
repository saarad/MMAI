using graphQLService.Services.Interfaces.DependencyInjection;
using graphQLService.DTOs.GraphQLTypes;
using dataAccess.Models;

namespace graphQLService.Assemblers.Interfaces
{
    public interface IMatchHistoryAssembler : ITransient
    {
        IEnumerable<MatchHistoryType> WriteMatchHistoryToGraphQLType(IEnumerable<MatchHistory> matchHistory);
    }
}
