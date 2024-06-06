using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class Bullet : MonoBehaviour, IPooledObject
{
    public EventHandler balaColisionada;

    public bool Active 
    {
        get
        {
            return gameObject.activeSelf;
        }

        set
        {
            gameObject.SetActive(value);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Si la bala colisiona con un objeto distinto al jugador, desactivar la bala y devolverla al pool
        if (!other.CompareTag("Player"))
        {
            balaColisionada?.Invoke(this, null);
        }
    }

    public void Reset()
    {
        
    }

}