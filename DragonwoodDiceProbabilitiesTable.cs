using System;
using System.Collections.Generic;
using System.Linq;

namespace DragonwoodDice
{
    class Program
    {
        const int REPS = 20000000;
        static void Main(string[] args)
        {
            int[] sides = { 1, 2, 2, 3, 3, 4 };
            Random r = new Random();

            for (int numDice = 1; numDice <= 6; numDice++)
            {
                Dictionary<int, int> outcomes = new Dictionary<int, int>();

                for (int i = 0; i < 6 * numDice; i++)
                {
                    outcomes[i] = 0;
                }

                for (int i = 0; i < REPS; i++)
                {
                    int count = 0;
                    for (int roll = 0; roll < numDice; roll++)
                    {
                        count += sides[r.Next(sides.Length)];
                    }
                    outcomes[count] = outcomes[count] + 1;
                }
                Console.WriteLine("## Number of dice: " + numDice);
                double cumulative = 0;
                Console.WriteLine("|Sum of roll|P(Exact Sum)|P(>= sum)|");
                Console.WriteLine("|-----|------|------|");
                foreach (var outcome in outcomes.Where(a => a.Value != 0).OrderBy(a => a.Key))
                {
                    double pOfSpecificRoll = (100.0 * outcome.Value) / (double)REPS;
                    Console.WriteLine("|{0,5}|{1,6:F1}|{2,6:F1}|", outcome.Key, pOfSpecificRoll, 100.0 - cumulative);
                    cumulative += pOfSpecificRoll;
                }
                Console.WriteLine();
            }
        }
    }
}
