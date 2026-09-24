using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Day2eEditor
{
    public class UniversalAttachmentDump
    {
        public BindingList<DumpItem> Items { get; set; } = new();
    }
    public class DumpItem
    {
        public string name { get; set; }
        public string configRoot { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public string parentClass { get; set; }
        // These will be populated for weapons and empty for most other items
        public BindingList<string> bullets { get; set; } = new();
        public BindingList<string> magazines { get; set; } = new();
        public BindingList<DumpSlot> attachmentSlots { get; set; } = new();
    }
    public class DumpSlot
    {
        public string slotName { get; set; }
        public BindingList<string> compatibleItems { get; set; } = new();
    }
}
