using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] float shakeDuration = 1f;
    [SerializeField] float shakeMagnitude = 0.5f;
    
    Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    public void Play() {
        StartCoroutine(Shake());
    }

    IEnumerator Shake() {
        for(float timeLeft = shakeDuration; timeLeft > 0; timeLeft -= Time.deltaTime) {
            transform.position = initialPosition + (Vector3)Random.insideUnitCircle * shakeMagnitude * (timeLeft / shakeDuration);
            yield return new WaitForEndOfFrame();
        }
        transform.position = initialPosition;
    }

}
