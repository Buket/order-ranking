using System.Collections.Generic;
using System.Linq;

namespace OrderSorting
{
    public class OrderRankingTool
    {
        /// <summary>
        /// reorder orders for sequential route
        /// </summary>
        /// <param name="orders"></param>
        /// <returns></returns>
        public static IList<(string SourceAddress, string DestAddress)> Reorder(List<(string SourceAddress, string DestAddress)> orders)
        {
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
            bool[] decision = new bool[2];
            var dict = new Dictionary<string, BiDirectionalOrder>();

            // O(n)
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

            //O(n)
            var hkv = dict.Where(kv => kv.Value.Prev == null).ToList().FirstOrDefault();
            var head = hkv.Key;
            var rankedOrders = new List<(string SourceAddress, string DestAddress)>();

            //O(n)
            while (dict[head].Next != null)
            {
                rankedOrders.Add((
                    SourceAddress: dict[head].Street,
                    DestAddress: dict[head].Next.Street));
                head = dict[head].Next.Street;
            }


            //total O(3n)
            return rankedOrders;
        }

        /// <summary>
        /// reorder orders for sequential route
        /// orders[i] - order where orders[i][0] source and orders[i][1] destination
        /// </summary>
        /// <param name="orders"></param>
        /// <returns></returns>
        public static IList<string[]> Reorder(IList<string[]> orders)
        {
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
            bool[] decision = new bool[2];
            var dict = new Dictionary<string, BiDirectionalOrder>(orders.Count);

            // O(n)
            foreach (var order in orders)
            {
                decision[0] = dict.ContainsKey(order[0]);
                decision[1] = dict.ContainsKey(order[1]);

                if (!decision[0] && !decision[1])
                {
                    dict.Add(order[0], new BiDirectionalOrder(order[0]));
                    dict.Add(order[1], new BiDirectionalOrder(order[1]));
                }

                if (decision[0] && !decision[1])
                {
                    dict.Add(order[1], new BiDirectionalOrder(order[1]));
                }

                if (!decision[0] && decision[1])
                {
                    dict.Add(order[0], new BiDirectionalOrder(order[0]));
                }

                dict[order[0]].Next = dict[order[1]];
                dict[order[1]].Prev = dict[order[0]];
            }

            //O(n)
            var hkv = dict.Where(kv => kv.Value.Prev == null).ToList().FirstOrDefault();
            var head = hkv.Key;
            var rankedOrders = new List<string[]>();

            //O(n)
            while (dict[head].Next != null)
            {
                var order = new string[2];
                order[0] = dict[head].Street;
                order[1] = dict[head].Next.Street;
                rankedOrders.Add(order);
                head = dict[head].Next.Street;
            }


            //total O(3n)
            return rankedOrders;
        }
    }
}
