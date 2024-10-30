using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI hpText; // ให้ลาก TextMeshPro ที่สร้างขึ้นใน Inspector
    public PlayerController playerController;

    void Update()
    {
        hpText.text = "HP:" + playerController.hp;
    }

    /*public void UpdateHP(int hp)
    {
        hpText.text = "HP: " + hp; // แสดง HP ปัจจุบัน
    }*/
}
