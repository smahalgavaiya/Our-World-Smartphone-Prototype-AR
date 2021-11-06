namespace OurWorld.Scripts.Interfaces
{
    public interface IDataMapper<TOrigin,TTarget>
    {
         TTarget MapObject(TOrigin sourceObject);
    }
}