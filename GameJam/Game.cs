﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GameJam
{
    class Game
    {
        public List<Currency> allCurrencies = new List<Currency>();
        public List<Trend> allTrends = new List<Trend>();
        List<List<Currency>> historicalValues = new List<List<Currency>>();

        static Random r = new Random();

        public Dictionary<Currency, int> ownedMetals = new Dictionary<Currency, int>();
        public double money = 150;

        public Game()
        {
            allCurrencies.Add(new Currency("Gold", 15, 25, 15, 25));
            allCurrencies.Add(new Currency("Silver", 10, 15, 10, 15));
            allTrends.Add(new Trend(r.Next(1, 10), allCurrencies[0], nextDouble(0.9, 1.1)));
            allTrends.Add(new Trend(r.Next(1, 10), allCurrencies[1], nextDouble(0.9, 1.1)));
            ownedMetals.Add(allCurrencies[0], 0);
            ownedMetals.Add(allCurrencies[1], 0);
        }

        public void advanceTimeStep()
        {
            for (int i = 0; i < allTrends.Count-1; i++)
            {
                allTrends[i].nextTick();
                if (allTrends[i].tickDone())
                {
                    allTrends[i] = new Trend(r.Next(1, 10), allTrends[i].Currency, nextDouble(0.9, 1.1));
                }
            }

            List<Currency> hValues = new List<Currency>();
            foreach (var item in allCurrencies)
            {
                hValues.Add(new Currency(item.Name,
                                         item.Value,
                                         item.MinValue,
                                         item.MaxValue,
                                         0,
                                         0
                                         ));
                item.newRandomValue();
            }
            historicalValues.Add(hValues);
        }

        public static double nextDouble(double min, double max)
        {
            double random = r.NextDouble();
            return min + random * (max - min);
        }

        public void buy(Currency cBuy, int amount)
        {
            money -= cBuy.Value * amount;
            ownedMetals[cBuy] += amount;
        }

        public void sell(Currency cSell, int amount)
        {
            money += cSell.Value * amount;
            ownedMetals[cSell] += amount;
        }
    }
}
