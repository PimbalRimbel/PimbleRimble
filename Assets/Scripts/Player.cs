using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Rigidbody2D Rigidbody2D;
    public GameObject BulletPrefab;

    private int numero = 0;
    public float velDisparo = 0.5f; //velocidad de disparo
    public float nextDisparo;
    private int salud = 5;

    public float speed;
    public float jump;
    public float horizontal;
    public bool suelo;


    //PATR�N OBSERVER
    private List<IObserver<float>> observers = new List<IObserver<float>>();

    public void AddObserver (IObserver<float> observer) //agregamos
    {
        observers.Add(observer);
    }

    public void RemoveObserver (IObserver<float> observer) //eliminamos
    {
        observers.Remove(observer);
    }


    public void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public void Update()
    {
        //Movimiento
        horizontal = Input.GetAxisRaw("Horizontal");

        if (horizontal < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (horizontal > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        //Detecto el suelo
        suelo = Physics2D.Raycast(transform.position, Vector3.down, 0.1f);

        //Salto
        if (Input.GetKeyDown(KeyCode.W) && suelo)
        {
            Jump();
        }

        // Disparar
        if (Input.GetKey(KeyCode.Space) && Time.time > nextDisparo + 0.25f)
        {
            Disparo();

            nextDisparo = Time.time;
        }

        Posicion();
    }

    public void Posicion()
    {
        if (transform.position.y < -0.5)
        {
            Reestablecer();
        }
    }

    private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(horizontal * speed, Rigidbody2D.velocity.y);
    }

    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
    }

    public void Disparo()
    {
        Vector3 direction;
        if (transform.localScale.x == 1.0f) direction = Vector3.right;
        else direction = Vector3.left;

        GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);
    }

    public void Alcanzado()
    {
        salud -= 1;
        if (salud == 0)
        {
            Reestablecer();
        }
    }

    public void Reestablecer()
    {
        numero = 1;
        if (salud <= 0)
        {
            transform.position = new Vector3(-1, 0.1f, 0);
            salud = 5;
        }
        else
        { 
            transform.position = new Vector3(-1, 0.1f, 0);
            salud = 5;
        }

    }
}
