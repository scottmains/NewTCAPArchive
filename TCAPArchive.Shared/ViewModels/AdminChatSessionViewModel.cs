using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCAPArchive.Shared.Domain;

namespace TCAPArchive.Shared.ViewModels
{
    public class AdminChatSessionViewModel
    {
       public ChatSession ChatSession { get; set; }
       public Predator predator { get; set; }
       public Decoy decoy { get; set; }
       public int LineCount { get; set; }
    }
    

}
