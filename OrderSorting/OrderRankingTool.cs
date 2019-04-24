using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrderSorting
{
    public class OrderRankingTool
    {
        private Dictionary<string, BiDirectionalOrder> dict = new Dictionary<string, BiDirectionalOrder>();

        /// <summary>
        /// таблица принятия решений
        /// при поиске src и dst в dict
        /// 0 - элемент с указанным key не найден
        /// 1 - элемент с указанным key найден
        /// 
        /// обновляем свзяи => dict[src].next = dict[dst] и dict[dst].prev = dict[src]
        /// 
        /// src  dst
        ///  0    0   добавляем оба адреса в dict и обновляем свзяи
        ///  0    1   добавляем адрес src в dict и обновляем свзяи
        ///  1    0   добавляем адрес dst в dict и обновляем свзяи
        ///  1    1   обновляем свзяи
        /// </summary>      
        private bool[] decision = new bool[2];

        /// <summary>
        /// 
        /// </summary>
        public OrderRankingTool()
        {
        }

        /// <summary>
        /// упордочивает заказы для последовательного маршрута
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
