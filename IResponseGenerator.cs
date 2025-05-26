using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberSecurity_Bot
{
    public interface IResponseGenerator
    {
        string GenerateResponse(string input);
    }
}
