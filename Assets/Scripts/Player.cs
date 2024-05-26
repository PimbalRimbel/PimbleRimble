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
            // Activar la bala antes de moverla
            bullet.gameObject.SetActive(true);

            // Establecer la posición inicial de la bala
            Vector3 bulletStartPosition = transform.position + (Vector3.right * 0.5f); // Ajusta la posición inicial según el jugador

            // Determinar la dirección de movimiento basada en la escala del jugador
            Vector3 direction = transform.localScale.x == 1.0f ? Vector3.right : Vector3.left;

            // Invertir la dirección si el jugador está mirando hacia la derecha
            if (transform.localScale.x == 1.0f)
            {
                direction *= -1.0f;
            }

            bullet.transform.position = bulletStartPosition; // Establecer la posición inicial de la bala

            // Iniciar la corrutina para mover la bala
            StartCoroutine(MoverBala(bullet.gameObject, direction));
        }
    }

    private IEnumerator MoverBala(GameObject bulletObject, Vector3 direction)
    {
        while (bulletObject.activeSelf)
        {
            bulletObject.transform.Translate(direction * Time.deltaTime * velDisparo); // Mover la bala en la dirección especificada
            yield return null;
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

    private void Reestablecer()
    {
        // Reiniciar posición y salud del jugador
        transform.position = new Vector3(-1, 0.1f, 0);
        salud = 5;
    }
}