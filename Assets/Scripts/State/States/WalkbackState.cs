using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkbackState : IEnemyState
{
    private Pig _enemy;
    private float moveDistance = 1.0f;
    private float moveSpeed = .7f;


    public void Enter(Pig enemy)
    {
        _enemy = enemy;
        FlipSprite();
        _enemy.StartCoroutine(MoveBackward());

    }

    public void Execute()
    {
    
    }
    public void Exit()
    {
        _enemy.StopAllCoroutines();
    }

    private IEnumerator MoveBackward()
    {
        float movedDistance = 0f;
        while (movedDistance < moveDistance)
        {
            float distanceToMove = Mathf.Min(moveSpeed * Time.deltaTime, moveDistance - movedDistance);
            _enemy.transform.Translate(Vector3.left * distanceToMove);
            movedDistance += distanceToMove;
            yield return null;
        }
        _enemy.ChangeState(new IdleState());
    }

    private void FlipSprite()
    {
        Vector3 scale = _enemy.transform.localScale;
        scale.x *= -1; // Voltear el eje X
        _enemy.transform.localScale = scale;
    }

}
