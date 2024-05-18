using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D Rigidbody2D;

    private int numero = 0;


    //PATRÓN OBSERVER
    private List<IObserver<float>> observers = new List<IObserver<float>>();

    public void AddObserver (IObserver<float> observer) //agregamos
    {
        observers.Add(observer);
    }

    public void RemoveObserver (IObserver<float> observer) //eliminamos
    {
        observers.Remove(observer);
    }

    public void NotifyObserver()
    {
        foreach(IObserver<float> observer in observers)
        {
            observer?.UpdateObserver(numero);
        }
    }


    public void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
