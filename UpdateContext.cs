using System;
using System.Collections.Generic;

namespace csharp
{
    using Strategy = Action<Item>;
    /// <summary>
    /// UpdateContext is a class that handles how items change through time.
    /// </summary>
    public class UpdateContext
    {
        /// <summary>
        /// Updates the item according to the corresponding strategy
        /// </summary>
        /// <param name="item"></param>
        public void Update(Item item)
        {
            GetMatchingStrategy(item)?.Invoke(item);
        }

        readonly Dictionary<string, Strategy> TagsToStrategies = new Dictionary<string, Strategy>();
        /// <summary>
        /// Sets the update strategy for all the items that contain this specific string in their name
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="strategy"></param>
        public void SetStrategyForItemsWithTag(string tag, Strategy strategy)
        {
            TagsToStrategies[tag.ToLowerInvariant()] = strategy;
        }

        readonly Dictionary<string, Strategy> NamesToStrategies = new Dictionary<string, Strategy>();
        /// <summary>
        /// Sets the update strategy for all the items that have this name
        /// </summary>
        /// <param name="name"></param>
        /// <param name="strategy"></param>
        public void SetStrategyForAllItemsWithName(string name, Strategy strategy)
        {
            NamesToStrategies[name.ToLowerInvariant()] = strategy;
        }

        Strategy defaultStrategy;
        /// <summary>
        /// Sets the update strategy for any item that cannot be matched
        /// </summary>
        /// <param name="strategy"></param>
        public void SetDefaultStrategy(Strategy strategy)
        {
            defaultStrategy = strategy;
        }

        
        /* This function could be absorbed by ApplyUpdateStrategy, but since it helps
         * us check an item's strategy directly and more importantly reduces the
         * overall amount of code I chose to keep it as is
         */
        Strategy GetMatchingStrategy(Item item)
        {
            string itemName = item.Name.ToLowerInvariant();
            //try to find strategy for the item's full name
            if (NamesToStrategies.ContainsKey(itemName))
                return NamesToStrategies[itemName];
            //try to find a strategy for a specific part of the name
            foreach (KeyValuePair<string, Strategy> pair in TagsToStrategies)
                if (itemName.Contains(pair.Key))
                    return pair.Value;

            //return the default strategy, if any
            return defaultStrategy;
        }






    }
}
