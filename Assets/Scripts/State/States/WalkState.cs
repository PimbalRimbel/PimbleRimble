using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : IEnemyState
{
    private Pig _enemy;
    private float moveDistance = 3.0f;
    private float moveSpeed = 1.0f;


    public void Enter(Pig enemy)
    {
        _enemy = enemy;
        FlipSprite();
        _enemy.StartCoroutine(MoveForward());
        Debug.Log("Empieza a moverse");
        
    }

    public void Execute()
    {
        //_enemy.StartMoving();

        /*if (true)
        {
            Debug.Log("Debería cambiar a Search");
            //_enemy.ChangeState(new SearchState());
            
        }*/
    }
    public void Exit()
    {
        Debug.Log("Exiting Walk State");
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
        scale.x *= -1; // Voltear el eje X
        _enemy.transform.localScale = scale;
    }

}
