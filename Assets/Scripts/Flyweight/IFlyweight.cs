using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFlyweight
{
    void Display(Vector3 position, int health, float speed);
    void SetHealth(int h);
    void SetSpeed(float s);
    int GetHealth();
    float GetSpeed();
}
