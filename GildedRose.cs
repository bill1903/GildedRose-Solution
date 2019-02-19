using System;
using System.Collections.Generic;

namespace csharp
{
    public class GildedRose
    {
        IList<Item> Items;
        UpdateContext updateContext;
        public GildedRose(IList<Item> Items)
        {
            this.Items = Items;

            updateContext = new UpdateContext();
            updateContext.SetDefaultStrategy(StrategyFactory.CreateDegradableStrategy(1));
            updateContext.SetStrategyForItemsWithTag("Sulfuras", null);
            updateContext.SetStrategyForItemsWithTag("Aged Brie", StrategyFactory.CollectibleStrategy);
            updateContext.SetStrategyForItemsWithTag("Backstage Passes", StrategyFactory.EventPassStrategy);
            updateContext.SetStrategyForItemsWithTag("Conjured", StrategyFactory.CreateDegradableStrategy(2));
        }


        public void UpdateQuality()
        {
            foreach(var item in Items)
                updateContext.Update(item);
        }
    }
}
