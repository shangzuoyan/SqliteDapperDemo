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
            RefreshTokenDal test = new RefreshTokenDal();
            test.Insert(new RefreshToken() {  ClientId="2222", Id="11", UserName="GoldenKeyHu", ProtectedTicket="222222", ExpiresUtc="2015-1-2", IssuedUtc="2016-1-8"});
            var result = test.GetEntities(m => true);
            foreach (var item in result)
            {
                Console.WriteLine(item.UserName); 
            }
            //IEnumerable<RefreshToken> tokens = test.GetEntities(i => i.Id.Contains("a"));
            Console.ReadLine();
        }
    }
}
