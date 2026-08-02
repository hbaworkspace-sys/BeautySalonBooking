namespace BeautySalonBooking.Contracts.Authentication.Dtos;

public class PermissionDto
{
    /// <summary>
    /// کد دسترسی
    /// مثال:
    /// User.Create
    /// Branch.Delete
    /// Dashboard
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// عنوان فارسی
    /// مثال:
    /// ایجاد کاربر
    /// حذف شعبه
    /// داشبورد
    /// </summary>
    public string Title { get; set; } = string.Empty;
}