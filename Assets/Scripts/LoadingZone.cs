using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingZone : MonoBehaviour
{
    [SerializeField]
    int chargeRange = 2;
    [SerializeField]
    int chargeRate = 1;
    [SerializeField]
    float chargeTime = 1;

    private void Start()
    {
        StartCoroutine(Charge());
    }

    private IEnumerator Charge()
    {
        while (Application.isPlaying)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, chargeRange);
            foreach (Collider2D collider in colliders)
            {
                EnergyControll energyControll = null;
                collider.TryGetComponent<EnergyControll>(out energyControll);
                if(energyControll != null)
                {
                    energyControll.AddEnergy(chargeRate);
                }
            }
            yield return new WaitForSeconds(chargeTime);
        }
    }
}
