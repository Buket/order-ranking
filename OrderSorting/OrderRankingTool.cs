using System.Collections.Generic;
using System.Linq;

namespace OrderSorting
{
    public class OrderRankingTool
    {
        private Dictionary<string, BiDirectionalOrder> dict = new Dictionary<string, BiDirectionalOrder>();

        /// <summary>
        /// decision accept table
        /// when seek src and dst at dict
        /// 0 - dict item with given key was not found
        /// 1 - dict item with given key was not found
        /// 
        /// update refs => dict[src].next = dict[dst] и dict[dst].prev = dict[src]
        /// 
        /// src  dst
        ///  0    0   add both address in dict and update refs
        ///  0    1   add src address in dict and update refs
        ///  1    0   add dst address in dict and update refs
        ///  1    1   don`t add address, just update refs
        /// </summary>      
        private bool[] decision = new bool[2];

        /// <summary>
        /// 
        /// </summary>
        public OrderRankingTool()
        {
        }

        /// <summary>
        /// reorder orders for sequential route
        /// </summary>
        /// <param name="orders"></param>
        /// <returns></returns>
        public IList<(string SourceAddress, string DestAddress)> Reorder(List<(string SourceAddress, string DestAddress)> orders)
        {
            foreach (var o in orders)
            {
                decision[0] = dict.ContainsKey(o.SourceAddress);
                decision[1] = dict.ContainsKey(o.DestAddress);

                if (!decision[0] && !decision[1])
                {
                    dict.Add(o.SourceAddress, new BiDirectionalOrder(o.SourceAddress));
                    dict.Add(o.DestAddress, new BiDirectionalOrder(o.DestAddress));
                }

                if (decision[0] && !decision[1])
                {
                    dict.Add(o.DestAddress, new BiDirectionalOrder(o.DestAddress));
                }

                if (!decision[0] && decision[1])
                {
                    dict.Add(o.SourceAddress, new BiDirectionalOrder(o.SourceAddress));
                }

                dict[o.SourceAddress].Next = dict[o.DestAddress];
                dict[o.DestAddress].Prev = dict[o.SourceAddress];
            }

            var hkv = dict.Where(kv => kv.Value.Prev == null).ToList().FirstOrDefault();
            var head = hkv.Key;
            var rankedOrders = new List<(string SourceAddress, string DestAddress)>();

                        while (dict[head].Next != null)
            {
                rankedOrders.Add((
                    SourceAddress: dict[head].Street,
                    DestAddress: dict[head].Next.Street));
                head = dict[head].Next.Street;
            }

            return rankedOrders;
        }
    }
}
