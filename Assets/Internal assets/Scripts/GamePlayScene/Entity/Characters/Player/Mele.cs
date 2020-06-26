using System.Collections;
using System.Collections.Generic; //useless for this script usages
using UnityEngine;
using UnityEngine.UIElements;

public class Mele : MonoBehaviour, ITick //maybe 'Melee'?
{
    [SerializeField] private Transform attackPoint;
    private int damage = 50;
    public TickData tickData { get; set; }

    private void Start()
    {
        ManagerUpdate.AddTo(this);
    }

    public void SetDamage(int dmg)
    {
        damage = dmg;
    }

    public void Attack()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, 0.75f); //'magic number' 0.75f
        foreach (Collider enemy in hitEnemies)
        {
            var t = enemy.GetComponent<EnemyHealth>();
            if (t != null)
            {
                t.TakeDamage(damage);
            }
        }
    }


    public void Tick()
    {
        if (Input.GetMouseButtonDown((int)MouseButton.LeftMouse))
        {
            Attack();
        }
    }
}