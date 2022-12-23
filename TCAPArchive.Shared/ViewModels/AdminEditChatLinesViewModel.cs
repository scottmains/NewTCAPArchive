using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCAPArchive.Shared.Domain;

namespace TCAPArchive.Shared.ViewModels
{
    public class AdminEditChatLinesViewModel
    {
        public byte[] ImageData { get; set; }
        
        public ChatLine chatLine { get; set; }
    }
}
