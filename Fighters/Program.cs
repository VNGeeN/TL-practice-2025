// public interface IFighter
// {
//     //
//     int CurrentHealth { get; }
//     object Name { get; }
//     object Armor { get; }
//     object MaxHealth { get; }
//     object Race { get; }
//     object Class { get; }
//     object Weapon { get; }

//     object CalculateArmor();
//     object CalculateDamage();
//     void TakeDamage( object value );
// }
// //наследуемся от базовго класса
// public class BaseClass : IFighter
// {
//     // public virtual void Fight(){

//     // }
// }

// // и т.д.

class Program
{
    static void Main( string[] args )
    {
        var gameManager = new GameManager();
        gameManager.Run();
    }
}