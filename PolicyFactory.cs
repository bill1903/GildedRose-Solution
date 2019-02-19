using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp
{
    using Policy = Action<Item>;
    /// <summary>
    /// Here you can find some generic policies and policy constructors
    /// </summary>
    class PolicyFactory
    {
        /// <summary>
        /// The item's quality degrades with double the degradation rate after it expires
        /// </summary>
        /// <param name="degradationRate"></param>
        /// <returns></returns>
        public static Policy CreateDegradablePolicy(int degradationRate)
        {
            return (item) => {
                UpdateItemExpiration(item);
                if (item.SellIn < 0)
                    item.Quality -= 2 * degradationRate;
                else
                    item.Quality -= degradationRate;
                ClampItemQuality(item);
            };
        }

        /// <summary>
        /// The item never degrades in quality or expires.
        /// </summary>
        public static readonly Policy LegendaryPolicy = (item) =>
        {
            //do nothing
        };

        /// <summary>
        /// The item's quality increases as time passes.
        /// </summary>
        public static readonly Policy CollectiblePolicy = (item) =>
        {
            UpdateItemExpiration(item);

            if (item.SellIn < 0)
                item.Quality += 2;
            else
                item.Quality++;

            ClampItemQuality(item);
        };

        public static readonly Policy EventPassPolicy = (item) =>
        {
            UpdateItemExpiration(item);

            if (item.SellIn < 0)
                item.Quality = MinQuality;
            else if (item.SellIn < 5)
                item.Quality += 3;
            else if (item.SellIn < 10)
                item.Quality += 2;
            else
                item.Quality++;

            ClampItemQuality(item);
        };

        static void UpdateItemExpiration(Item item)
        {
            item.SellIn--;
        }


        /// <summary>
        /// The maximum attainable quality for any non legendary item
        /// </summary>
        public const int MaxQuality = 50;
        /// <summary>
        /// The minimum quality of an item
        /// </summary>
        public const int MinQuality = 0;

        static void ClampItemQuality(Item item)
        {
            item.Quality = System.Math.Min(MaxQuality, System.Math.Max(MinQuality, item.Quality));
        }
    }
}
