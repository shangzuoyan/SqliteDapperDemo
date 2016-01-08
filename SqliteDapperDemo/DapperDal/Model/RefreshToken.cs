using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperDal
{
    public class RefreshToken
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string ClientId { get; set; }

        public string IssuedUtc { get; set; }

        public string ExpiresUtc { get; set; }

        public string ProtectedTicket { get; set; }
    }
}
