﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QTEManager : MonoBehaviour
{
    public TextMeshProUGUI qteText;
    public float qteTimeLimit = 3f;
    private Queue<KeyCode> keyQueue;
    private bool isQTEActive = false;
    private float timer;
    public int parryPoints = 0;
    public int maxParryPoints = 3;
    private bool parryUsedInThisQTE = false;

    [SerializeField] private PlayerController playerHP;
    [SerializeField] private Animator animator; // ลาก Animator ของ GameObject ที่ต้องการใน Inspector

    private void Start()
    {
        animator = GetComponent<Animator>(); // เข้าถึง Animator ของ GameObject นี้
 
        if (animator == null)
        {
            Debug.LogError("No Animator component found on this GameObject.");
        }
    }

    public void StartQTE(int numKeys)
    {
        keyQueue = new Queue<KeyCode>();
        List<KeyCode> possibleKeys = new List<KeyCode> { KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow };

        for (int i = 0; i < numKeys; i++)
        {
            KeyCode randomKey = possibleKeys[Random.Range(0, possibleKeys.Count)];
            keyQueue.Enqueue(randomKey);
        }

        DisplayNextKey();
        timer = qteTimeLimit;
        isQTEActive = true;
        parryUsedInThisQTE = false; // รีเซ็ตการใช้งาน parry เมื่อเริ่ม QTE
    }

    void Update()
    {
        if (isQTEActive)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                EndQTE(false);
            }

            if (Input.anyKeyDown)
            {
                if (Input.GetKeyDown(KeyCode.Space) && !parryUsedInThisQTE && parryPoints > 0)
                {
                    UseParry();
                    parryUsedInThisQTE = true;
                }
                else if (keyQueue.Count > 0 && keyQueue.Peek() == GetKeyCodePressed())
                {
                    keyQueue.Dequeue();
                    if (keyQueue.Count == 0)
                    {
                        EndQTE(true);
                    }
                    else
                    {
                        DisplayNextKey();
                    }
                }
                else if (keyQueue.Count > 0)
                {
                    EndQTE(false);
                }
            }
        }
    }

    void UseParry()
    {
        parryPoints--;
        qteText.text = "Parry Used!";
    }

    void DisplayNextKey()
    {
        qteText.text = "Press: " + keyQueue.Peek().ToString();
    }

    void EndQTE(bool success)
    {
        isQTEActive = false;
        qteText.text = success ? "Success!" : "Failed!";

        if (success)
        {
            Debug.Log("Success!");
            animator.SetTrigger("Attack"); // เรียกใช้ Trigger "Attack" เมื่อ QTE สำเร็จ
        }
        else
        {
            Debug.Log("Failed!");
            playerHP.hp -= 1;
            Debug.Log($"Hp Player = {playerHP.hp}");
        }
    }

    KeyCode GetKeyCodePressed()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) return KeyCode.UpArrow;
        if (Input.GetKeyDown(KeyCode.DownArrow)) return KeyCode.DownArrow;
        if (Input.GetKeyDown(KeyCode.LeftArrow)) return KeyCode.LeftArrow;
        if (Input.GetKeyDown(KeyCode.RightArrow)) return KeyCode.RightArrow;
        return KeyCode.None;
    }
}
