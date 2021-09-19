namespace OurWorld.Scripts.Interfaces.Serialization
{
    public interface ISerializationOption
    {
         public string ContentType {get;}

         T Deserialize<T>(string textToSerialize);
    }
}