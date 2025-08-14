using CarFactory.Enums;

namespace CarFactory.Services
{
    public class DescriptionService : IDescriptionService
    {
        public string GetEngineDescription( EngineType engineType ) => engineType switch
        {
            EngineType.Gasoline => "Бензиновый (150 л.с.)",
            EngineType.Diesel => "Дизельный (200 л.с.)",
            _ => "Неизвестный двигатель"
        };

        public string GetTransmissionDescription( TransmissionType transmissionType ) => transmissionType switch
        {
            TransmissionType.Automatic => "Автоматическая (6 передач)",
            TransmissionType.Manual => "Механическая (5 передач)",
            _ => "Неизвестная коробка передач"
        };

        public string GetBodyDescription( BodyType bodyType ) => bodyType switch
        {
            BodyType.Sedan => "Седан",
            BodyType.Hatchback => "Хэтчбек",
            _ => "Неизвестный тип кузова"
        };

        public string GetColorDescription( ColorType colorType ) => colorType switch
        {
            ColorType.Red => "Красный",
            ColorType.Blue => "Синий",
            ColorType.Black => "Чёрный",
            ColorType.White => "Белый",
            _ => "Неизвестный цвет"
        };

        public string GetColorName( ColorType colorType ) => GetColorDescription( colorType );
    }
}
