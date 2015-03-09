using MinakoNoTangoLib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinakoNoTangoLib.Library
{
    public class MinakoNoTangoAuthentication : IMinakoNoTangoAuthentication
    {
        public bool IsValidToken(string userName, string token)
        {
            return true;
        }

        SecurityToken IMinakoNoTangoAuthentication.GetAuthenticationToken(string userName, string password)
        {
            return new SecurityToken()
            {
                Username = userName,
                Token = "123abc",
                ExpirationDate = DateTime.Now.AddHours(1)
            };
        }
    }
}
