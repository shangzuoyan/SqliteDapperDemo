using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperDal
{
    class RefreshTokenDal:BaseDal<RefreshToken>
    {
        public override void SetQuerys()
        {
            querys.GetEntities = "select * from RefreshToken";
            querys.Add = "insert into RefreshToken VALUES(@Id,@UserName,@ClientId,@IssuedUtc,@ExpiresUtc,@ProtectedTicket);";
            querys.Update = "update RefreshToken set Id=@Id,IssuedUtc=@IssuedUtc,ExpiresUtc=@ExpiresUtc,ProtectedTicket=@ProtectedTicket where UserName=@UserName and ClientId=@ClientId";
            querys.Delete = "delete from RefreshToken where UserName=@UserName and ClientId=@ClientId";
        }
    }
}
