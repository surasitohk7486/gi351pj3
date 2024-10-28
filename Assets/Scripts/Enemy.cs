using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float detectionRange = 5f; // ระยะตรวจจับผู้เล่น
    public LayerMask playerLayer;
    private Transform player;
    private bool isPlayerDetected = false;

    // อ้างถึง QTEManager ที่ใช้ในโปรเจกต์นี้
    [SerializeField]  private QTEManager qteManager;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        qteManager = FindObjectOfType<QTEManager>();
    }

    void Update()
    {
        DetectPlayer();
    }

    void DetectPlayer()
    {
        // ตรวจสอบว่าผู้เล่นอยู่ในระยะการตรวจจับของบอทหรือไม่
        if (!isPlayerDetected && Vector2.Distance(transform.position, player.position) <= detectionRange)
        {
            isPlayerDetected = true;
            EStartQTE();
        }
    }

    void EStartQTE()
    {
        if (qteManager != null)
        {
            int randomKeyCount = Random.Range(3, 6); // สุ่มจำนวนปุ่มระหว่าง 3-5 สำหรับ QTE
            qteManager.StartQTE(randomKeyCount);
        }
    }

    // ใช้เพื่อแสดงระยะการตรวจจับของบอทใน Editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
