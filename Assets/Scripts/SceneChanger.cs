using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // กำหนดชื่อฉากที่ต้องการจะไป
    [SerializeField] private string sceneToLoad;

    private bool isOn = false;

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.E) && isOn)
        {
            LoadNextScene();
        }
    }

    // ฟังก์ชันสำหรับเปลี่ยนฉาก
    private void LoadNextScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isOn = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isOn = false;
    }
}
