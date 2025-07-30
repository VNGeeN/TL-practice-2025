using Fighters.UI.Interfaces;
namespace Fighters.Entities.Races;

public class Human : IRace, INameable
{
    public string Name => "Человек";
    public int Health => 100;
    public int Strength => 10;
    public int Armor => 5;
}