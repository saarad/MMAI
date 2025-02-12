using dataAccess.Models;

namespace dataAccess.Interfaces
{
    public interface IFighterRepository : IRepository
    {
        Fighter GetOneFighter(string name);
        Fighter CreateOneFighter(Fighter fighter);
        IEnumerable<Fighter> GetAllFighters();
    }
}
