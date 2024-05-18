using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D Rigidbody2D;
    public BulletPool bulletPool;
    public Transform puntoDisparo;

    private int numero = 0;
    public float velDisparo = 0.5f; //velocidad de disparo
    public float nextDisparo = 0f;

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
        bulletPool = FindObjectOfType<BulletPool>(); // Encuentra el BulletPool en la escena
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

        //Disparar
        if (Input.GetButton("Fire1") && Time.time >= nextDisparo)
        {
            nextDisparo = Time.time + velDisparo;

            Disparo();
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
        Bullet bullet = bulletPool.GetBullet();
        if (bullet != null)
        {
            bullet.transform.position = puntoDisparo.position;
            bullet.transform.rotation = puntoDisparo.rotation;

            bullet.GetComponent<BulletScript>().SetDirection(puntoDisparo.up);
        }
    }
}
