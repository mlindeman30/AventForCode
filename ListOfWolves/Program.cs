using System;
using System.Collections.Generic;
using System.Linq;

namespace ListOfWolves
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Top { get; set; }
        public int Length => Name.Length;
    }

    public static class Program
    {
        public static void Main()
        {
            var people = new List<Person>
            {
                new Person { Id = 1, Name = "Celine" },
                new Person { Id = 2, Name = "Govert" },
                new Person { Id = 3, Name = "Amy" },
                new Person { Id = 4, Name = "Lynn" },
                new Person { Id = 5, Name = "Mélanie" },
                new Person { Id = 6, Name = "Dorianne" },
                new Person { Id = 7, Name = "Lisa" },
                new Person { Id = 8, Name = "Nina" },
                new Person { Id = 9, Name = "Michael" },
                new Person { Id = 10, Name = "Daisy" },
                new Person { Id = 11, Name = "Milan" },
                new Person { Id = 12, Name = "Joey" },
                new Person { Id = 13, Name = "Thijs" },
                new Person { Id = 14, Name = "Marleen" },
                new Person { Id = 15, Name = "Rick" },
                new Person { Id = 16, Name = "Eleonora" },
                new Person { Id = 17, Name = "Nienke" }
            };

            // Generate all combinations of 5 people:
            var allFiveCombos = GetCombinations(people, 5);

            // Scenario #1: Top = true if Id < 9, false otherwise
            var scenario1 = new List<List<Person>>();
            foreach (var combo in allFiveCombos)
            {
                var updated = combo
                    .Select(p => new Person
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Top = p.Id < 9
                    })
                    .ToList();
                scenario1.Add(updated);
            }

            // Scenario #2: Top = true if Id <= 9, false otherwise
            var scenario2 = new List<List<Person>>();
            foreach (var combo in allFiveCombos)
            {
                var updated = combo
                    .Select(p => new Person
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Top = p.Id <= 9
                    })
                    .ToList();
                scenario2.Add(updated);
            }

            // Scenario #3: ignore all combos where id = 9 is included
            var scenario3 = new List<List<Person>>();
            foreach (var combo in allFiveCombos.Where(x=> !x.Any(y => y.Id == 9)))
            {
                var updated = combo
                    .Select(p => new Person
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Top = p.Id <= 9
                    })
                    .ToList();
                scenario3.Add(updated);
            }

            var scenario1results = scenario1
                .Where(CheckComboConditions)
                .ToList();

            var scenario2results = scenario2
                .Where(CheckComboConditions)
                .ToList();

            var scenario3results = scenario2
                .Where(CheckComboConditions)
                .ToList();

            // Example: Check and report how many combos pass the conditions in each scenario
            Console.WriteLine("Scenario #1 valid combos: " +
                scenario1results.Count());
            Console.WriteLine("Scenario #2 valid combos: " +
                scenario2results.Count());
            Console.WriteLine("Scenario #2 valid combos: " +
                scenario3results.Count());

            // You can also print the valid combos if needed
            Console.WriteLine("Valid combos for Scenario #1:");
            foreach (var combo in scenario1results)
            {
                Console.WriteLine(string.Join(", ", combo.Select(p => p.Name)));
            }

            Console.WriteLine("Valid combos for Scenario #2:");
            foreach (var combo in scenario2results)
            {
                Console.WriteLine(string.Join(", ", combo.Select(p => p.Name)));
            }

            Console.WriteLine("Valid combos for Scenario #3:");
            foreach (var combo in scenario3results)
            {
                Console.WriteLine(string.Join(", ", combo.Select(p => p.Name)));
            }

            //check for overlapping combinations
            var scenario1resultslist = scenario1results.Where(x => scenario2results.Any(y => x.All(z => y.Any(a => z.Id == a.Id)))).ToList();
            var scenario2resultslist = scenario2results.Where(x => scenario1results.Any(y => x.All(z => y.Any(a => z.Id == a.Id)))).ToList();

            var overlappingCombos = scenario1resultslist.Intersect(scenario2resultslist).ToList();

            Console.WriteLine("Overlapping combinations between scenarios: " + overlappingCombos.Count);
            foreach (var combo in overlappingCombos)
            {
                Console.WriteLine(string.Join(", ", combo.Select(p => p.Name)));
            }
        }

        /// <summary>
        /// Checks if a combo of people meets the specified conditions:
        /// 1) The first and last are not both wolves.
        /// 2) The smallest interval between any two wolves is at least 2 players in between.
        /// 3) More wolves in bottom half than in top half of the list.
        /// 4) More wolves on even positions (1-based) than on odd.
        /// 5) Wolves collectively have 32 letters in their names.
        /// </summary>
        public static bool CheckComboConditions(List<Person> combo)
        {
            if (combo.Count < 2) return false;

            // 1) First and last not both wolves need to check
            if (combo.Any(x => x.Id == 1) && combo.Any(x => x.Id == 17)) 
                return false;

            // 2) Smallest interval between two wolves is at least 2 players in between
            var sortedIds = combo.Select(x => x.Id).OrderBy(x => x).ToList();
            for (int i = 0; i < sortedIds.Count - 1; i++)
            {
                // Must have at least 2 players between them => ID difference >= 2
                if (sortedIds[i + 1] - sortedIds[i] < 3)
                    return false;
            }

            // 3) More wolves in bottom half than top half
            int half = combo.Count / 2; // for 5, first 2 are "top half", last 3 "bottom half"
            var topHalf = combo.Count(p => p.Top);
            var bottomHalf = combo.Count(p => !p.Top);
            if (bottomHalf <= topHalf) 
                return false;

            // 4) More wolves on even positions (1-based) than odd
            int wolvesEven = combo
                .Where((p) => (p.Id % 2) == 0)
                .Count();
            int wolvesOdd = combo
                .Where(p => (p.Id % 2) == 1)
                .Count();
            if (wolvesEven <= wolvesOdd) 
                return false;

            // 5) Wolves collectively have 32 letters in their names
            int sumLetters = combo.Sum(p => p.Length);
            if (sumLetters != 32) 
                return false;

            return true;
        }

        private static IEnumerable<List<Person>> GetCombinations(List<Person> people, int size)
        {
            // A simple recursive approach to yield all combinations of 'size' elements
            if (size == 0)
            {
                yield return new List<Person>();
                yield break;
            }

            for (int i = 0; i < people.Count; i++)
            {
                var head = people[i];
                var tail = people.GetRange(i + 1, people.Count - (i + 1));

                foreach (var tailCombo in GetCombinations(tail, size - 1))
                {
                    tailCombo.Insert(0, head);
                    yield return tailCombo;
                }
            }
        }
    }
}


