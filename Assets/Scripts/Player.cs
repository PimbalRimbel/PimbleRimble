using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D Rigidbody;
    public BulletPool bulletPool; //referencia al BulletPool

    //Bala
    public float velDisparo = 0.5f; //velocidad de disparo
    private float nextDisparo;

    //Player
    public float speed = 5f; 
    public float jump = 10f; 
    private int salud = 5; 

    //Suelo
    private float horizontal; 
    private bool suelo; 

    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Movimiento horizontal
        horizontal = Input.GetAxisRaw("Horizontal");

        if (horizontal < 0.0f)
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f); // Girar a la izquierda
        }
        else if (horizontal > 0.0f)
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f); // Girar a la derecha
        }

        // Detectar si el jugador está en el suelo
        Vector3 raycastOrigin = transform.position + Vector3.down * 0.1f;
        suelo = Physics2D.Raycast(raycastOrigin, Vector2.down, 0.2f, LayerMask.GetMask("Ground"));

        // Dibujar el Raycast en la ventana de Scene
        Color rayColor = suelo ? Color.green : Color.red;
        Debug.DrawRay(raycastOrigin, Vector2.down * 0.1f, rayColor);

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
    }

    private void FixedUpdate()
    {
        // Movimiento horizontal
        Rigidbody.velocity = new Vector2(horizontal * speed, Rigidbody.velocity.y);
    }

    private void Jump()
    {
        Rigidbody.AddForce(Vector2.up * jump, ForceMode2D.Impulse); // Aplicar fuerza de salto
    }

    private void Disparo()
    {
        // Obtener una bala del BulletPool
        Bullet bullet = bulletPool.GetBullet();
        if (bullet != null)
        {
            Vector3 direction = transform.localScale.x == 1.0f ? Vector3.right : Vector3.left;

            bullet.transform.position = transform.position + direction * 0.1f; // Posición inicial de la bala
            bullet.gameObject.SetActive(true); // Activar la bala
        }
    }

    public void Alcanzado()
    {
        salud -= 1;
        if (salud <= 0)
        {
            Reestablecer();
        }
    }

    private void Posicion()
    {
        if (transform.position.y < -0.5)
        {
            Reestablecer(); // Reestablecer posición si ha caído demasiado
        }
    }

    private void Reestablecer()
    {
        // Reiniciar posición y salud del jugador
        transform.position = new Vector3(-1, 0.1f, 0);
        salud = 5;
    }
}