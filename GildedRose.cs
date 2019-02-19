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
            updateContext.SetDefaultStrategy(StrategyFactory.CreateProgressiveQualityChangeStrategy(-1));
            updateContext.SetStrategyForItemsWithTag("Sulfuras", null);
            updateContext.SetStrategyForItemsWithTag("Aged Brie", StrategyFactory.CreateProgressiveQualityChangeStrategy(1));
            updateContext.SetStrategyForItemsWithTag("Backstage Passes", StrategyFactory.EventPassStrategy);
            updateContext.SetStrategyForItemsWithTag("Conjured", StrategyFactory.CreateProgressiveQualityChangeStrategy(-2));
        }


        public void UpdateQuality()
        {
            foreach(var item in Items)
                updateContext.Update(item);
        }
    }
}
