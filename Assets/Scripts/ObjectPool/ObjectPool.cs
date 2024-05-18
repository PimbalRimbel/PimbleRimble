using System.Collections.Generic;


public class ObjectPool : IPool
{
    public List<IPooledObject> _objects;

    private int _activeObjects;

    public ObjectPool()
    {
        _activeObjects = 0;
        _objects = new List<IPooledObject>(5);
    }


    public void Get(IPooledObject obj)
    {
        if (!_objects.Contains(obj))
        {
            _objects.Add(obj);
        }

        if (!obj.Active)
        {
            obj.Active = true;
            _activeObjects += 1;
            ((Bullet)obj).gameObject.SetActive(true); // Activar el GameObject
        }
    }

    public void Release(IPooledObject obj)
    {
        if (_objects.Contains(obj))
        {
            obj.Active = false;
            _activeObjects -= 1;
            ((Bullet)obj).Reset();
        }
    }
}
