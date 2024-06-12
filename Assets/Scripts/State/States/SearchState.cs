using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchState : IEnemyState
{
   private Pig _enemy;
   

    public void Enter(Pig enemy)
    {
        _enemy = enemy;
        Debug.Log("Entering search state");
    }

    public void Execute()
    {
        Debug.Log("Player detected, switching to walk state");
        _enemy.ChangeState(new WalkState());
        /*Collider[] hitColliders = Physics.OverlapSphere(_enemy.transform.position, detectionRadius);

        foreach(var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                Debug.Log("Player detected, switching to walk state");
                _enemy.ChangeState(new WalkState());
                return;
            }
        }*/

        /*PRUEBA DE DETECCIÓN CON RAYCAST
         * RaycastHit hit;
        if (Physics.Raycast(_enemy.transform.position, _enemy.transform.forward, out hit, 10.0f))
        {
            if (hit.collider.CompareTag("Player"))
            {
                Debug.Log("Player detected, switching to walk");
                _enemy.ChangeState(new WalkState());
                return;
            }
        }*/
    }
    public void Exit()
    {
        Debug.Log("Exiting search state");
    }

      
}
