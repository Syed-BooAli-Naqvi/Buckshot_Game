using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TrainingManager : Singleton<TrainingManager>
{
    public GameObject PausePanel;
    public GameObject LevelCompletePanel;
    public GameObject LevelFailedPanel;

    public float totalTime = 60f; // Total time for the timer
    public Text timerText, enemyCountTxt; // Reference to the UI text component to display the timer
    public int enemyCount;
    private float timeRemaining; // Current time remaining
    private Coroutine timerCoroutine; // Reference to the timer coroutine
    public MoveObject bullet;

    private void Start()
    {
        GiveInfo("You are in training phase, once you complete your training you will be fully ready");
        timeRemaining = totalTime;
        StartTimer();
        enemyCountTxt.text = "Total Enemy Killed : " + (enemyCount + 1) + " / 40";
    }

    public void AddEnemyCount()
    {
        enemyCountTxt.text = "Total Enemy Killed : " + (enemyCount + 1) + " / 40";
        enemyCount += 1;

        if (enemyCount >= 39)
            ShowLevelCompletePanel();
    }
    void StartTimer()
    {
        if (timerCoroutine != null)
            StopCoroutine(timerCoroutine); // Stop the coroutine if it's already running

        timerCoroutine = StartCoroutine(Countdown()); // Start the timer coroutine
    }

    public void AddTime() 
    {
        timeRemaining += 5;
    }

    IEnumerator Countdown()
    {
        while (timeRemaining > 0)
        {
            UpdateTimerDisplay(timeRemaining); // Update the timer display

            yield return new WaitForSeconds(1f); // Wait for 1 second

            timeRemaining -= 1f; // Decrease the time remaining by 1 second
        }
        if (enemyCount != 39)
            ShowlevelFail();
        else
            ShowLevelCompletePanel();
        // Timer has run out
        timeRemaining = 0f;
        UpdateTimerDisplay(timeRemaining);
        Debug.Log("Time has run out!");
    }

    void UpdateTimerDisplay(float timeToDisplay)
    {
        // Convert time to minutes and seconds
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        // Update the UI text to display the time
        timerText.text = "Time Remaining "+  string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void ShowLevelCompletePanel()
    {
        LevelCompletePanel.SetActive(true);
    }

    public void ShowlevelFail()
    {
        LevelFailedPanel.SetActive(true);
    }
    public void ShowLevelComplteWithDelay(float time)
    {
        StartCoroutine(ShowCompleteWithDelay(time));
    }

    IEnumerator ShowCompleteWithDelay(float time)
    {
        yield return new WaitForSeconds(time);
        ShowLevelCompletePanel();
    }

    public void ShowLevelFailWithDelay(float time)
    {
        StartCoroutine(ShowFailWithDelay(time));
    }

    IEnumerator ShowFailWithDelay(float time)
    {
        yield return new WaitForSeconds(time);
        ShowlevelFail();
    }

    public void gotohome()
    {
        LoadingScript.Instance.loadscene(1);
    }

    public void reloadGame()
    {
        LoadingScript.Instance.loadscene(2);
    }

    public void Nextbtnlevel()
    {
        StartCoroutine(reloadingaftersometime());
    }

    IEnumerator reloadingaftersometime()
    {
        yield return new WaitForSeconds(2f);
        LoadingScript.Instance.loadscene(2);
    }
    public void Gamepause()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0.01f;
    }

    public void GameResume()
    {
        Time.timeScale = 1f;
        PausePanel.SetActive(false);
    }
    public Text info;
    public GameObject infoPanel;
    public void GiveInfo(string info)
    {
        this.info.text = info;
        infoPanel.SetActive(true);
    }
}