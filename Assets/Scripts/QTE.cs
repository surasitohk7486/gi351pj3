using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTE : MonoBehaviour
{
    // ประกาศตัวแปรสำหรับปุ่มที่สามารถสุ่มได้
    private KeyCode[] availableKeys = { KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.DownArrow};

    [SerializeField] private GameObject[] bottonKey = new GameObject[4];

    // List สำหรับเก็บลำดับปุ่มที่สุ่มมา
    private List<KeyCode> randomKeys = new List<KeyCode>();

    // ตัวแปรเพื่อติดตามตำแหน่งลำดับการกดปุ่มของผู้เล่น
    private int currentKeyIndex = 0;

    // จำนวนปุ่มที่ต้องการสุ่มในครั้งเดียว
    public int numberOfKeysToPress = 4;

    void Start()
    {
        // สุ่มปุ่มหลายปุ่มเมื่อเริ่มเกม
        GenerateRandomKeys();
        Debug.Log("Press the keys in order: " + KeysToString(randomKeys));
    }

    void Update()
    {
        // ตรวจสอบว่าผู้เล่นกดปุ่มที่ถูกต้องตามลำดับหรือไม่
        if (Input.GetKeyDown(randomKeys[currentKeyIndex]))
        {
            Debug.Log("Correct key: " + randomKeys[currentKeyIndex].ToString());
            currentKeyIndex++; // เพิ่มลำดับถ้ากดถูก

            // ตรวจสอบว่าผู้เล่นกดครบทุกปุ่มตามลำดับแล้วหรือไม่
            if (currentKeyIndex >= randomKeys.Count)
            {
                Debug.Log("All keys pressed correctly!");
                
                
                // สุ่มปุ่มใหม่เมื่อกดถูกต้องครบทุกปุ่ม
                GenerateRandomKeys();
                Debug.Log("Press the keys in order: " + KeysToString(randomKeys));
                currentKeyIndex = 0;
                
            }
        }
        else
        {
            // ตรวจสอบว่าผู้เล่นกดปุ่มผิดหรือไม่
            foreach (KeyCode key in availableKeys)
            {
                if (Input.GetKeyDown(key) && key != randomKeys[currentKeyIndex])
                {
                    Debug.Log("Wrong key pressed! Start over.");
                    currentKeyIndex = 0; // รีเซ็ตลำดับใหม่
                    
                    Debug.Log("Press the keys in order: " + KeysToString(randomKeys));
                }
            }
        }
    }

    // ฟังก์ชันสำหรับสุ่มปุ่มหลายปุ่ม
    void GenerateRandomKeys()
    {
        randomKeys.Clear();
        for (int i = 0; i < numberOfKeysToPress; i++)
        {
            int randomNum = Random.Range(0, availableKeys.Length);
            KeyCode randomKey = availableKeys[randomNum];
            randomKeys.Add(randomKey);

            switch(i)
            {
                case 0: bottonKey[randomNum].transform.position = new Vector2(-4, 0);
                    break;
                case 1: bottonKey[randomNum].transform.position = new Vector2(-1.5f, 0);
                    break;
                case 2: bottonKey[randomNum].transform.position = new Vector2(1.35f, 0);
                    break;
                case 3: bottonKey[randomNum].transform.position = new Vector2(4, 0);
                    break;
            }
            Instantiate(bottonKey[randomNum]);

        }
    }

    // ฟังก์ชันสำหรับแปลงลำดับปุ่มเป็นข้อความเพื่อพิมพ์ใน console
    string KeysToString(List<KeyCode> keys)
    {
        string keyString = "";
        foreach (KeyCode key in keys)
        {
            keyString += key.ToString() + " ";
        }
        return keyString.Trim();
    }

}
