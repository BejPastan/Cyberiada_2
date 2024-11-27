using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMovement : MonoBehaviour
{
    public void MoveLeft()
    {
        transform.Translate(Vector3.left * Time.deltaTime);
    }

    public void MoveRight()
    {
        transform.Translate(Vector3.right * Time.deltaTime);
    }

    public void Jump()
    {
        Debug.Log("Jump");
        GetComponent<Rigidbody2D>().AddForce(Vector2.up*3, ForceMode2D.Impulse);
    }

    public void HighJump()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * 6, ForceMode2D.Impulse);
    }
}
