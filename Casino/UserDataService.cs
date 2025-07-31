namespace Casino
{
    public class UserDataService
    {
        public UserGameData UserData { get; private set; } = new();

        public void UpdateUserData( string userName, int userBalance )
        {
            UserData = new UserGameData
            {
                UserName = userName,
                UserBalance = userBalance
            };
        }

        public void ResetUserData()
        {
            UserData = new UserGameData();
        }
    }
}