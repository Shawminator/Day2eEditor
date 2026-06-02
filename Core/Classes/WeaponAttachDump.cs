using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day2eEditor
{
    public class WeaponAttachDump
    {
        public BindingList<DumpWeapon> DumpWeapons { get; set; }
    }
    public class DumpWeapon
    {
        public string name { get; set; }
        public BindingList<string> attachments { get; set; }
        public BindingList<string> attachmentsBayonet { get; set; }
        public BindingList<string> attachmentsBipods { get; set; }
        public BindingList<string> attachmentsButtStocks { get; set; }
        public BindingList<string> attachmentsHandguards { get; set; }
        public BindingList<string> attachmentIllumination { get; set; }
        public BindingList<string> attachmentsOpticsAndSights { get; set; }
        public BindingList<string> attachmentsMuzzles { get; set; }
        public BindingList<string> attachmentsWraps { get; set; }
        public BindingList<string> attachmentsAFG { get; set; }
        public BindingList<string> bullets { get; set; }
        public BindingList<string> magazines { get; set; }
    }

}
