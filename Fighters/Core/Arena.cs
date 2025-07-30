using Fighters.Entities.Characters;

namespace Fighters.Core
{
    public class Arena
    {
        private readonly Random _random = new Random();
        /// <summary> Начать битву между списком бойцов </summary>
        /// <returns>Победитель битвы</returns>
        public IFighter StartBattle( List<IFighter> fighters )
        {
            Console.Clear();
            Console.WriteLine( "=== НАЧАЛО БИТВЫ ===" );
            Console.WriteLine( $"Участники: {string.Join( ", ", fighters.Select( f => f.Name ) )}" );
            Console.WriteLine( "----------------------------------" );

            int round = 1;
            var aliveFighters = fighters.ToList();  // Копия списка для отслеживания живых бойцов

            // Основной цикл битвы (пока не останется 1 победитель)
            while ( aliveFighters.Count > 1 )
            {
                Console.WriteLine( $"\nРАУНД {round++}" );

                // Случайный порядок ходов в раунде
                var shuffled = aliveFighters.OrderBy( f => _random.Next() ).ToList();

                // Обработка хода каждого бойца
                foreach ( var attacker in shuffled )
                {
                    if ( attacker.CurrentHealth <= 0 ) continue;  // Пропуск мертвых

                    var target = Get_randomTarget( attacker, aliveFighters );
                    if ( target == null ) break;  // Если не осталось целей

                    // Расчет параметров атаки
                    int damage = attacker.CalculateDamage();
                    int targetArmor = target.CalculateArmor();
                    int effectiveDamage = Math.Max( damage - targetArmor, 0 );

                    // Вывод информации об атаке
                    Console.WriteLine( $"{attacker.Name} атакует {target.Name}" );
                    Console.WriteLine( $"Урон: {damage} | Броня цели: {targetArmor} | Эффективный урон: {effectiveDamage}" );

                    // Применение урона
                    target.TakeDamage( effectiveDamage );
                    Console.WriteLine( $"{target.Name}: {target.CurrentHealth}/{target.MaxHealth} HP" );

                    // Проверка смерти цели
                    if ( target.CurrentHealth <= 0 )
                    {
                        Console.WriteLine( $"{target.Name} УНИЧТОЖЕН!" );
                        aliveFighters.Remove( target );
                    }

                    // Прерывание если остался 1 боец
                    if ( aliveFighters.Count <= 1 ) break;
                }

                // Удаление мертвых бойцов из списка живых
                aliveFighters = aliveFighters.Where( f => f.CurrentHealth > 0 ).ToList();
                Console.WriteLine( "----------------------------------" );
            }

            return aliveFighters.First();  // Возврат победителя
        }

        private IFighter? Get_randomTarget( IFighter attacker, List<IFighter> fighters )
        {
            var possibleTargets = fighters
                .Where( f => f != attacker && f.CurrentHealth > 0 )
                .ToList();

            return possibleTargets.Count == 0 ? null : possibleTargets[ _random.Next( possibleTargets.Count ) ];
        }
    }
}
