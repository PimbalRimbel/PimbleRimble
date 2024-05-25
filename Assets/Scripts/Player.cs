using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Rigidbody2D Rigidbody;
    public GameObject BulletPrefab;

    // Bala
    private int numero = 0;
    public float velDisparo = 0.5f; // velocidad de disparo
    private float nextDisparo;

    // Player
    public float speed = 5f;
    public float jump = 10f;
    private int salud = 5;

    // Movimiento
    private float horizontal;
    private bool suelo;

    // PATRÓN OBSERVER
    private List<IObserver<float>> observers = new List<IObserver<float>>();

    public void AddObserver(IObserver<float> observer) // agregamos
    {
        observers.Add(observer);
    }

    public void RemoveObserver(IObserver<float> observer) // eliminamos
    {
        observers.Remove(observer);
    }

    public void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        // Movimiento
        horizontal = Input.GetAxisRaw("Horizontal");

        if (horizontal < 0.0f)
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }
        else if (horizontal > 0.0f)
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }

        // Detecto el suelo
        Vector3 raycastOrigin = transform.position + Vector3.down * 0.1f;
        suelo = Physics2D.Raycast(raycastOrigin, Vector2.down, 0.2f, LayerMask.GetMask("Ground"));

        // Dibujar el Raycast en la ventana de Scene
        Color rayColor = suelo ? Color.green : Color.red;
        Debug.DrawRay(raycastOrigin, Vector2.down * 0.2f, rayColor);

        // Salto
        if (Input.GetKeyDown(KeyCode.W) && suelo)
        {
            Jump();
        }

        // Disparar
        if (Input.GetKey(KeyCode.Space) && Time.time > nextDisparo + velDisparo)
        {
            Disparo();
            nextDisparo = Time.time;
        }

        Posicion();
    }

    private void FixedUpdate()
    {
        // Movimiento horizontal
        Rigidbody.velocity = new Vector2(horizontal * speed, Rigidbody.velocity.y);
    }

    private void Jump()
    {
        Rigidbody.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
    }

    public void Disparo()
    {
        Vector3 direction = transform.localScale.x == 1.0f ? Vector3.right : Vector3.left;
        Instantiate(BulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);
    }

    public void Alcanzado()
    {
        salud -= 1;
        if (salud <= 0)
        {
            Reestablecer();
        }
    }

    public void Posicion()
    {
        if (transform.position.y < -0.5)
        {
            Reestablecer();
        }
    }

    public void Reestablecer()
    {
        numero = 1;
        transform.position = new Vector3(-1, 0.1f, 0);
        salud = 5;
    }
}
