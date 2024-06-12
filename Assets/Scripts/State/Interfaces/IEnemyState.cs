using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyState
{
    void Enter(Pig enemy);
    void Execute();
    void Exit();
}
