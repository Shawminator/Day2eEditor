using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpansionPlugin
{
    public class ExpansionLoot : IDeepCloneable<ExpansionLoot>, IEquatable<ExpansionLoot>
    {
        public string Name { get; set; }
        public BindingList<ExpansionLootVariant> Attachments { get; set; }
        public decimal Chance { get; set; }
        public decimal QuantityPercent { get; set; }
        public int Max { get; set; }
        public int Min { get; set; }
        public BindingList<ExpansionLootVariant> Variants { get; set; }

        public ExpansionLoot(string name, BindingList<ExpansionLootVariant> attachments = null, decimal chance = 1, decimal quantityPercent = -1, BindingList<ExpansionLootVariant> variants = null, int max = -1, int min = 0)
        {
            Name = name;
            if (attachments != null)
                Attachments = attachments;
            else
                Attachments = new BindingList<ExpansionLootVariant>();
            Chance = chance;
            if (variants == null)
                Variants = new BindingList<ExpansionLootVariant>();
            else
                Variants = variants;

            QuantityPercent = quantityPercent;
            Max = max;
            Min = min;
        }
        public ExpansionLoot()
        {
            Chance = (decimal)0.25;
            QuantityPercent = -1;
            Max = -1;
            Min = 0;
            Attachments = new BindingList<ExpansionLootVariant>();
            Variants = new BindingList<ExpansionLootVariant>();
        }
        public override string ToString()
        {
            return Name;
        }

        public bool Equals(ExpansionLoot other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return Name == other.Name &&
                   Chance == other.Chance &&
                   QuantityPercent == other.QuantityPercent &&
                   Max == other.Max &&
                   Min == other.Min &&
                   ListsAreEqual(Attachments, other.Attachments) &&
                   ListsAreEqual(Variants, other.Variants);
        }
        public override bool Equals(object? obj) => Equals(obj as ExpansionLoot);
        private bool ListsAreEqual(BindingList<ExpansionLootVariant> list1, BindingList<ExpansionLootVariant> list2)
        {
            if (list1 == null || list2 == null)
                return list1 == list2;

            if (list1.Count != list2.Count)
                return false;

            for (int i = 0; i < list1.Count; i++)
            {
                if (!list1[i].Equals(list2[i]))
                    return false;
            }

            return true;
        }
        public ExpansionLoot Clone()
        {
            return new ExpansionLoot()
            {
                Name = this.Name,
                Chance = this.Chance,
                QuantityPercent = this.QuantityPercent,
                Max = this.Max,
                Min = this.Min,
                Attachments = new BindingList<ExpansionLootVariant>(this.Attachments.Select(a => a.Clone()).ToList()),
                Variants = new BindingList<ExpansionLootVariant>(this.Variants.Select(v => v.Clone()).ToList())
            };
        }
    }
    public class ExpansionLootVariant : IDeepCloneable<ExpansionLootVariant>, IEquatable<ExpansionLootVariant>
    {
        public string Name { get; set; }
        public BindingList<ExpansionLootVariant> Attachments { get; set; }
        public decimal Chance { get; set; }

        public ExpansionLootVariant(string _name, BindingList<ExpansionLootVariant> _Attachments = null, decimal _Chance = 1)
        {
            Name = _name;
            if (_Attachments != null)
                Attachments = _Attachments;
            else
                Attachments = new BindingList<ExpansionLootVariant>();
            Chance = _Chance;
        }
        public ExpansionLootVariant()
        {
            Chance = (decimal)0.2;
            Attachments = new BindingList<ExpansionLootVariant>();
        }

        public override string ToString()
        {
            return Name;
        }
        public bool Equals(ExpansionLootVariant other)
        {
            if(other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return Name == other.Name &&
                   Chance == other.Chance &&
                   ListsAreEqual(Attachments, other.Attachments);
        }
        public override bool Equals(object? obj) => Equals(obj as ExpansionLootVariant);
        private bool ListsAreEqual(BindingList<ExpansionLootVariant> list1, BindingList<ExpansionLootVariant> list2)
        {
            if (list1 == null || list2 == null)
                return list1 == list2;

            if (list1.Count != list2.Count)
                return false;

            for (int i = 0; i < list1.Count; i++)
            {
                if (!list1[i].Equals(list2[i]))
                    return false;
            }

            return true;
        }
        public ExpansionLootVariant Clone()
        {
            return new ExpansionLootVariant()
            {
                Name = this.Name,
                Chance = this.Chance,
                Attachments = new BindingList<ExpansionLootVariant>(this.Attachments.Select(a => a.Clone()).ToList())
            };
        }
    }
}
