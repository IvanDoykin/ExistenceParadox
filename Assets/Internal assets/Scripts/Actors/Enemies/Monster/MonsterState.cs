using UnityEngine;

public abstract class MonsterState : MonoBehaviour
{
    public abstract void Pursue(Monster monster);
    public abstract void Idle(Monster monster);
    public abstract void DoAction(Monster monster);
}
