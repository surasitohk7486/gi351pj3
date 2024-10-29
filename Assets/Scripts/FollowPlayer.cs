using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player; // อ้างอิงถึงตำแหน่งของ Player
    public Vector3 offset; // ใช้สำหรับกำหนดระยะห่างจาก Player

    void Update()
    {
        if (player != null)
        {
            // ปรับตำแหน่งของวัตถุให้ตามตำแหน่งของ Player โดยเพิ่มระยะ offset
            transform.position = player.position + offset;
        }
    }
}
