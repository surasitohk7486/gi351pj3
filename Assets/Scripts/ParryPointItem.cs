using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryPointItem : MonoBehaviour
{
    [SerializeField] private QTEManager qteManager;

    public void SetParryManager(QTEManager manager)
    {
        qteManager = manager;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && qteManager.parryPoints < qteManager.maxParryPoints)
        {
            qteManager.parryPoints++;
            Destroy(gameObject);
        }
    }
}
