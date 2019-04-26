using NUnit.Framework;
using OrderSorting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderSortingTests
{
    [TestFixture]
    public class OrderToolArrayUnitTests
    {
        [Test]
        public void CountNotChangedTest()
        {
            var orders = new List<string[]>
            {
                new []{ "Южный", "Печатники"},
                new []{"Текстильщики", "Нижегородний" },
                new []{ "Печатники", "Текстильщики"}
            };
            var rankedOrders = OrderRankingTool.Reorder(orders);
            Assert.AreEqual(orders.Count, rankedOrders.Count);
        }

        [Test]
        public void CorrectlyReorderedFirstAddressTest()
        {
            var orders = new List<string[]>
            {
                new []{ "Южный", "Печатники"},
                new []{ "Текстильщики", "Нижегородний" },
                new []{ "Печатники", "Текстильщики"}
            };
            var rankedOrders = OrderRankingTool.Reorder(orders);
            Assert.True(rankedOrders.First()[0].Equals("Южный"));
        }

        [Test]
        public void CorrectlyReorderedLastAddressTest()
        {
            var orders = new List<string[]>
            {
                new []{ "Южный", "Печатники"},
                new []{"Текстильщики", "Нижегородний" },
                new []{ "Печатники", "Текстильщики"}
            };
            var rankedOrders = OrderRankingTool.Reorder(orders);
            Assert.True(rankedOrders.Last()[1].Equals("Нижегородний"));
        }


        [TestCase(10)]
        [TestCase(1000)]
        [TestCase(10000)]
        public void CanReorderManyOrdersCaseTest(int count)
        {
            var @case = new List<(string SourceAddress, string DestAddress)>();
            
            for (int j = 0; j < count; j++)
            {
                var order = (SourceAddress: $"адрес № {j}", DestAddress: $"адрес № {j + 1}");
                @case.Add(order);
            }
            @case.Shuffle();

            var rankedOrders = OrderRankingTool.Reorder(@case);
            Assert.True(rankedOrders.First().SourceAddress.Equals("адрес № 0"));
            Assert.True(rankedOrders.Last().DestAddress.Equals($"адрес № {count}"));
        }
    }
}
