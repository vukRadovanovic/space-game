using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    
    UIDisplay uiDisplay;

    int currentScore = 0; 

    static ScoreKeeper instance;

    void ManageSingleton() {
        if(instance != null) {
            gameObject.SetActive(false);
            Destroy(gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Awake() {
        ManageSingleton();
        uiDisplay = FindObjectOfType<UIDisplay>();
    }

    public int GetScore() {
        return currentScore;
    }

    public void AddScore(int addAmt) {
        currentScore = Mathf.Clamp(currentScore + addAmt, 0, int.MaxValue);
        uiDisplay.UpdateScoreUI();
    }

    public void ResetScore() {
        currentScore = 0;
    }
    
}
