using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FwClient : MonoBehaviour
{
    public List<Transform> spawnPositions;
    public GameObject greenPigPrefab;
    public GameObject redPigPrefab;
    private FwFactory factory;

    private void Start()
    {
        factory = new FwFactory();
        //Creamos instancias flyweight para cada tipo de cerdo
        IFlyweight greenPig = factory.GetFlyweight("GreenPig", greenPigPrefab, new Vector3(1.5f, 1.5f, 1.5f));
        IFlyweight redPig = factory.GetFlyweight("RedPig", redPigPrefab, new Vector3(2, 2, 2));
        greenPig.SetSpeed(.7f);
        redPig.SetSpeed(1);
        greenPig.SetHealth(1);
        redPig.SetHealth(2);
        // Creamos los cerdos en diferentes posiciones
        for (int i = 0; i < spawnPositions.Count; i += 2)
        {
            greenPig.Display(spawnPositions[i].position, 1, .7f);
            redPig.Display(spawnPositions[i+1].position, 2, 1f);
        }
    }
}
