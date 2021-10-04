using Newtonsoft.Json.Linq;

namespace FacuTheRock.Net.Algorithms.JsonCleanse.Implementation
{
    public interface IJsonCleanerCriteria
    {
        bool MustBeRemoved(string item);

        bool MustBeRemoved(JToken item);

        bool MustBeRemoved(JProperty item);
    }
}
