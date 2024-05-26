using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public float speed = 0.005f; 

    private void Update()
    {
        // Mover la bala hacia adelante en su dirección actual
        transform.Translate(Vector3.right * speed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Cuando la bala colisiona con otro objeto, liberarla y devolverla al pool
        gameObject.SetActive(false);
    }
}
