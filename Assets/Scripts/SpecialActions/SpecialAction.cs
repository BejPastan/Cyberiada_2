using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RobotMovement))]
public class SpecialAction : MonoBehaviour
{
    public int energyCost = 1;

    public virtual void Execute()
    {
        Debug.Log("Special Action Executed");
    }

    public virtual void Stop()
    {
        Debug.Log("Special Action Stopped");
    }
}
