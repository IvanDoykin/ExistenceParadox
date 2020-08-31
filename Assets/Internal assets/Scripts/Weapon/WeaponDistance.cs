using UnityEngine;

public class WeaponDistance : Weapon
{
    [SerializeField]
    private GameObject bulletPrefab;
    public override void Attack()
    {
        Instantiate(bulletPrefab, transform.position, transform.rotation);
    }
}
