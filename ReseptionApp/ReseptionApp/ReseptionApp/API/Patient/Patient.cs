
namespace ReseptionApp.API.Patient
{
    public class Patient
    {
        public int id { get; set; }
        public int get_order { get; set; }
        public string name { get; set; }
        public int phone { get; set; }
        public string birthday { get; set; }
        public string created_at { get; set; }
        public int hospital { get; set; }
    }
}
