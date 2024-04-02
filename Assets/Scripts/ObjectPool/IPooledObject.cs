
public interface IPooledObject
{
    public bool Active
    {
        get;
        set;
    }

    public void Reset();
}
