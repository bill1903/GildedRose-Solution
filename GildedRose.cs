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
            updateProtocol.SetDefaultPolicy(PolicyFactory.CreateDegradablePolicy(1));
            updateProtocol.SetPolicyForItemsWithTag("Sulfuras", PolicyFactory.LegendaryPolicy);
            updateProtocol.SetPolicyForItemsWithTag("Aged Brie", PolicyFactory.CollectiblePolicy);
            updateProtocol.SetPolicyForItemsWithTag("Backstage Passes", PolicyFactory.EventPassPolicy);
            updateProtocol.SetPolicyForItemsWithTag("Conjured", PolicyFactory.CreateDegradablePolicy(2));
        }


        public void UpdateQuality()
        {
            foreach(var item in Items)
                updateProtocol.ApplyUpdatePolicy(item);
        }
    }
}
