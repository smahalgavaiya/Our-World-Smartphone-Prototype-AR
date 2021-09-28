using Mapbox.Json;
using Mapbox.Utils.JsonConverters;
using OurWorld.Scripts.Interfaces.Serialization;

namespace OurWorld.Scripts.Utilities.DataSerializers
{
    public class MapBoxNewtonsoftJsonSerializerOption : ISerializationOption
    {
        public string ContentType => "application/json";

        public T Deserialize<T>(string textToSerialize)
        {
             try{
                var deserialized = JsonConvert.DeserializeObject<T>(textToSerialize,JsonConverters.Converters);

                Debug.Log($"Deserialization Success : {textToSerialize}");

                return deserialized;
            }
            catch (JsonSerializationException ex){
                Debug.LogError($"Deserialization Failed. Error : {ex.Message}");
                return default;
            }
        }
    }
}