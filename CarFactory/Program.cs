using CarFactory.UI;
using CarFactory.Factories.Body;
using CarFactory.Factories.Transmission;
using CarFactory.Factories.Engine;
using Microsoft.Extensions.DependencyInjection;

namespace CarFactory
{
    class Program
    {
        static void Main( string[] args )
        {
            ServiceProvider? serviceProvider = new ServiceCollection()
                .AddSingleton<IEngineFactory, EngineFactory>()
                .AddSingleton<ITransmissionFactory, TransmissionFactory>()
                .AddSingleton<IBodyFactory, BodyFactory>()
                .AddSingleton<ICarConfigurator, CarConfigurator>()
                .AddSingleton<MainMenu>()
                .BuildServiceProvider();
            UI.MainMenu? mainMenu = serviceProvider.GetService<UI.MainMenu>();
            mainMenu?.Show();
        }
    }
}