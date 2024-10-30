using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI hpText; // ����ҡ TextMeshPro ������ҧ���� Inspector
    public PlayerController playerController;

    void Update()
    {
        hpText.text = "HP:" + playerController.hp;
    }

    /*public void UpdateHP(int hp)
    {
        hpText.text = "HP: " + hp; // �ʴ� HP �Ѩ�غѹ
    }*/
}
