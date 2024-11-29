using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyControll : MonoBehaviour
{

    public float maxEnergy = 5;
    private float energy = 5;

    public bool UseEnergy(float amount)
    {
        if (energy >= amount)
        {
            energy -= amount;
            return true;
        }
        return false;
    }

    public void AddEnergy(float amount)
    {
        energy += amount;
        if (energy > maxEnergy)
        {
            energy = maxEnergy;
        }
    }
}
