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
       public ChatSession chatsession { get; set; }
       public string PredatorName { get; set; }
       public string DecoyName { get; set; }
       public int LineCount { get; set; }
       public byte[] ImageData { get; set; }
    }
    

}
