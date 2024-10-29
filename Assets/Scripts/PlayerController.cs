﻿using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int hp = 3;
    public float moveSpeed = 5f;
    public float climbSpeed = 3f;
    public Transform wallCheckPoint;
    public float wallCheckRadius = 0.2f;
    public LayerMask wallLayer;
    public bool isHiddenBehindWall = false;

    private Rigidbody2D rb;
    private bool isOnWall = false;

    [SerializeField]
    private Animator animator;
    [SerializeField]
    private SpriteRenderer spriteRenderer;

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

        if(move != 0)
        {
            spriteRenderer.flipX = move < 0; 
        }

        animator.SetBool("IsPlayerRun", Mathf.Abs(move) > 0);

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
