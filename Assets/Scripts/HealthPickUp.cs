using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    public float healAmount = 20f;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == GameManager.Instance.player)
        {
            GameManager.Instance.HealPlayer(healAmount);
            Destroy(gameObject);
        }
    }
}
