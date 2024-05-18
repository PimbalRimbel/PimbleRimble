using UnityEngine;

public class Bullet : MonoBehaviour, IPooledObject
{
    
    public bool Active { get; set; }

    public void Reset()
    {
        // Reinicia el estado de la bala
        gameObject.SetActive(false);
    }
}