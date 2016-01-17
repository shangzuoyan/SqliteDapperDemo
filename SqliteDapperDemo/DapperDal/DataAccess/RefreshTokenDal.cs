using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperDal
{
    class RefreshTokenDal:BaseDal<RefreshToken>
    {
        public override void SetSqlCUD()
        {
            sqlCUD.Add = "insert into RefreshToken VALUES(@Id,@UserName,@ClientId,@IssuedUtc,@ExpiresUtc,@ProtectedTicket);";
            sqlCUD.Update = "update RefreshToken set Id=@Id,IssuedUtc=@IssuedUtc,ExpiresUtc=@ExpiresUtc,ProtectedTicket=@ProtectedTicket where UserName=@UserName and ClientId=@ClientId";
            sqlCUD.Delete = "delete from RefreshToken where UserName=@UserName and ClientId=@ClientId";
        }
    }
}
