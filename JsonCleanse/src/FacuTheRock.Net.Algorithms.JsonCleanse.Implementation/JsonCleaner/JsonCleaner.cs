using FacuTheRock.Net.Algorithms.JsonCleanse.Implementation.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FacuTheRock.Net.Algorithms.JsonCleanse.Implementation
{
    public class JsonCleaner : IJsonCleaner
    {
        private readonly IJsonCleanerCriteria _jsonCleanerCriteria;

        public JsonCleaner(IJsonCleanerCriteria jsonCleanerCriteria) =>
            _jsonCleanerCriteria = jsonCleanerCriteria;

        public string Clean(JObject jobject)
        {
            Clean(jobject as JToken);

            return JsonConvert.SerializeObject(jobject);
        }

        public string Clean(string json) =>
            Clean(JObject.Parse(json));

        private void Clean(JToken token)
        {
            switch (token.Type)
            {
                case JTokenType.Object:
                    CleanObject(token);
                    break;
                case JTokenType.Array:
                    CleanArray(token);
                    break;
                // Just as a good practice
                default:
                    break;
            }
        }

        private void CleanObject(JToken token)
        {
            token.Children<JProperty>()
                .RemoveFromToken(_jsonCleanerCriteria.MustBeRemoved);

            Clean(token.Children<JProperty>());
        }

        private void CleanArray(JToken token)
        {
            token.Children<JToken>()
                .RemoveFromToken(_jsonCleanerCriteria.MustBeRemoved);

            Clean(token.Children());
        }

        private void Clean(JEnumerable<JProperty> tokens)
        {
            foreach (var prop in tokens)
                Clean(prop.Value);
        }

        private void Clean(JEnumerable<JToken> tokens)
        {
            foreach (var prop in tokens)
                Clean(prop);
        }
    }
}
