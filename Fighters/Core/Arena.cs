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
            // Диагностика перед боем
            if ( fighters == null )
                throw new ArgumentNullException( nameof( fighters ), "Список бойцов не инициализирован." );

            Console.WriteLine( $"[DEBUG] Количество бойцов: {fighters.Count}" );
            for ( int i = 0; i < fighters.Count; i++ )
            {
                var f = fighters[ i ];
                if ( f == null )
                    throw new InvalidOperationException( $"[DEBUG] Боец #{i} равен NULL" );
                Console.WriteLine( $"[DEBUG] Боец #{i}: {f.GetType().Name}, Name={f.Name}, HP={f.CurrentHealth}/{f.MaxHealth}" );
            }
            Console.WriteLine( "=== НАЧАЛО БИТВЫ ===" );
            Console.WriteLine( "Список бойцов:" );
            // foreach ( var f in fighters )
            //     Console.WriteLine( f?.Name ?? "NULL" );
            Console.WriteLine( $"Участники: {string.Join( ", ", fighters.Select( f => f.Name ) )}" );
            Console.WriteLine( "----------------------------------" );

            int round = 1;
            List<IFighter> aliveFighters = fighters.ToList();  // Копия списка для отслеживания живых бойцов

            // Основной цикл битвы (пока не останется 1 победитель)
            while ( aliveFighters.Count > 1 )
            {
                Console.WriteLine( $"\nРАУНД {round++}" );

                // Случайный порядок ходов в раунде
                List<IFighter>? shuffled = aliveFighters.OrderBy( f => _random.Next() ).ToList();

                // Обработка хода каждого бойца
                foreach ( IFighter attacker in shuffled )
                {
                    if ( attacker.CurrentHealth <= 0 ) continue;  // Пропуск мертвых

                    IFighter? target = Get_randomTarget( attacker, aliveFighters );
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
            List<IFighter> possibleTargets = fighters
                .Where( f => f != attacker && f.CurrentHealth > 0 )
                .ToList();

            return possibleTargets.Count == 0 ? null : possibleTargets[ _random.Next( possibleTargets.Count ) ];
        }
    }
}
