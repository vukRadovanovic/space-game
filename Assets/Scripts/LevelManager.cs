using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float sceneLoadDelay = 2f;
    ScoreKeeper scoreKeeper;

    private void Awake() {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    public void QuitGame() {
        Debug.Log("Quitting Game");
        Application.Quit();
    }

    public void LoadMainMenu() {
        SceneManager.LoadScene(0);
    }

    public void LoadGame() {
        scoreKeeper.ResetScore();
        SceneManager.LoadScene(1);
    }

    public void LoadGameOver() {
        StartCoroutine(WaitAndLoad(2, sceneLoadDelay));
    }

    IEnumerator WaitAndLoad(int sceneIdx, float delay) {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneIdx);
    }
}
