
namespace ReseptionApp.API
{
    public class AllApi
    {
        public static readonly string BASE_URL = "http://192.168.67.104:8000/";

        public static readonly string LOGIN = BASE_URL + "login/";

        public static readonly string DOCTOR = BASE_URL + "api/doctor/";

        public static readonly string HOSPITAL = BASE_URL + "api/hospital/";

        public static readonly string PATIENT = BASE_URL + "api/patient/filter_date/";

        public static readonly string PATINET_ADD = BASE_URL + "api/patient/";

    }
}
