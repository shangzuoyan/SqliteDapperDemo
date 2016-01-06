using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperDal
{
    public static class Program
    {
        public static void Main()
        {
            RefreshToken refreshToken = new RefreshToken() {
                Id = "dafdasf",
                UserName = "goldenkey",
                ClientId = new Guid(),
                IssuedUtc = DateTime.Now,
                ExpiresUtc = DateTime.Now.AddDays(10),
                ProtectedTicket = "testTikect"
            };
            RefreshTokenDal rtDal = new RefreshTokenDal();
            rtDal.Insert(refreshToken);
            Console.ReadLine();
        }
    }
}
