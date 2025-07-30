using Fighters.UI.Interfaces;


namespace Fighters.Entities.Weapons
{
    public interface IWeapon : INameable
    {
        int Damage { get; }
    }
}