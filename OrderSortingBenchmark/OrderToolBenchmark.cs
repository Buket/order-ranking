using BenchmarkDotNet.Attributes;
using OrderSorting;
using System.Collections.Generic;

namespace OrderSortingBenchmark
{
    //[CoreJob, CoreRtJob]
    //[RPlotExporter, RankColumn]
    public class OrderToolBenchmark
    {
        private OrderRankingTool indexer = new OrderRankingTool();
        private List<List<(string SourceAddress, string DestAddress)>> testsOrders;

        
        public OrderToolBenchmark()
        {
            testsOrders = new List<List<(string SourceAddress, string DestAddress)>>();
            //10 delivers
            for (int i = 0; i < 10; i++)
            {
                //100 address
                var @case = new List<(string SourceAddress, string DestAddress)>();
                for (int j = 0; j < 12; j++)
                {
                    var order = (SourceAddress:$"адрес № {j}", DestAddress:$"адрес № {j+1}");
                    @case.Add(order);
                }
                @case.Shuffle();
                testsOrders.Add(@case);
            }
        }

        [ParamsSource(nameof(ValuesForInput))]
        public List<(string SourceAddress, string DestAddress)> Input { get; set; }

        // public property
        public IEnumerable<List<(string SourceAddress, string DestAddress)>> ValuesForInput => testsOrders;

        [Benchmark]
        public void Benchmark1() => indexer.Reorder(Input);
    }
}
