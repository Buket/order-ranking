using NUnit.Framework;
using OrderSorting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderSortingTests
{
    [TestFixture]
    public class OrderToolUnitTests
    {
        private List<(string SourceAddress, string DestAddress)> orders;

        [SetUp]
        public void Init()
        {
            orders = new List<(string SourceAddress, string DestAddress)>
            {
                ("Южный", "Печатники"),
                ("Текстильщики", "Нижегородний"),
                ("Печатники", "Текстильщики")
            };
        }

        [Test]
        public void CountNotChangedTest()
        {
            var indexer = new OrderRankingTool();
            var rankedOrders = indexer.Reorder(orders);
            Assert.AreEqual(orders.Count, rankedOrders.Count);
        }

        [Test]
        public void CorrectlyReorderedFirstAddressTest()
        {
            var indexer = new OrderRankingTool();
            var rankedOrders = indexer.Reorder(orders);
            Assert.True(rankedOrders.First().SourceAddress.Equals("Южный"));
        }

        [Test]
        public void CorrectlyReorderedLastAddressTest()
        {
            var indexer = new OrderRankingTool();
            var rankedOrders = indexer.Reorder(orders);
            Assert.True(rankedOrders.Last().DestAddress.Equals("Нижегородний"));
        }


        [TestCase(10)]
        [TestCase(1000)]
        [TestCase(100000)]
        public void CanReorderManyOrdersCaseTest(int count)
        {
            var indexer = new OrderRankingTool();

            var @case = new List<(string SourceAddress, string DestAddress)>();
            
            for (int j = 0; j < count; j++)
            {
                var order = (SourceAddress: $"адрес № {j}", DestAddress: $"адрес № {j + 1}");
                @case.Add(order);
            }
            @case.Shuffle();

            var rankedOrders = indexer.Reorder(@case);
            Assert.True(rankedOrders.First().SourceAddress.Equals("адрес № 0"));
            Assert.True(rankedOrders.Last().DestAddress.Equals($"адрес № {count}"));
        }
    }
}
