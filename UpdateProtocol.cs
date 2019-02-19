using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp
{
    using Policy = Action<Item>;
    /// <summary>
    /// UpdateProtocol is a class that handles how items change through time.
    /// </summary>
    public class UpdateProtocol
    {
        /// <summary>
        /// Updates the item according to the corresponding policy
        /// </summary>
        /// <param name="item"></param>
        public void ApplyUpdatePolicy(Item item)
        {
            GetMatchingPolicy(item)?.Invoke(item);
        }

        readonly Dictionary<string, Policy> TagsToPolicies = new Dictionary<string, Policy>();
        /// <summary>
        /// Sets the update policy for all the items that contain this specific string in their name
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="policy"></param>
        public void SetPolicyForItemsWithTag(string tag, Policy policy)
        {
            TagsToPolicies[tag.ToLowerInvariant()] = policy;
        }

        readonly Dictionary<string, Policy> NamesToPolicies = new Dictionary<string, Policy>();
        /// <summary>
        /// Sets the update policy for all the items that have this name
        /// </summary>
        /// <param name="name"></param>
        /// <param name="policy"></param>
        public void SetPolicyForAllItemsWithName(string name, Policy policy)
        {
            NamesToPolicies[name.ToLowerInvariant()] = policy;
        }

        Policy defaultPolicy;
        /// <summary>
        /// Sets the update policy for any item that cannot be matched to any other policy
        /// </summary>
        /// <param name="policy"></param>
        public void SetDefaultPolicy(Policy policy)
        {
            defaultPolicy = policy;
        }

        
        /* This function could be absorbed by ApplyUpdatePolicy, but since it helps
         * us check an item's policy directly and more importantly reduces the
         * overall amount of code I chose to keep it as is
         */
        Policy GetMatchingPolicy(Item item)
        {
            string itemName = item.Name.ToLowerInvariant();
            //try to find policy for the item's full name
            if (NamesToPolicies.ContainsKey(itemName))
                return NamesToPolicies[itemName];
            //try to find a policy for a specific part of the name
            foreach (KeyValuePair<string, Policy> pair in TagsToPolicies)
                if (itemName.Contains(pair.Key))
                    return pair.Value;

            //return the default policy, if any
            return defaultPolicy;
        }






    }
}
