using Project_Management_System.Common.Views;

namespace Project_Management_System.Common;

public class UserInfoProvider
{
    public UserInfo UserInfo { get; set; }

    public UserInfoProvider()
    {
        UserInfo = new UserInfo(); // Ensure it's initialized by default
    }
}