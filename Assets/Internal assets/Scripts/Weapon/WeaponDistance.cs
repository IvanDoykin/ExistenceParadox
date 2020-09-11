using System.Collections;
using UnityEngine;

public class WeaponDistance : Weapon
{
    [SerializeField]
    private GameObject bulletPrefab = null;
    [SerializeField]
    private int roundsPerMinute = 0;
    private bool isReadyFire = true;
    public override void Attack()
    {
        if(isReadyFire)
        {
            isReadyFire = !isReadyFire;
            Instantiate(bulletPrefab, transform.position, transform.rotation);
            StartCoroutine(Example());
        }

    }

    private IEnumerator Example()
    {
        yield return new WaitForSeconds(60.0f / roundsPerMinute);
        isReadyFire = !isReadyFire;
    }
}
