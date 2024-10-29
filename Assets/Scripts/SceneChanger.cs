using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // กำหนดชื่อฉากที่ต้องการจะไป
    [SerializeField] private string sceneToLoad;

    void Update()
    {
        // ตรวจสอบว่าผู้เล่นกดปุ่ม E หรือไม่
        if (Input.GetKeyDown(KeyCode.E))
        {
            LoadNextScene();
        }
    }

    // ฟังก์ชันสำหรับเปลี่ยนฉาก
    private void LoadNextScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
