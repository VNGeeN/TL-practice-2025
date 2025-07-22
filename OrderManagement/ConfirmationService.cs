public static class ConfirmationService
{
    public static NavigationOptions ConfirmOrder( OrderData data )
    {
        string confirmMessage =
            $"Здравствуйте, {data.Name}, вы заказали {data.Count} {data.Product} " +
            $"на адрес {data.Address}, всё верно ? ";

        return DataInputService.GetNavigationChoice
            (
                confirmMessage,
                new Dictionary<NavigationOptions, string>
                {
                    { NavigationOptions.Confirm, "Да, всё верно" },
                    { NavigationOptions.Back, "Назад к редактированию" },
                    { NavigationOptions.Menu, "В главное меню" },
                    { NavigationOptions.Cancel, "Отменить заказ" }
                }
            );
    }
}