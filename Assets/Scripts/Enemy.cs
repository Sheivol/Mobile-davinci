using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float health = 100f;
    public Slider healthBarSlider;

    void Start()
    {
        GameManager.Instance.SetEnemy(gameObject, health, healthBarSlider);
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            GameManager.Instance.DamagePlayer(20f);
        }
    }
}
