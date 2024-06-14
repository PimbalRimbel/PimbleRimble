using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FwPig : IFlyweight
{
    int health;
    float speed;
    private GameObject pigPrefab;
    private Vector3 size;

    public FwPig(GameObject pigPrefab, Vector3 size/*, int health, float speed*/)
    {
        /*this.health = health;
        this.speed = speed;*/
        this.pigPrefab = pigPrefab;
        this.size = size;
    }
    public void SetHealth(int h)
    {
        health = h;
    }
    public void SetSpeed(float s)
    {
        speed = s;
    }
    public int GetHealth()
    {
        return health;
    }
    public float GetSpeed()
    {
        return speed;
    }

    public void Display(Vector3 position, int health, float speed){
        GameObject pig = GameObject.Instantiate(pigPrefab, position, Quaternion.identity);
        pig.transform.localScale = size;

        Pig pigComponent = pig.AddComponent<Pig>(); 
        pigComponent.health = health;
        pigComponent.speed = speed;
        Debug.Log($"Creo cerdo {health}h {speed}v");
    }
}
