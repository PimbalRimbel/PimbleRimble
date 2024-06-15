using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionState : IEnemyState
{
    private Pig _enemy;
    private float jumpForce = 5f;
    private Transform player;
    private float detectionRange = 5f;

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

        if (distanceToPlayer <= detectionRange)
        {
            
            _enemy.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            
        }

        yield return new WaitForSeconds(1f);

        _enemy.ChangeState(_enemy.GetPreviousState());
    }
}
