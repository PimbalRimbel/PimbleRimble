using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class BulletScript : MonoBehaviour
{
    public float velocidad;

    private Rigidbody2D Rigidbody2D;
    private Vector3 Direction;

    private void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Rigidbody2D.velocity = Direction * velocidad;
    }

    public void SetDirection(Vector3 direction)
    {
        Direction = direction;
    }

    /*
    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyScript enemy = other.GetComponent<EnemyScript>();
        PlayerS player = other.GetComponent<PlayerS>();

        if (enemy != null)
        {
            enemy.Hit();
        }
        if (player != null)
        {
            player.Hit();
        }
        gameObject.SetActive(false); // En lugar de Destroy
    }
    */
}