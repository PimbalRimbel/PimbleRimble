using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> : IPool where T : IPooledObject
{
    public List<IPooledObject> _objects;

    private int _activeObjects;

    public ObjectPool(int numElementos, Object objectPrefab)
    {
        _activeObjects = 0;
        _objects = new List<IPooledObject>(numElementos);

        for (int i = 0; i < numElementos; i++) // Crea instancias y agrégalas al pool
        {
            var instance = (IPooledObject)Object.Instantiate(objectPrefab);

            instance.Active = false; // Desactivar la bala
            _objects.Add(instance);
        }
    }


    public IPooledObject Get()
    {
        foreach (var obj in _objects)
        {
            if (!obj.Active)
            {
                obj.Active = true;
                _activeObjects += 1;
                return obj;
            }
        }
        return null; // Si no hay balas disponibles en el pool
    }

    public void Release(IPooledObject obj)
    {
        if (_objects.Contains(obj))
        {
            obj.Active = false;
            _activeObjects -= 1;

            obj.Reset();
        }
    }
}
