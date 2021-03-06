
using System.Collections.Generic;

namespace HospitalApp.API.Retsept.RetseptItem
{
    public class RetseptItem
    {
        public int id { get; set; }
        public int get_order { get; set; }
        public string name { get; set; }
        public string duration { get; set; }
        public Retsept retsept { get; set; }
    }

    public class Doctor
    {
        public int id { get; set; }
        public string password { get; set; }
        public object last_login { get; set; }
        public bool is_superuser { get; set; }
        public string username { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public bool is_staff { get; set; }
        public bool is_active { get; set; }
        public string date_joined { get; set; }
        public int type { get; set; }
        public string phone { get; set; }
        public string room { get; set; }
        public int direction { get; set; }
        public int hospital { get; set; }
        public List<object> groups { get; set; }
        public List<object> user_permissions { get; set; }
    }

    public class Patient
    {
        public int id { get; set; }
        public string name { get; set; }
        public int phone { get; set; }
        public string birthday { get; set; }
        public int hospital { get; set; }
    }

    public class Retsept
    {
        public int id { get; set; }
        public string date { get; set; }
        public Doctor doctor { get; set; }
        public Patient patient { get; set; }
    }

}
