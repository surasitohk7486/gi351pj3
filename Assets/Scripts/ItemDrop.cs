﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public enum DropType { Health, ParryPoint };
    public DropType lastDrop = DropType.Health;

    bool playerInRange = false;

    [SerializeField] private QTEManager qteManager;
    [SerializeField] private PlayerController playerHP;

    void Start()
    {
        qteManager = FindObjectOfType<QTEManager>();
        playerHP = FindObjectOfType<PlayerController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player Near");
            playerInRange = true; // ตั้งค่าให้ทราบว่าผู้เล่นอยู่ในเขต
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player Left");
            playerInRange = false; // เมื่อผู้เล่นออกจากเขต เปลี่ยนค่าเป็น false
        }
    }

    void Update()
    {
        // ตรวจสอบว่าอยู่ในเขตและกดปุ่ม E
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            DropItem();
        }
    }

    void DropItem()
    {
        if (lastDrop == DropType.Health && playerHP.hp < 3)
        {
            playerHP.hp += 1; // เพิ่มพลังชีวิตให้ผู้เล่น
            lastDrop = DropType.ParryPoint;
            
            Debug.Log("Hp Player + 1");
        }
        else
        {
            lastDrop = DropType.Health;
            Debug.Log($"Parry piont + 1");
            DropParryPoint();
        }
        Destroy(gameObject);
    }

    void DropParryPoint()
    {
        if (qteManager != null && qteManager.parryPoints < qteManager.maxParryPoints)
        {
            qteManager.parryPoints++;
        }
        else
        {
            // แสดง Parry Point ไอเทมบนพื้นเมื่อผู้เล่นมี Parry Point เต็มแล้ว
            InstantiateParryPoint();
        }
    }

    void InstantiateParryPoint()
    {
        GameObject parryItem = Instantiate(Resources.Load("ParryPointItem"), transform.position, Quaternion.identity) as GameObject;
        parryItem.GetComponent<ParryPointItem>().SetParryManager(qteManager);
    }
}
