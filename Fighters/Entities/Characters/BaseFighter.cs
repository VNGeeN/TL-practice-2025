using Fighters.Entities.Armors;
using Fighters.Entities.Races;
using Fighters.Entities.Weapons;
using Fighters.Entities.Classes;

namespace Fighters.Entities.Characters
{
    public abstract class BaseFighter : IFighter
    {
        public string Name { get; }     // Имя бойца (устанавливается при создании)
        public IRace Race { get; }      // Раса (не может быть изменена после создания)
        public IClass Class { get; }    // Класс (не может быть изменен после создания)

        // Оружие и броня могут быть изменены через методы SetWeapon/SetArmor
        public IWeapon Weapon { get; private set; }
        public IArmor Armor { get; private set; }

        public int CurrentHealth { get; private set; }
        public int MaxHealth { get; }  // Рассчитывается при создании бойца

        /// <summary>
        /// Конструктор базового бойца
        /// </summary>
        /// <param name="name">Имя бойца</param>
        /// <param name="race">Выбранная раса</param>
        /// <param name="fighterClass">Выбранный класс</param>
        protected BaseFighter( string name, IRace race, IClass fighterClass )
        {
            Name = name;
            Race = race;
            Class = fighterClass;

            // Значения по умолчанию
            Weapon = new Fists();
            Armor = new NoArmor();

            // Рассчет характеристик
            MaxHealth = Race.Health + Class.Health;
            CurrentHealth = MaxHealth;  // Начальное здоровье = максимуму
        }

        public void SetWeapon( IWeapon weapon ) => Weapon = weapon;
        public void SetArmor( IArmor armor ) => Armor = armor;

        /// <summary> Рассчет суммарного урона </summary>
        public int CalculateDamage() =>
            Race.Strength + Class.Strength + Weapon.Damage;

        /// <summary> Рассчет суммарной защиты </summary>
        public int CalculateArmor() =>
            Race.Armor + Armor.ArmorPoints;

        /// <summary> Обработка полученного урона с учетом защиты </summary>
        public void TakeDamage( int damage )
        {
            // Эффективный урон = урон - защита (но не меньше 0)
            int effectiveDamage = Math.Max( damage - CalculateArmor(), 0 );

            // Уменьшаем здоровье, но не ниже 0
            CurrentHealth = Math.Max( CurrentHealth - effectiveDamage, 0 );
        }
    }
}
