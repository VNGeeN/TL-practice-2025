using Fighters.Entities.Races;
using Fighters.Entities.Classes;
using Fighters.Entities.Weapons;
using Fighters.Entities.Armors;

namespace Fighters.Entities.Characters
{
    public interface IFighter
    {
        string Name { get; }          // Имя бойца
        IRace Race { get; }           // Раса бойца
        IClass Class { get; }         // Класс бойца
        IWeapon Weapon { get; }       // Экипированное оружие
        IArmor Armor { get; }         // Экипированная броня
        int CurrentHealth { get; }    // Текущее здоровье
        int MaxHealth { get; }        // Максимальное здоровье

        /// <summary> Установить новое оружие </summary>
        void SetWeapon( IWeapon weapon );

        /// <summary> Установить новую броню </summary>
        void SetArmor( IArmor armor );

        /// <summary> Получить урон </summary>
        void TakeDamage( int damage );

        /// <summary> Рассчитать суммарный урон (сила + оружие) </summary>
        int CalculateDamage();

        /// <summary> Рассчитать суммарную защиту (броня) </summary>
        int CalculateArmor();
    }
}
