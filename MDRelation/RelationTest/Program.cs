using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RelationTest
{
    public class Program
    {
        public static string seturl = "http://localhost:63794/api/relation/set";

        public static void Main(string[] args)
        {
            HttpClient client = new HttpClient();

            for (int i = 0; i < 1; i++)
            {
                var obj = new
                {
                    aid = Guid.NewGuid().ToString().ToLower(),
                    eid = Guid.NewGuid().ToString().ToLower(),
                    type = 2
                };
                var s = obj.ToString();
             var t=      client.PostAsync(seturl, new StringContent(s, Encoding.UTF8, "application/json"));
                var r = t.Result;
            }




        }
    }
}
