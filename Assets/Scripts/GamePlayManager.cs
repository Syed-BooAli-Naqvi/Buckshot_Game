using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class GamePlayManager : MonoBehaviour
{
    public GameObject PausePanel;
    public GameObject LevelCompletePanel;
    public GameObject LevelFailedPanel;

    public static GamePlayManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {

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

}