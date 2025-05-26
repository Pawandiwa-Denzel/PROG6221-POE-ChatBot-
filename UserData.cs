using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberSecurity_Bot
{
    
        public class UserData
        {
            public string Name { get; set; }
            public string FavoriteTopic { get; set; }
            public string CurrentMood { get; set; }
            public List<string> ConversationHistory { get; } = new List<string>();
        }
    
}
