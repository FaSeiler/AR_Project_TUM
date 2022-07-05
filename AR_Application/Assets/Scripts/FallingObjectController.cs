using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FallingObjectController : MonoBehaviour
{
    public int scoreToWinGame = 5;
    public CountdownTimer countdownTimer;
    public ImageTrackingPrefabs imageTrackingPrefabs;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI failedText;
    public Button restartButton;

    private static int score = 0;

    public void AddToScore()
    {
        score += 1;
        Debug.Log("Score: " + score);
    }


    private void Start()
    {
        countdownTimer.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(false);
        failedText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);


        CountdownTimer.OnCountdownTimerTimesUp += CountdownTimer_OnCountdownTimerTimesUp;
    }

    private void CountdownTimer_OnCountdownTimerTimesUp()
    {
        GameObject bucketGO = FindObjectOfType<SpawnerLoader>().gameObject;

        Debug.Log("TimesUp! Score: " + score);

        var portalFalling = bucketGO.GetComponent<PortalFalling>();

        portalFalling.DisableMesh();
        Transform rewardSpawnTransform = bucketGO.gameObject.transform;

        Destroy(bucketGO.GetComponent<SpawnerLoader>().spawnerGO);

        StopGame();

        if (score >= scoreToWinGame)
        {
            Debug.Log("WIN");
            UIDebugText.AddLog("Win!");
            // Collect reward
            Reward reward = FindObjectOfType<Reward>();
            reward.ShowReward(rewardSpawnTransform);

        }
        else
        {
            failedText.gameObject.SetActive(true);
            failedText.text = "You failed with score: " + score.ToString();
            imageTrackingPrefabs.StopTracking();

            restartButton.gameObject.SetActive(true);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            StartGame();
        }

        if (scoreText.gameObject.activeSelf == true)
        {
            scoreText.text = score.ToString();
        }
    }

    public void StartGame()
    {
        score = 0;
        failedText.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(true);
        countdownTimer.gameObject.SetActive(true);
        countdownTimer.ResetTimer();
        countdownTimer.StartTimer();
    }

    public void StopGame()
    {
        if (scoreText != null && countdownTimer != null)
        {
            scoreText.gameObject.SetActive(false);
            countdownTimer.gameObject.SetActive(false);
        }

        countdownTimer.ResetTimer();
    }

    public void OnDisable()
    {
        CountdownTimer.OnCountdownTimerTimesUp -= CountdownTimer_OnCountdownTimerTimesUp;
    }
}
