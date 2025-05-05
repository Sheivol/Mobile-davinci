using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // enemigo
    GameObject currentEnemy;
    float maxEnemyHealth;
    float currentEnemyHealth;
    Slider enemyHealthSlider;

    // pj
    public Slider playerHealthSlider;
    public float maxPlayerHealth = 100f;
    private float currentPlayerHealth;

    public GameObject player;
    public GameObject loseText;
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        currentPlayerHealth = maxPlayerHealth;
        UpdatePlayerHealthUI();
    }

    public void SetEnemy(GameObject enemy, float health, Slider slider)
    {
        currentEnemy = enemy;
        maxEnemyHealth = health;
        currentEnemyHealth = health;
        enemyHealthSlider = slider;

        enemyHealthSlider.maxValue = maxEnemyHealth;
        enemyHealthSlider.value = currentEnemyHealth;
    }

    public void DamageEnemy(float amount)
    {
        if (currentEnemy == null) return;

        currentEnemyHealth -= amount;
        if (currentEnemyHealth < 0) currentEnemyHealth = 0;

        enemyHealthSlider.value = currentEnemyHealth;

        if (currentEnemyHealth == 0)
        {
            Destroy(currentEnemy);
            currentEnemy = null;
        }
    }

    public void DamagePlayer(float amount)
    {
        currentPlayerHealth -= amount;
        currentPlayerHealth = Mathf.Clamp(currentPlayerHealth, 0, maxPlayerHealth);
        UpdatePlayerHealthUI();

        if (currentPlayerHealth <= 0)
        {
            Debug.Log("muerto");
            if (player != null) player.SetActive(false);
            if (loseText != null) loseText.SetActive(true);
        }
    }

    void UpdatePlayerHealthUI()
    {
        if (playerHealthSlider != null)
            playerHealthSlider.value = currentPlayerHealth / maxPlayerHealth;
    }

    public void HealPlayer(float amount)
    {
        currentPlayerHealth += amount;
        currentPlayerHealth = Mathf.Clamp(currentPlayerHealth, 0, maxPlayerHealth);
        UpdatePlayerHealthUI();
    }
}
