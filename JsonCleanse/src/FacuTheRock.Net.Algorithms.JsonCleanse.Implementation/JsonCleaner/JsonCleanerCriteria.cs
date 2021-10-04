using FacuTheRock.Net.Algorithms.JsonCleanse.Implementation.Helpers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace FacuTheRock.Net.Algorithms.JsonCleanse.Implementation
{
    public class JsonCleanerCriteria : IJsonCleanerCriteria
    {
        public static JsonCleanerCriteria Default = new();

        private readonly HashSet<string> _criteria;

        private readonly Func<string, bool> CheckNullOrEmpty;

        public JsonCleanerCriteria()
            : this(new HashSet<string>(), true)
        { }

        public JsonCleanerCriteria(bool checkNullOrEmpty)
            : this(new HashSet<string>(), checkNullOrEmpty)
        { }

        public JsonCleanerCriteria(
            HashSet<string> criteria,
            bool checkNullOrEmpty)
        {
            _criteria = criteria.ThrowIfNull();
            CheckNullOrEmpty = checkNullOrEmpty ?
                item => string.IsNullOrWhiteSpace(item) :
                item => false;
        }

        public void AddCriteria(string item) =>
            _criteria.Add(item);

        public void RemoveCriteria(string item) =>
            _criteria.Remove(item);

        public bool MustBeRemoved(JToken item) =>
            MustBeRemoved(item.ToString());

        public bool MustBeRemoved(JProperty item) =>
            MustBeRemoved(item.Value.ToString());

        public bool MustBeRemoved(string item) =>
            CheckNullOrEmpty(item) ||
            _criteria.Contains(item);
    }
}
