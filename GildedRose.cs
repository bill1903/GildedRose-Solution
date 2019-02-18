using System;
using System.Collections.Generic;

namespace csharp
{
    public class GildedRose
    {
        IList<Item> Items;
        UpdateProtocol updateProtocol;
        public GildedRose(IList<Item> Items)
        {
            this.Items = Items;

            updateProtocol = new UpdateProtocol();
            
            updateProtocol.SetDefaultPolicy(GlobalPolicies.DegradableItemPolicy(1));
            updateProtocol.SetPolicyForItemsWithTag("Sulfuras", GlobalPolicies.LegendaryPolicy);
            updateProtocol.SetPolicyForItemsWithTag("Aged Brie", GlobalPolicies.CollectiblePolicy);
            updateProtocol.SetPolicyForItemsWithTag("Backstage Passes", GlobalPolicies.EventPassPolicy);
            updateProtocol.SetPolicyForItemsWithTag("Conjured", GlobalPolicies.DegradableItemPolicy(2));
        }


        public void UpdateQuality()
        {
            foreach(var item in Items)
                updateProtocol.ApplyUpdatePolicy(item);
        }
    }
}
