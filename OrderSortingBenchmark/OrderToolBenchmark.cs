using BenchmarkDotNet.Attributes;
using OrderSorting;
using System.Collections.Generic;

namespace OrderSortingBenchmark
{
    //[CoreJob, CoreRtJob]
    //[RPlotExporter, RankColumn]
    public class OrderToolBenchmark
    {
        private List<(string SourceAddress, string DestAddress)> tupleOrders;
        private List<string[]> stringArrayOrders;

        public OrderToolBenchmark()
        {
            tupleOrders = new List<(string SourceAddress, string DestAddress)>();
            for (int j = 0; j < 22; j++)
            {
                var order = (SourceAddress: $"адрес № {j}", DestAddress: $"адрес № {j + 1}");
                tupleOrders.Add(order);
            }
            tupleOrders.Shuffle();


            stringArrayOrders = new List<string[]>();
            for (int j = 0; j < 22; j++)
            {
                var order = new[] { $"адрес № {j}", $"адрес № {j + 1}" };
                stringArrayOrders.Add(order);
            }
            stringArrayOrders.Shuffle();
        }

        [Benchmark]
        public void BenchmarkWithTupleData() => OrderRankingTool.Reorder(tupleOrders);

        [Benchmark]
        public void BenchmarkWithStringArrayData() => OrderRankingTool.Reorder(stringArrayOrders);
    }
}
