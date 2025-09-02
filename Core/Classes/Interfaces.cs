using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Day2eEditor
{
    /// <summary>
    /// Interfaces for Spawn Gear
    /// </summary>
    public interface IHasSpawnWeight
    {
        int SpawnWeight { get; set; }
    }
    public interface IHasSpawnItemType
    {
        string ItemType { get; set; }
    }
    public interface IHasSpawnName
    {
        string Name { get; set; }
    }
    public interface IHasQuikBarSlot
    {
        int QuickBarSlot { get; set; }
    }
    public interface IHasSimpleChildren
    {
        BindingList<string> SimpleChildrenTypes { get; set; }
    }
    public interface IHassimpleChildrenUseDefaultAttributes
    {
        bool SimpleChildrenUseDefaultAttributes { get; set; }
    }

}
