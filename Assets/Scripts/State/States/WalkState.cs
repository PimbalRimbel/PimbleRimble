using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : IEnemyState
{
    private Pig _enemy;
    private float moveDistance = 1.0f;
    private float moveSpeed;

    public void Enter(Pig enemy)
    {
        _enemy = enemy;
        FlipSprite();
        _enemy.StartCoroutine(MoveForward());
        moveSpeed = _enemy.GetSpeed();
    }

    public void Execute()
    {
        //_enemy.StartMoving();

        /*if (true)
        {
            Debug.Log("Deber�a cambiar a Detection");
            //_enemy.ChangeState(new DetectionState());
            
        }*/
    }
    public void Exit()
    {
        _enemy.StopAllCoroutines();
    }

    private IEnumerator MoveForward()
    {
        float movedDistance = 0f;
        while (movedDistance < moveDistance)
        {
            float distanceToMove = Mathf.Min(moveSpeed * Time.deltaTime, moveDistance - movedDistance);
            _enemy.transform.Translate(Vector3.right * distanceToMove);
            movedDistance += distanceToMove;
            yield return null;
        }
        _enemy.ChangeState(new IdleState());
    }
    private void FlipSprite()
    {
        Vector3 scale = _enemy.transform.localScale;
        scale.x *= -1; 
        _enemy.transform.localScale = scale;
    }

}
