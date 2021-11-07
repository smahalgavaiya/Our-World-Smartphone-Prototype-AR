using System.Collections.Generic;

namespace OurWorld.Scripts.Interfaces
{
    public interface IBatchRequest
    {
        IEnumerable<string> GetRequestMultipleURLParameters();
    }
}