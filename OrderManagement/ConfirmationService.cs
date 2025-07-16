public static class ConfirmationService
{
    public static NavigationOption ConfirmOrder( OrderData data )
    {
        string confirmMessage =
            $"Здравствуйте, {data.Name}, вы заказали {data.Count} {data.Product} " +
            $"на адрес {data.Address}, всё верно ? ";

        return DataInputService.GetNavigationChoice
            (
                confirmMessage,
                new Dictionary<NavigationOption, string>
                {
                    { NavigationOption.Confirm, "Да, всё верно" },
                    { NavigationOption.Back, "Назад к редактированию" },
                    { NavigationOption.Menu, "В главное меню" },
                    { NavigationOption.Cancel, "Отменить заказ" }
                }
            );
    }
}