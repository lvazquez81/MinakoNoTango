using MinakoNoTangoLib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinakoNoTangoLib.Library.Models
{
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public SecurityToken GetAuthenticationToken()
        {
            return new SecurityToken()
            {
                Username = this.Username,
                Token = Guid.NewGuid().ToString(),
                ExpirationDate = DateTime.Now.AddHours(1)
            };
        }
    }
}
