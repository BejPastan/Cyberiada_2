using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayController : MonoBehaviour
{
    LayerMask layerMask;

    private void Start()
    {
        layerMask = LayerMask.GetMask("Mirror", "Default");
        StartCoroutine(CastingRay());
    }

    private IEnumerator CastingRay()
    {
        while (Application.isPlaying)
        {
            Debug.Log("Casting Ray");
            Ray2D ray = new Ray2D(transform.position, transform.up*-1);
            RaycastHit2D hit;
            if (Physics2D.Raycast(ray.origin, ray.direction, 100, layerMask))
            {
                hit = Physics2D.Raycast(ray.origin, ray.direction, 100, layerMask);
                if (hit.transform.CompareTag("Reflect"))
                {
                    BounceRay(hit, ray.direction);
                }
                Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red, 0.3f);

            }
            else
            {
                Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 0.3f);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void BounceRay(RaycastHit2D hit, Vector2 rayDir)
    {
        Vector2 direction = Vector2.Reflect(rayDir, hit.normal);
        Ray2D ray = new Ray2D(hit.point + direction*0.01f, direction);
        RaycastHit2D newHit = Physics2D.Raycast(ray.origin, ray.direction, 100, layerMask);
        Debug.DrawRay(ray.origin, ray.direction * newHit.distance, Color.green, 0.3f);
        if (newHit)
        {
            if (newHit.transform.CompareTag("Reflect"))
            {
                BounceRay(newHit, ray.direction);
            }
            //Debug.DrawRay(ray.origin, ray.direction * newHit.distance, Color.green, 0.3f);
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.blue, 0.3f);
        }
    }
}
