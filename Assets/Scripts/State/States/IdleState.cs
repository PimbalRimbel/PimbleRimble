using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class IdleState : IEnemyState
{
    private Pig _enemy;
    private Transform player;

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
           /* if(player = GameObject.FindGameObjectWithTag("Player").transform){ 
                
            _enemy.ChangeState(new DetectionState());
            yield return new WaitForSeconds(1f);
            }*/
            _enemy.ChangeState(new WalkbackState());
            

        }
        else if (_enemy.GetPreviousState() is WalkbackState)
        {

            _enemy.ChangeState(new WalkState());
        }
    }
}
