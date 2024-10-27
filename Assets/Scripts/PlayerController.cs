using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float climbSpeed = 3f;
    public Transform wallCheckPoint;
    public float wallCheckRadius = 0.2f;
    public LayerMask wallLayer;
    public bool isHiddenBehindWall = false;

    private Rigidbody2D rb;
    private bool isOnWall = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float move = Input.GetAxis("Horizontal"); // ควบคุมการเดินด้วย A, D หรือปุ่มลูกศร
        rb.velocity = new Vector2(move * moveSpeed, rb.velocity.y);

        // กด W เพื่อปีนกำแพง
        if (Input.GetKeyDown(KeyCode.W) && isOnWall)
        {
            rb.velocity = new Vector2(rb.velocity.x, climbSpeed);
        }

        // กด S เพื่อหลบหลังกำแพง
        if (Input.GetKeyDown(KeyCode.S) && isNearWall())
        {
            isHiddenBehindWall = true;
        }

        // ออกจากการหลบหลังกำแพงเมื่อกด W
        if (isHiddenBehindWall && Input.GetKeyDown(KeyCode.W))
        {
            isHiddenBehindWall = false;
        }
    }

    bool isNearWall()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(wallCheckPoint.position, wallCheckRadius, wallLayer);
        return colliders.Length > 0;
    }

    private void OnDrawGizmos()
    {
        if (wallCheckPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(wallCheckPoint.position, wallCheckRadius);
        }
    }
}
