using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FwFactory : MonoBehaviour
{
    private Dictionary<string, IFlyweight> flyweights = new Dictionary<string, IFlyweight>();

    public IFlyweight GetFlyweight(string key, GameObject prefab, Vector3 size)
    {
        if (!flyweights.ContainsKey(key))
        {
            flyweights[key] = new FwPig(prefab, size);
        }
        return flyweights[key];
    }
}
