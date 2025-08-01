using Fighters.Entities.Characters;
using Fighters.UI.Enums;

namespace Fighters.UI.Menus
{
    public abstract class MenuBase
    {
        public MenuState CurrentState { get; set; }
        public Stack<MenuState> StateHistory { get; } = new Stack<MenuState>();
        public IFighter? CurrentFighter { get; set; }
        public string FighterType { get; set; } = "Боец";

        public abstract void Display();
        public abstract void HandleInput( NavigationOptions option );

        public void NavigateTo( MenuState newState )
        {
            StateHistory.Push( CurrentState );
            CurrentState = newState;
        }
    }
}