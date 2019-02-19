using System;

namespace csharp
{
    using Strategy = Action<Item>;
    /// <summary>
    /// Here you can find some generic strategies and strategy constructors
    /// </summary>
    class StrategyFactory
    {
        /// <summary>
        /// The item's quality changes twice as fast after its sell in date
        /// </summary>
        /// <param name="qualityChangeRate"></param>
        /// <returns></returns>
        public static Strategy CreateProgressiveQualityChangeStrategy(int qualityChangeRate)
        {
            return (item) => {
                UpdateItemExpiration(item);
                if (item.SellIn < 0)
                    item.Quality += 2 * qualityChangeRate;
                else
                    item.Quality += qualityChangeRate;
                ClampItemQuality(item);
            };
        }

        public static readonly Strategy EventPassStrategy = (item) =>
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
