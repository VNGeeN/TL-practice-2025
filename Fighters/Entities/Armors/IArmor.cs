using Fighters.UI.Interfaces;

namespace Fighters.Entities.Armors
{
    public interface IArmor : INameable
    {
        int ArmorPoints { get; }
    }
}