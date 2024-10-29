using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void onClickMenu()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void onClickExit()
    {
        Application.Quit();
    }
}
