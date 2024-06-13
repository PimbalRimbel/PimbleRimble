using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionState : IEnemyState
{
    private Pig _enemy;
    private float jumpForce = 5f;

    public void Enter(Pig enemy)
    {
        _enemy = enemy;
        _enemy.StartCoroutine(DetectAndJump());
        Debug.Log("Entering DetectionState");
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
        _enemy.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(1f);

        _enemy.ChangeState(_enemy.GetPreviousState());
    }
}
