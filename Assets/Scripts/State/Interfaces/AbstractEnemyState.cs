using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractEnemyState : IEnemyState
{
    protected FwPig enemy;

    public AbstractEnemyState(FwPig enemy)
    {
        this.enemy = enemy;
    }

    public abstract void Enter(Pig _enemy);

    public abstract void Execute();
    public abstract void Exit();
    
    
}
