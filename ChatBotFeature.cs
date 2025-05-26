using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberSecurity_Bot
{
    public abstract class ChatBotFeature
    {
        public abstract string ProcessInput(string input, UserData userData);
    }
}
