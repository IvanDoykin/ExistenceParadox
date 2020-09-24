using UnityEngine;

public abstract class MonsterState : MonoBehaviour
{
    public virtual void Pursue(Monster monster, bool call = false)
    {

    }

    public virtual void Idle(Monster monster)
    {

    }

    public virtual void DoAction(Monster monster)
    {

    }
}
