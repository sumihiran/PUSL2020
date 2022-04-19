namespace PUSL2020.Web.Identity;

public static class EmployeeAuthenticationWebDefaults
{
        /// <summary>
        /// The default value used by CookieAuthenticationMiddleware for the
        /// CookieAuthenticationOptions.LoginPath
        /// </summary>
        public static readonly PathString LoginPath = new("/BackOffice/Login");

        /// <summary>
        /// The default value used by CookieAuthenticationMiddleware for the
        /// CookieAuthenticationOptions.LogoutPath
        /// </summary>
        public static readonly PathString LogoutPath = new("/BackOffice/Logout");

        /// <summary>
        /// The default value used by CookieAuthenticationMiddleware for the
        /// CookieAuthenticationOptions.AccessDeniedPath
        /// </summary>
        public static readonly PathString AccessDeniedPath = new("/BackOffice/AccessDenied");

        /// <summary>
        /// The default value of the CookieAuthenticationOptions.ReturnUrlParameter
        /// </summary>
        public static readonly string ReturnUrlParameter = "ReturnUrl";
}