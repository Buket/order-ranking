﻿using System;
using System.Collections.Generic;
using System.Text;

namespace OrderSorting
{
    public static class ListExtension
    {
        private static Random rng = new Random();

        /// <summary>
        /// Перемешивает элементы в списке
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
