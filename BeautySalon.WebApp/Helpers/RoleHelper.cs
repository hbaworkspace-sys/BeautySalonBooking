
namespace BeautySalonBooking.WebApp.Helpers
{
    public static class RoleHelper
    {
        public static string GetRoleName(int authType)
        {
            return authType switch
            {
                1 => "مشتری",
                2 => "مدیر مجموعه",
                3 => "ارائه‌دهنده خدمات",
                4 => "پذیرش",
                90 => "مدیر سیستم",
                99 => "توسعه‌دهنده",
                100 => "سایر",
                _ => "کاربر"
            };
        }

        public static string GetRoleColor(int authType)
        {
            return authType switch
            {
                1 => "#6c757d",
                2 => "#0d6efd",
                3 => "#198754",
                4 => "#fd7e14",
                90 => "#dc3545",
                99 => "#6f42c1",
                100 => "#adb5bd",
                _ => "#6c757d"
            };
        }

        public static string GetRoleIcon(int authType)
        {
            return authType switch
            {
                1 => "👤",
                2 => "🏢",
                3 => "💇",
                4 => "📋",
                90 => "👑",
                99 => "💻",
                100 => "🧑",
                _ => "👤"
            };
        }

        public static string GetRoleDescription(int authType)
        {
            return authType switch
            {
                1 => "کاربر عادی سالن",
                2 => "مدیریت کل مجموعه",
                3 => "ارائه‌دهنده خدمات به مشتریان",
                4 => "مدیریت نوبت‌ها و پذیرش",
                90 => "مدیریت کامل سیستم",
                99 => "دسترسی سطح توسعه",
                100 => "سایر نقش‌ها",
                _ => "نقش نامشخص"
            };
        }
    }
}