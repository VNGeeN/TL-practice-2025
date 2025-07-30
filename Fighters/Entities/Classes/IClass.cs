using Fighters.UI.Interfaces;

namespace Fighters.Entities.Classes
{
    public interface IClass : INameable
    {
        int Health { get; }
        int Strength { get; }
    }
}