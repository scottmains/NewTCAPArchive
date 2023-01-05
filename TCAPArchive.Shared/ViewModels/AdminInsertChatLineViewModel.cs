using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCAPArchive.Shared.Domain;

namespace TCAPArchive.Shared.ViewModels
{
    public class AdminInsertChatLineViewModel
    {
        public ChatLine chatline { get; set; }
        public int PositionToInsert { get; set; }   
    }
}
