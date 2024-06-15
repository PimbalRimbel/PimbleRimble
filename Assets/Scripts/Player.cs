using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; //para reiniciar
using System;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    public BulletPool bulletPool; //referencia al BulletPool
    public SceneManagement sceneManagement;
    public Transform spawnPos;
    private LifeManager lifeManager;

    //Bala
    public float velDisparo = 1.5f; //velocidad de disparo
    private float nextDisparo;

    //Player
    public float moveSpeed = 5f; // Velocidad de movimiento del personaje
    public float speed = 5f;
    public float jump = 5f;
    public int salud = 3;

    //Suelo
    private float horizontal;
    private bool suelo;


    private void Start()
    {        
        rb = GetComponent<Rigidbody2D>();
        lifeManager = FindObjectOfType<LifeManager>();
        sceneManagement = FindObjectOfType<SceneManagement>();

    }

    private void Update()
    {
        // Movimiento horizontal
        horizontal = Input.GetAxisRaw("Horizontal");

        if (horizontal < 0.0f)
        {
            transform.localScale = new Vector3(-2.0f, 2.0f, 2.0f); // Girar a la izquierda
        }
        else if (horizontal > 0.0f)
        {
            transform.localScale = new Vector3(2.0f, 2.0f, 2.0f); // Girar a la derecha
        }

        // Detectar si el jugador est� en el suelo
        Vector3 raycastOrigin = transform.position + Vector3.down * 0.1f;
        RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, Vector2.down, 0.2f, LayerMask.GetMask("Ground"));
        suelo = hit.collider != null;

        // Salto
        if (Input.GetKeyDown(KeyCode.W))
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
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private void Jump()
    {
        rb.AddForce(Vector2.up * jump, ForceMode2D.Impulse); // Aplicar fuerza de salto
    }

    private void Disparo()
    {
        // Obtener una bala del BulletPool
        Bullet bullet = bulletPool.GetBullet();

        if (bullet != null)
        {

            // Determinar la direcci�n de movimiento basada en la escala del jugador
            Vector3 direction = transform.localScale.x > 0 ? Vector3.right : Vector3.left;
            direction = direction * -1;

            // Establecer la posici�n inicial de la bala
            Vector3 bulletStartPosition = transform.position + (direction * -0.3f); // Ajusta la posici�n inicial seg�n el jugador

            // Invertir la direcci�n si el jugador est� mirando hacia la derecha
            if (transform.localScale.x == 1.0f)
            {
                direction *= -1.0f;
            }

            bullet.transform.position = bulletStartPosition; // Establecer la posici�n inicial de la bala


            // Restablecer el tiempo de disparo
            nextDisparo = Time.time;

            // Iniciar la corrutina para mover la bala
            StartCoroutine(MoverBala(bullet.gameObject, direction));
        }
    }

    private IEnumerator MoverBala(GameObject bulletObject, Vector3 direction)
    {
        while (bulletObject.activeSelf)
        {
            bulletObject.transform.Translate(direction * Time.deltaTime * velDisparo); // Mover la bala en la direcci�n especificada
            yield return null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(RestartLevelWithDelay(0.3f));
        }
        if (collision.gameObject.CompareTag("Win"))
        {
            sceneManagement.escenaWin();
            Debug.Log("Intento cambiar de escena");

        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            salud--;
            if (salud == 0) sceneManagement.escenaLost();
            lifeManager.LoseLife(salud);
        }
    }


    private IEnumerator RestartLevelWithDelay(float delay)
    {

        yield return new WaitForSeconds(delay);
        transform.position = spawnPos.transform.position;
    }
}