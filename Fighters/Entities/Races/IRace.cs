using Fighters.UI.Interfaces;

namespace Fighters.Entities.Races
{
    public interface IRace : INameable
    {
        int Health { get; }
        int Strength { get; }
        int Armor { get; }
    }
}