using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeHeight : SpecialAction
{
    [SerializeField]
    int[] scale;

    int currentScale = 0;

    public override void Execute()
    {
        currentScale++;
        if(currentScale >= scale.Length)
        {
            currentScale = 0;
        }
        transform.localScale = new Vector3(1, scale[currentScale], 1);


    }
}
