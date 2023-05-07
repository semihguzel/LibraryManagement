using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Helpers.UserHelpers;

public static class UserServiceHelper
{
    public static void CheckArgsForException(User user)
    {
        if (user == null)
            throw new ArgumentNullException();
        if (string.IsNullOrEmpty(user.Username) ||
            string.IsNullOrEmpty(user.Email) ||
            string.IsNullOrEmpty(user.Phone) ||
            user.UserRoles.Count == 0)
            throw new ArgumentException();
    }
}