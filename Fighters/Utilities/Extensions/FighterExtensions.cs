using Fighters.Entities.Characters;


namespace Fighters.Utilities.Extensions
{
    public static class FighterExtensions
    {
        public static bool IsAlive( this IFighter fighter )
        {
            return fighter.CurrentHealth > 0;
        }

        public static double HealthPercentage( this IFighter fighter )
        {
            return ( double )fighter.CurrentHealth / fighter.MaxHealth * 100;
        }

        public static string GetStatus( this IFighter fighter )
        {
            return $"{fighter.Name}: {fighter.CurrentHealth}/{fighter.MaxHealth} HP";
        }

        public static string GetFullInfo( this IFighter fighter )
        {
            return $"Имя: {fighter.Name}\n" +
                   $"Раса: {fighter.Race.Name}\n" +
                   $"Класс: {fighter.Class.Name}\n" +
                   $"Оружие: {fighter.Weapon.Name} (Урон: {fighter.Weapon.Damage})\n" +
                   $"Броня: {fighter.Armor.Name} (Защита: {fighter.Armor.ArmorPoints})\n" +
                   $"Здоровье: {fighter.CurrentHealth}/{fighter.MaxHealth}\n" +
                   $"Суммарный урон: {fighter.CalculateDamage()}\n" +
                   $"Суммарная защита: {fighter.CalculateArmor()}";
        }

        public static void ResetHealth( this IFighter fighter )
        {
            fighter.TakeDamage( -fighter.MaxHealth ); // Восстанавливает здоровье
        }

        public static double CalculateEffectivenessAgainst( this IFighter attacker, IFighter defender )
        {
            int attackPower = attacker.CalculateDamage();
            int defensePower = defender.CalculateArmor();

            // Простая формула: атака / защита
            return defensePower > 0 ? ( double )attackPower / defensePower : attackPower * 1.5;
        }
    }
}