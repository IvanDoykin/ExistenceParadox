using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WeaponDistance : Weapon
{
    [SerializeField] private GameObject bulletPrefab = null;
    [SerializeField] private int roundsPerMinute = 0;
    [SerializeField] private int maxQuanityAmmo = 0;

    private bool isReadyFire = true;
    private int countQuanityAmmo = 0;

    public void ReloadWeapon(Slider slider)
    {
        StartCoroutine(DelayReload(slider));
        countQuanityAmmo = maxQuanityAmmo;
    }

    public override void Attack()
    {
        if (isReadyFire && countQuanityAmmo > 0)
        {
            isReadyFire = !isReadyFire;
            countQuanityAmmo--;
            Instantiate(bulletPrefab, transform.position, transform.rotation);
            StartCoroutine(DelayShot());
        }
    }

    private IEnumerator DelayShot()
    {
        yield return new WaitForSeconds(60.0f / roundsPerMinute);
        isReadyFire = !isReadyFire;
    }

    private IEnumerator DelayReload(Slider slider)
    {
        float timeCount = 0.0f;
        float timeReload = 5.0f;
        slider.maxValue = timeReload;
        slider.value = 0.0f;
        while (timeCount <= timeReload)
        {
            timeCount += Time.fixedDeltaTime;
            slider.value = timeCount;
            yield return new WaitForFixedUpdate();
        }
    }
}
