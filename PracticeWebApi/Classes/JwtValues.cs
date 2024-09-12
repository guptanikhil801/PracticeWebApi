﻿namespace PracticeWebApi.Classes
{
    public class JwtValues
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Key { get; set; }
        public int ExpiresInMinutes { get; set; }
    }
}
