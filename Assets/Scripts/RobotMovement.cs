using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RobotMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    public float acceleration = 1f;

    public float jumpForce = 5f;
    public float highJumpForce = 10f;

    public bool canDoubleJump = false;
    bool doubleJumped = false;

    [SerializeField]
    float airMultiplyer = 0.2f;

    Rigidbody2D rb;

    private Vector2 bounds;

    [Header("Energy Settings")]
    EnergyControll energyControll;
    float energyToJump = 1;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bounds = GetComponent<Collider2D>().bounds.size;
        TryGetComponent<EnergyControll>(out energyControll);
    }

    private void Update()
    {
        if (isGrounded())
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            doubleJumped = false;
        }
    }

    public void MoveLeft()
    {
        if(isGrounded())
        {
            if(rb.velocity.x > -moveSpeed)
            {
                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            }
        }
        else
        {
            if (rb.velocity.x > -moveSpeed * airMultiplyer)
            {
                //rb.velocity = new Vector2(-moveSpeed * airMultiplyer, rb.velocity.y);
                rb.velocity += new Vector2(-acceleration * Time.deltaTime, 0);
            }
        }
    }

    public void MoveRight()
    {
        if(isGrounded())
        {
            if (rb.velocity.x < moveSpeed)
            {
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            }
        }
        else
        {
            if (rb.velocity.x < moveSpeed * airMultiplyer)
            {
                //rb.velocity = new Vector2(moveSpeed * airMultiplyer, rb.velocity.y);
                rb.velocity += new Vector2(acceleration * Time.deltaTime, 0);
            }
        }
    }

    public void Jump()
    {
        if(isGrounded())
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            doubleJumped = false;
        }
        else if (canDoubleJump && !doubleJumped)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            doubleJumped = true;
        }
    }

    public void HighJump()
    {
        if(energyControll == null || energyControll.UseEnergy(energyToJump))
        {
            if (isGrounded())
            {
                rb.AddForce(Vector2.up * highJumpForce, ForceMode2D.Impulse);
                doubleJumped = false;
            }
            else if (canDoubleJump && !doubleJumped)
            {
                rb.AddForce(Vector2.up * highJumpForce, ForceMode2D.Impulse);
                doubleJumped = true;
            }
        }
        else
        {
            Jump();
        }
    }

    private bool isGrounded()
    {
        Debug.DrawRay(transform.position, Vector2.down * bounds.y * 0.55f, Color.red);
        List<RaycastHit2D> hit = Physics2D.RaycastAll(transform.position, Vector2.down, bounds.y * 0.55f).ToList();
        hit.AddRange(Physics2D.RaycastAll(transform.position, Vector2.down + Vector2.left, 0.55f* bounds.magnitude));
        hit.AddRange(Physics2D.RaycastAll(transform.position, Vector2.down + Vector2.right, 0.55f * bounds.magnitude));
        hit = hit.OrderBy(x => x.distance).ToList();
        for(int i = 0; i < 4; i++)
        {
            try
            {
                if (hit[i].collider.gameObject != gameObject)
                {
                    return true;
                }
            }
            catch { return false; }
        }
        return false;
    }
}
