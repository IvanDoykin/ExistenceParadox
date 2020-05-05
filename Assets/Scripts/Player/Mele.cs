using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Mele : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    private int damage = 50;

    public void SetDamage(int dmg)
    {
        damage = dmg;
    }

    public void Attack()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, 0.75f);
        foreach (Collider enemy in hitEnemies)
        {
            var t = enemy.GetComponent<EnemyHealth>();
            if (t != null)
            {
                t.TakeDamage(damage);
            }
        }
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown((int)MouseButton.LeftMouse))
        {
            Attack();
        }
    }
}
