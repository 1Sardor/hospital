﻿
namespace HospitalApp.API
{
    public class AllApi
    {
        public static readonly string BASE_URL = "http://192.168.67.104:8000/"; //http://167.99.212.88/

        public static readonly string LOGIN = BASE_URL + "login/";

        public static readonly string CLIENT = BASE_URL + "api/client/";

        public static readonly string MONITOR = BASE_URL + "api/monitors/";
    }
}