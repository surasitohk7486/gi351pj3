using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public enum DropType { Health, ParryPoint };
    public DropType lastDrop = DropType.Health;

    [SerializeField] private QTEManager qteManager;
    [SerializeField] private PlayerController playerHP;

    void Start()
    {
        qteManager = FindObjectOfType<QTEManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player Near");
        }
        if (other.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            DropItem();
        }
    }

    void DropItem()
    {
        if (lastDrop == DropType.Health)
        {
            if (playerHP.hp <= 3)
            {
                playerHP.hp += 1; // เพิ่มพลังชีวิตให้ผู้เล่น
            }
            lastDrop = DropType.ParryPoint;
            
            Debug.Log("Hp Player + 1");
        }
        else
        {
            lastDrop = DropType.Health;
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
