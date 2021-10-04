using System;
using System.Collections.Generic;

namespace FacuTheRock.Net.Algorithms.JsonCleanse.Implementation
{
    class Program
    {
        private const string JSON =
            @"{
                'name': {
                'first': 'Robert',
                'middle': '',
                'last': 'Smith'
                },
                'age': 25,
                'DOB': '-',
                'hobbies': [
                'running',
                'coding',
                '-'
                ],
                'education': {
                'highschool': 'N/A',
                'college': 'Yale'
                }
            }";

        private static readonly HashSet<string> _criteria = new()
        {
            "N/A",
            "-"
        };

        static void Main()
        {
            var jsonCleanerCriteria = new JsonCleanerCriteria(_criteria, true);
            var jsonCleaner = new JsonCleaner(jsonCleanerCriteria);

            var jsonResult = jsonCleaner.Clean(JSON);

            Console.WriteLine(jsonResult);
            Console.ReadLine();
        }
    }
}
