using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // ใช้ SceneManager

public class Door : MonoBehaviour
{
    [SerializeField] private string sceneToLoad; // ชื่อฉากที่ต้องการเปลี่ยนไป

    private void OnTriggerEnter2D(Collider2D other)
    {
        // ตรวจสอบว่าชนกับวัตถุที่มีแท็ก "Player" หรือไม่ (สามารถเปลี่ยนตามต้องการ)
        if (other.CompareTag("Player"))
        {
            // เปลี่ยนไปยังฉากที่ระบุ
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}