using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Helpers.UserRoleHelpers;

public static class UserRoleServiceHelper
{
    public static void CheckArgsForException(UserRole userRole)
    {
        if (userRole == null)
            throw new ArgumentNullException();
        if (string.IsNullOrWhiteSpace(userRole.Name) ||
            userRole.CreatedDate == DateTime.MinValue)
            throw new ArgumentException();
    }
}