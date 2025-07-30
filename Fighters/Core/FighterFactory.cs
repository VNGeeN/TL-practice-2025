using Fighters.Entities.Armors;
using Fighters.Entities.Characters;
using Fighters.Entities.Classes;
using Fighters.Entities.Races;
using Fighters.Entities.Weapons;

namespace Fighters.Core
{

    public class FighterFactory
    {
        /// <summary> Создать бойца с указанными компонентами </summary>
        public IFighter CreateFighter(
            string fighterType,
            IRace race,
            IClass fighterClass,
            IWeapon weapon,
            IArmor armor )
        {
            // Валидация обязательных компонентов
            if ( race == null ) throw new ArgumentNullException( "Раса не выбрана" );
            if ( fighterClass == null ) throw new ArgumentNullException( "Класс не выбран" );
            if ( weapon == null ) throw new ArgumentNullException( "Оружие не выбрано" );
            if ( armor == null ) throw new ArgumentNullException( "Броня не выбрана" );

            // Создание и конфигурация бойца
            var fighter = new CustomFighter( fighterType, race, fighterClass );
            fighter.SetWeapon( weapon );
            fighter.SetArmor( armor );

            return fighter;
        }

        public List<IRace> GetAvailableRaces() => new() { new Human(), new Orc(), new Elf() };
        public List<IClass> GetAvailableClasses() => new() { new Knight(), new Mercenary(), new Mage() };
        public List<IWeapon> GetAvailableWeapons() => new() { new Sword(), new Axe(), new Staff() };
        public List<IArmor> GetAvailableArmors() => new() { new PlateArmor(), new LeatherArmor(), new MagicShield() };
    }
}