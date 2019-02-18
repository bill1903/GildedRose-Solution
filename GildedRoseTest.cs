using NUnit.Framework;
using System.Collections.Generic;

namespace csharp
{
    [TestFixture]
    public class GildedRoseTest
    {
        //Template for testing a single item
        /*
        [Test]
        public void foo()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "foo", SellIn = 0, Quality = 0 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual("fixit", Items[0].Name);
        }*/

        void TestQualityProgression(string name, int sellIn, int quality, int[] qualityProgression)
        {
            IList<Item> Items = new List<Item> {
                new Item { Name = name, SellIn = sellIn, Quality = quality }
            };

            GildedRose app = new GildedRose(Items);

            for (int i = 0; i < qualityProgression.Length; i++)
            {
                app.UpdateQuality();
                Assert.AreEqual(qualityProgression[i], Items[0].Quality);
            }
        }

        [Test]
        public void NonSpecialItem()
        {
            int[] qualityProgression =
                {7, 6, 5, 4, 3, 2, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};

            TestQualityProgression("normal item", 12, 8, qualityProgression);

        }

        [Test]
        public void ConjuredItem()
        {
            int[] qualityProgression =
                {6, 4, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};

            TestQualityProgression("Conjured Mana Cake", 12, 8, qualityProgression);

        }

        [Test]
        public void ConjuredItemQualityAfterSaleDate()
        {
            int[] qualityProgression =
                {16, 14, 12, 8, 4, 0, 0};

            TestQualityProgression("Conjured Mana Cake", 3, 18, qualityProgression);

        }

        [Test]
        public void QualityAfterSaleDate()
        {
            int[] qualityProgression =
                {7, 6, 5, 3, 1, 0, 0};

            TestQualityProgression("normal item", 3, 8, qualityProgression);

        }

        [Test]
        public void Sulfuras()
        {
            int[] qualityProgression =
                {80, 80, 80, 80, 80, 80, 80, 80, 80, 80, 80, 80, 80, 80, 80, 80, 80};
            TestQualityProgression("Sulfuras, Hand of Ragnaros", 12, 80, qualityProgression);
            
        }

        [Test]
        public void AgedBrie()
        {

            int[] qualityProgression =
                {39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 50, 50, 50, 50};
            TestQualityProgression("Aged Brie", 12, 38, qualityProgression);
        }

        [Test]
        public void AgedBrieAfterSellDate()
        {

            int[] qualityProgression =
                {29, 30, 31, 32, 33, 34, 35, 36, 38, 40, 42, 44, 46, 48, 50, 50};
            TestQualityProgression("Aged Brie", 8, 28, qualityProgression);
        }


        [Test]
        public void BackstagePasses()
        {
            
            int[] qualityProgression = {
                25,     //12 days left
                26,     //11 days left
                28,     //10 days left
                30,     //9 days left
                32,     //8 days left
                34,     //7 days left
                36,     //6 days left
                39,     //5 days left
                42,     //4 days left
                45,     //3 days left
                48,     //2 days left
                50,     //1 days left
                0,      //Concert day
                0,      //After festival
                0,      //After festival
                0       //After festival


            };
            TestQualityProgression("Backstage passes to a TAFKAL80ETC concert", 12, 24, qualityProgression);


        }
    }
}
