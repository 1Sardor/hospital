
namespace ReseptionApp.API.Hospital
{
    public class Hospital
    {
        public int id { get; set; }
        public int get_order { get; set; }
        public int type { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string inn { get; set; }
        public Region region { get; set; }
    }

    public class Region
    {
        public int id { get; set; }
        public string name { get; set; }
        public int province { get; set; }
    }
}
