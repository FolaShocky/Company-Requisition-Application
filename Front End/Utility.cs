using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace DissertWindowsFormApplication
{
    public class Utility
    {
        public static User LoggedInUser { get; set; }

        public static Chat SelectedChat { get; set; }

        public static ChatParticipant SelectedChatParticipant { get; set; }
    }
}
