
namespace HospitalApp.API
{
    public class AllApi
    {
        public static readonly string BASE_URL = "http://192.168.67.104:8000/"; //http://167.99.212.88/

        public static readonly string LOGIN = BASE_URL + "login/";

        public static readonly string PATIENT = BASE_URL + "api/patient/search/";

        public static readonly string RETSEPTS = BASE_URL + "api/retsepts/get_retsepts_of_client/";

        public static readonly string RETSEPT_ITEM = BASE_URL + "api/drugs/get_by_retsep/";

        public static readonly string PATINET_ADD = BASE_URL + "api/patient/";
    }
}
