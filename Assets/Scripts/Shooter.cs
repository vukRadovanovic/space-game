using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float firingRate = 0.5f;
    
    [Header("AI")]
    [SerializeField] bool useAI = false;
    [SerializeField] float firingRateVariance = 0f;
    [SerializeField] float minimumFiringRate = 0.05f;

    [HideInInspector] public bool isFiring = false;

    Coroutine firingCoroutine;
    AudioPlayer audioPlayer;

    void Awake() {
        audioPlayer = FindObjectOfType<AudioPlayer>();    
    }

    void Start()
    {
        if(useAI) {
            isFiring = true;
        }
    }

    void Update()
    {
        Fire();
    }

    void Fire() {
        if (isFiring && firingCoroutine == null) {
            firingCoroutine = StartCoroutine(FireContinuously());
        } else if (!isFiring && firingCoroutine != null) {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    IEnumerator FireContinuously() {
        while(true) {

            GameObject instance = Instantiate(projectilePrefab,
                                    gameObject.transform.position,
                                    Quaternion.identity);

            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
            if(rb != null) 
            {
                rb.velocity = transform.up * projectileSpeed;
            }

            audioPlayer.PlayShootingClip();

            Destroy(instance,projectileLifetime);
            yield return new WaitForSeconds(GetRandomFiringRate());
        }
    }


    public float GetRandomFiringRate() {
        float spawnTime = Random.Range(firingRate - firingRateVariance,
                                        firingRate + firingRateVariance);

        return Mathf.Clamp(spawnTime, minimumFiringRate, float.MaxValue);    
    }


}
