using Example1.DAL;
using Example1.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Example1
{
    class Program
    {
        public static string Data(string t)
        {
            DbContext dbContext = new DbContext();
            var userInfo = dbContext.GetInfoUser();
            string userName = userInfo.First().Name;

            return userName;
        }

        static void Main(string[] args)
        {
            var usersCache = new NaiveCache<User>();

            var userNameCache = new MyCache<string, string>(TimeSpan.FromMilliseconds(5000),  Data);

            Console.WriteLine("First time: " + userNameCache["UserName"]);
            Task.Delay(10000).Wait();
            Console.WriteLine("after 12s: " + userNameCache["UserName"]);

            //usersListCache.CleanUp();
            Console.ReadLine();
        }
    }
}
 