using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QTEManager : MonoBehaviour
{
    public Image qteImage; // Image สำหรับแสดงปุ่ม QTE

    public Sprite upArrowSprite; // กำหนด Sprite สำหรับปุ่ม Up
    public Sprite downArrowSprite; // กำหนด Sprite สำหรับปุ่ม Down
    public Sprite leftArrowSprite; // กำหนด Sprite สำหรับปุ่ม Left
    public Sprite rightArrowSprite; // กำหนด Sprite สำหรับปุ่ม Right

    public float qteTimeLimit = 3f;
    private Queue<KeyCode> keyQueue;
    private bool isQTEActive = false;
    private int kill = 0;
    private float timer;

    public int parryPoints = 0;
    public int maxParryPoints = 3;
    private bool parryUsedInThisQTE = false;

    [SerializeField] private PlayerController playerHP;
    [SerializeField] private Animator animator; // ลาก Animator ของ GameObject ที่ต้องการใน Inspector
    [SerializeField] private Enemy[] f;

    [SerializeField]
    private AudioSource attackAudioSource; // AudioSource สำหรับเสียงฟัน
    [SerializeField]
    private AudioClip attackClip; // AudioClip สำหรับเสียงฟัน

    [SerializeField]
    private AudioSource hurtAudioSource; // AudioSource สำหรับเสียงเมื่อโดนดาเมจ
    [SerializeField]
    private AudioClip hurtClip; // AudioClip สำหรับเสียงเมื่อโดนดาเมจ

    private void Start()
    {
        animator = GetComponent<Animator>(); // เข้าถึง Animator ของ GameObject นี้
        if (animator == null)
        {
            Debug.LogError("No Animator component found on this GameObject.");
        }

        qteImage.gameObject.SetActive(false); // ซ่อนภาพ QTE เมื่อเริ่มเกม
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
        qteImage.gameObject.SetActive(true); // แสดงภาพ QTE
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
                    // เรียก EndQTE(false) เพื่อทำให้ตัวละครแสดงอนิเมชั่นเจ็บ
                    EndQTE(false);
                }
            }
        }
    }

    void UseParry()
    {
        parryPoints--;
        qteImage.sprite = null; // ล้างรูปภาพเมื่อใช้ Parry
    }

    void DisplayNextKey()
    {
        KeyCode nextKey = keyQueue.Peek();
        switch (nextKey)
        {
            case KeyCode.UpArrow:
                qteImage.sprite = upArrowSprite;
                break;
            case KeyCode.DownArrow:
                qteImage.sprite = downArrowSprite;
                break;
            case KeyCode.LeftArrow:
                qteImage.sprite = leftArrowSprite;
                break;
            case KeyCode.RightArrow:
                qteImage.sprite = rightArrowSprite;
                break;
        }
    }

    void EndQTE(bool success)
    {
        isQTEActive = false;
        qteImage.gameObject.SetActive(false); // ซ่อนภาพ QTE เมื่อจบ QTE

        if (success)
        {
            Debug.Log("Success!");
            animator.SetTrigger("Attack"); // เรียกใช้ Trigger "Attack" เมื่อ QTE สำเร็จ
            attackAudioSource.PlayOneShot(attackClip); // เล่นเสียงฟัน
            Destroy(f[kill].gameObject);
            kill += 1;
        }
        else
        {
            Debug.Log("Failed!");
            playerHP.hp -= 1;
            f[kill].isPlayerDetected = false;
            Debug.Log($"Hp Player = {playerHP.hp}");

            // เรียกใช้ Trigger Hurt เมื่อกดปุ่มผิด
            hurtAudioSource.PlayOneShot(hurtClip); // เล่นเสียงดาเมจ
            animator.SetTrigger("Hurt");
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
