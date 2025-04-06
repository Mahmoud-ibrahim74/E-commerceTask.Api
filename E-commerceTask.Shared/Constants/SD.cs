namespace E_commerceTask.Shared.Constants;

public static class SD
{
    public static class FileSettings
    {
        public const string SpecialChar = @"|!#$%&[]=?»«@£§€{};<>";
        public const int Length = 50;
        public const long MaxFile512Mb = 512 * 1024 * 1024;
        public const long MaxFile50Mb = 50 * 1024 * 1024;
        public const long MaxFile10Mb = 10 * 1024 * 1024;
        public const string AllowedExtension = ".jpg,.png,.jpeg";
        public const double KB = 1024.0;
        public const long MB = 1024 * 1024;
        public const long GB = 1024 * 1024 * 1024;
        public const long MaxRequestbodySize2GB = 2 * GB;

        public const string UsersPath = "Auth/Users";

    }
    public static class Shared
    {
        public const string EcommerceDbConnection = "EcommerceDbConnection";
        public const string EcommerceApp = "EcommerceApp";
        public const string AppVersionRoute = "api/v1/";
        public const string AppVersion = "v1";

    }
    public static class Modules
    {
        public const string AdminPanel = "AdminPanel";
    }
    public static class ApiRoutes
    {


    }
 

}
