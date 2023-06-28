using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIDisplay : MonoBehaviour
{

    [Header("Health")]
    private int maxHealth = 0;
    [SerializeField] Health health;
    [SerializeField] Slider healthBar;

    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;
    

    private void Awake() {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    private void Start() {
        // Health
        healthBar.minValue = 0f;
        healthBar.maxValue = health.GetHealth();
        healthBar.value = healthBar.maxValue;

        // Score
        scoreText.text = "000000000";
    }

    public void UpdateHealthUI() {
        healthBar.value = health.GetHealth();
    }

    public void UpdateScoreUI() {
        scoreText.text = scoreKeeper.GetScore().ToString("000000000");
    }
}
