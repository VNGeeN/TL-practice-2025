using CarFactory.Entities.Cars;

namespace CarFactory.UI
{
    public interface ICarConfigurator
    {
        ICar Configure();
    }
}