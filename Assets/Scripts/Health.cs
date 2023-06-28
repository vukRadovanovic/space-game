using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 50;
    [SerializeField] ParticleSystem hitEffect;
    
    [SerializeField] bool applyCameraShake = false;
    CameraShake cameraShake;
    AudioPlayer audioPlayer;

    [SerializeField] bool isPlayer;
    [SerializeField] int pointsOnHit = 10;
    [SerializeField] int pointsOnDeath = 100;
    ScoreKeeper scoreKeeper;

    UIDisplay uiDisplay;


    LevelManager levelManager;

    private void Awake() {
        cameraShake = Camera.main.GetComponent<CameraShake>();   
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        uiDisplay = FindObjectOfType<UIDisplay>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        if(damageDealer != null) {
            TakeDamage(damageDealer.GetDamage());
            PlayHitEffect();
            ShakeCamera();
            audioPlayer.PlayDamageClip();
            damageDealer.Hit();
        }
    }

    public void TakeDamage(int damage) {
        health -= damage;

        if(isPlayer) {
            uiDisplay.UpdateHealthUI();
        }

        if (health <= 0) {
            if (scoreKeeper != null) {
                scoreKeeper.AddScore(pointsOnDeath);
            }
            if (isPlayer) {
                levelManager.LoadGameOver();
            }
            Destroy(gameObject);
        } else {
            if (scoreKeeper != null) {
                scoreKeeper.AddScore(pointsOnHit);
            }
        }
    }

    public int GetHealth() {
        return health;
    }

    void PlayHitEffect() {
        if(hitEffect != null) {
            ParticleSystem instance = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }

    void ShakeCamera() {
        if (cameraShake != null && applyCameraShake) {
            cameraShake.Play();
        }
    }
}
