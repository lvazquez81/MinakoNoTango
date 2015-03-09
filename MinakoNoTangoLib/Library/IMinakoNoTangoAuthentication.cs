using MinakoNoTangoLib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinakoNoTangoLib.Library
{
    public interface IMinakoNoTangoAuthentication
    {
        SecurityToken GetAuthenticationToken(string userName, string password);

        bool IsValidToken(string userName, string token);
    }
}
