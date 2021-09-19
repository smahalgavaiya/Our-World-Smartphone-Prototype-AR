namespace Our_World_Smartphone_Prototype_AR.Assets._Game.Scripts.Interfaces
{
    public interface IDataMapper<TOrigin,TTarget>
    {
         TTarget MapObject(TOrigin sourceObject);
    }
}