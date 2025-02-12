using graphQLService.Services.Interfaces.DependencyInjection;
using graphQLService.DTOs.GraphQLTypes;

namespace graphQLService.Services.Interfaces
{
    public interface IFighterService : ITransient
    {
        FighterType GetOneFighter(string name);
        IEnumerable<FighterType> GetAllFighters();
        Task<PredictedWinnerType> GetPredictedWinner(string redName, string blueName);
    }
}
