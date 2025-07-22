public static class OrderDeliveryService
{
    public static void SuccesfullOrder( OrderData data )
    {
        DateTime deliveryDate = DateTime.Today.AddDays( 3 );
        string successMessage =
            $"{data.Name}! Ваш заказ {data.Product} в количестве {data.Count} оформлен! " +
            $"Ожидайте доставку по адрессу {data.Address} к {deliveryDate:dd.MM.yyyy}";

        Console.WriteLine( successMessage );
    }
}