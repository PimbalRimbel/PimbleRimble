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
        //AuxGet(aux, index);
        int index = _objects.IndexOf(obj);
        if (!_objects[index].Active)
        {
            _objects[index].Active = true;
            _activeObjects += 1;
        }
    }

    public void Release(IPooledObject obj)
    {
        int index = _objects.IndexOf(obj);
        _objects[index].Active = false;
        _activeObjects -= 1;
        _objects[index].Reset();
        //Get(_objects[index],index);
    }

    private void AuxGet(IPooledObject aux, int index)
    {
        //yield return new WaitForSeconds(3f);
        if (!_objects[index].Active)
        {
            _objects[index].Active = true;
            _activeObjects += 1;
        }
    }
}
