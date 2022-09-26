﻿using System.Text;

namespace WebServer.HTTP
{
    public class ResponseCookie : Cookie
    {
        public ResponseCookie(string name, string value) : base(name, value)
        {
            this.Path = "/";
        }

        public int MaxAge { get; set; }
        public string Path { get; set; }
        public bool HttpOnly { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"{this.Name}={this.Value}; Path={this.Path};");

            if (this.MaxAge != 0)
            {
                sb.Append($" Max-Age={this.MaxAge};");
            }

            if (this.HttpOnly)
            {
                sb.Append($" HttpOnly;");
            }

            return sb.ToString();
        }
    }
}