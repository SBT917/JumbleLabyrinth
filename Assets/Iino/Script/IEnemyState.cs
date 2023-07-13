using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyState
{
    void EnterState(Enemy enemy);
    void UpdateState();
    void ExitState();
}
#region Enemy States
public class IdleState : IEnemyState
{
    //...state specific methods
    public void EnterState(Enemy enemy)
    {
        throw new System.NotImplementedException();
    }

    public void ExitState()
    {
        throw new System.NotImplementedException();
    }

    public void UpdateState()
    {
        throw new System.NotImplementedException();
    }
}

public class WanderState : IEnemyState
{
    //...state specific methods
    public void EnterState(Enemy enemy)
    {
        throw new System.NotImplementedException();
    }

    public void ExitState()
    {
        throw new System.NotImplementedException();
    }

    public void UpdateState()
    {
        throw new System.NotImplementedException();
    }
}

public class ChasingState : IEnemyState
{
    //...state specific methods
    public void EnterState(Enemy enemy)
    {
        
    }

    public void ExitState()
    {
        
    }

    public void UpdateState()
    {
        
    }
}

public class AttackingState : IEnemyState
{
    //...state specific methods
    public void EnterState(Enemy enemy)
    {
        throw new System.NotImplementedException();
    }

    public void ExitState()
    {
        throw new System.NotImplementedException();
    }

    public void UpdateState()
    {
        throw new System.NotImplementedException();
    }
}
#endregion
