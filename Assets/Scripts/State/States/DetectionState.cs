using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionState : IEnemyState
{
    private Pig _enemy;
    private float jumpForce = 5f;
    private Transform player;
    private float detectionRange = 5f;

    private Rigidbody2D rb;

    public void Enter(Pig enemy)
    {
        _enemy = enemy;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Debug.Log("Jugador Detectado");
        _enemy.StartCoroutine(DetectAndJump());
    }

    public void Execute()
    {
        
    }

    public void Exit()
    {
        _enemy.StopAllCoroutines();
    }
    
    private IEnumerator DetectAndJump()
    {
        float distanceToPlayer = Vector2.Distance(_enemy.transform.position, player.position);
        Debug.Log("Distancia al jugador: " + distanceToPlayer);

        if (distanceToPlayer <= detectionRange)
        {
            Debug.Log("Jugador dentro del rango, saltando.");
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        else
        {
            Debug.Log("Jugador fuera del rango.");
        }

        yield return new WaitForSeconds(1f);

        _enemy.ChangeState(_enemy.GetPreviousState());
    }
}
