using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class IdleState : IEnemyState
{
    private Pig _enemy;
    
    public void Enter(Pig enemy)
    {
        _enemy = enemy;
        _enemy.StartCoroutine(IdleAndChangeState());
        
    }

    public void Execute()
    {
        // Este estado es idle, no hace nada.
    }

    public void Exit()
    {
        // Puedes agregar cualquier l�gica necesaria al salir del estado Idle
        _enemy.StopAllCoroutines();
    }

    private IEnumerator IdleAndChangeState()
    {
        yield return new WaitForSeconds(1f);

        if (_enemy.GetPreviousState() is WalkState)
        {
            _enemy.ChangeState(new WalkbackState());
        }
        else if (_enemy.GetPreviousState() is WalkbackState)
        {
            _enemy.ChangeState(new WalkState());
        }
    }
}
