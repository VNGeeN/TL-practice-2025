using Fighters.Entities.Races;
using Fighters.Entities.Classes;

namespace Fighters.Entities.Characters
{
    public class CustomFighter : BaseFighter
    {
        public CustomFighter( string name, IRace race, IClass fighterClass )
            : base( name, race, fighterClass ) { }
    }
}