using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceMonster : Monster
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private Collider ignoredCollider;

    public override void Attack(Transform attackeObject, int damage)
    {
        GetComponent<MonsterPursue>().readyForAttack = false;
        GetComponent<Renderer>().material.SetColor("_BaseColor", Color.black);

        GameObject createdProjectile = Instantiate(projectile, transform.position, new Quaternion());
        createdProjectile.transform.rotation = transform.rotation;
        createdProjectile.transform.LookAt(attackeObject);
        Physics.IgnoreCollision(createdProjectile.GetComponent<Collider>(), ignoredCollider);

        StartCoroutine(AttackCooldown());
    }

    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(60f / GetComponent<MonsterPursue>().attackSpeed);
        GetComponent<MonsterPursue>().readyForAttack = true;
        GetComponent<Renderer>().material.SetColor("_BaseColor", Color.red);
    }
}
