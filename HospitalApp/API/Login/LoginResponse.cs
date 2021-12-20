
namespace HospitalApp.API.Login
{
    public class LoginResponse
    {
        public int status { get; set; }
        public string username { get; set; }
        public int user_id { get; set; }
        public string token { get; set; }
        public int type { get; set; }
    }
}
