using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // ตัวละครที่ต้องการให้กล้องติดตาม
    public float smoothSpeed = 0.125f; // ความเร็วในการเคลื่อนกล้อง (ค่าที่ต่ำลงจะนุ่มนวลขึ้น)
    public Vector3 offset; // ระยะห่างระหว่างกล้องกับตัวละคร

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset; // คำนวณตำแหน่งที่ต้องการให้กล้องอยู่
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); // ทำให้การเคลื่อนไหวของกล้องนุ่มนวลขึ้น
            transform.position = smoothedPosition; // อัพเดตตำแหน่งกล้อง
        }
    }
}
