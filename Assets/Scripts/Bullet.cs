using UnityEngine;

public class Bullet : MonoBehaviour, IPooledObject
{
    public bool Active { get; set; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Si la bala colisiona con un objeto distinto al jugador, desactivar la bala y devolverla al pool
        if (!other.CompareTag("Player"))
        {
            Reset();
        }
    }

    public void Reset()
    {
        // Desactivar la bala y devolverla al pool
        gameObject.SetActive(false);
    }
}