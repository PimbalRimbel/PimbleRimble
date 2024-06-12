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
            _enemy.transform.Translate(Vector3.left * distanceToMove);
            movedDistance += distanceToMove;
            yield return null;
        }
        _enemy.ChangeState(new IdleState());
    }

}
