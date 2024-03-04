using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public GameObject player;
    public Animator animator;
    public Animator animator1;
    public Avatar[] playerAvatars;
    public GameObject[] playerObjects, playerObjects1, rccControllerObjects, playerControllerObjects, gun1, gun2, gun3, gun4, gun5;
    public RCC_Settings RCC_Settings;
    public Transform carPT;
    public RCC_CarControllerV3 rCC_Car;
    public GameObject enterNameObj;
    public UnityEvent enterNameEvent;
    public GameObject[] chooGun, opp1Gun, opp2Gun;
    public Animator playerAnim, chooAnim, opp1Anim, opp2Anim;
    public GameObject currentPlayerGun, currentPlayerchooGun, currentPlayeropp1Gun, currentPlayeropp2Gun, gameplayCam, gameplayCam1;
    public Image[] playerHeart, chooHeart, opp1Heart, opp2Heart;
    public RCC_Camera CC_Camera;

    private void Start()
    {
#if UNITY_EDITOR
        RCC_Settings.mobileControllerEnabled = false;
#else
        RCC_Settings.mobileControllerEnabled = true;
#endif
        SetCharacter();
        SetController(false);
        GiveInfo("Its a new day, lets try playing a daring game!\nGo to your car."); //StartGame();
    }
    public void SetCharacter() 
    {
        for (int i = 0; i < playerObjects.Length; i++)
        {
            playerObjects[i].SetActive(false);
            playerObjects1[i].SetActive(false);
        }
        int CharacterIndex = PlayerPrefs.GetInt(SharedPrefs.CharacterIndex);
        animator.avatar = playerAvatars[CharacterIndex];
        animator1.avatar = playerAvatars[CharacterIndex];
        playerObjects[CharacterIndex].SetActive(true);
        playerObjects1[CharacterIndex].SetActive(true);
    }

    public void SetController(bool setCar)
    {
        rCC_Car.enabled = setCar;
        if (setCar)
        {
            CC_Camera.cameraTarget.playerVehicle = rCC_Car;
            player.SetActive(false);
            player.transform.SetPositionAndRotation(carPT.position, carPT.rotation);
            player.transform.SetParent(carPT);
        }
        else
        {
            player.transform.SetParent(null);
        }
        for (int i = 0; i < playerControllerObjects.Length; i++)
        {
            playerControllerObjects[i].SetActive(!setCar);
        }
        for (int i = 0; i < rccControllerObjects.Length; i++)
        {
            rccControllerObjects[i].SetActive(setCar);
        }
    }

    public void GetNameAndLetPlayerEnter(bool get) 
    {
        enterNameObj.SetActive(get);
        if (!get && enterNameEvent != null)
        {
            enterNameEvent.Invoke();
        }
    }

    public void SetGun(int num)
    {
        int CharacterIndex = PlayerPrefs.GetInt(SharedPrefs.CharacterIndex);
        switch (num)
        {
            case 1:
                isPistol = true;
                currentPlayerGun = gun1[CharacterIndex];
                break;
            case 2:
                isPistol = false;
                currentPlayerGun = gun2[CharacterIndex];
                break;
            case 3:
                isPistol = true;
                currentPlayerGun = gun3[CharacterIndex];
                break;
            case 4:
                isPistol = false;
                currentPlayerGun = gun4[CharacterIndex];
                break;
            case 5:
                isPistol = false;
                currentPlayerGun = gun5[CharacterIndex];
                break;
        }

        currentPlayeropp1Gun = opp1Gun[num - 1];
        currentPlayeropp2Gun = opp2Gun[num - 1];
        currentPlayerchooGun = chooGun[num - 1];
    }
    public Button fire,fireYourself;
    public void StartGame()
    {
        StartCoroutine(StartRounds());
    }
    public bool fired, firedYourSelf, isPistol;
    public void Fire() 
    {
        fired = true;
    }

    public void FireYourself()
    {
        firedYourSelf = true;
    }

    public int playerLifeCount, chooLifeCount, opp1LifeCount, opp2LifeCount;
    public Text currentState;
    public List<int> roundChecker;
    void RandomizeList(List<int> list)
    {
        // Randomly shuffle the list using Fisher-Yates algorithm
        for (int i = 0; i < list.Count; i++)
        {
            int temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
    public int totalRounds;
    public IEnumerator StartRounds()
    {
        
        string trigger = isPistol ? "Pistol" : "Gun";
        float wait = isPistol ? 2: 3;

        int randomBulletCount = Random.Range(3, 6);
        int emptyRandomCount = Random.Range(2, randomBulletCount);
        Debug.LogError(randomBulletCount);
        Debug.LogError(emptyRandomCount);
        bulletArea.SetBullets(randomBulletCount, emptyRandomCount);
        roundChecker.Clear();
        Debug.LogError(randomBulletCount - emptyRandomCount);
        for (int i = 0; i < randomBulletCount-emptyRandomCount; i++)
        {
            Debug.Log("Adding 1");
            roundChecker.Add(1);
        }
        for (int i = 0; i < emptyRandomCount; i++)
        {
            Debug.Log("Adding 0");
            roundChecker.Add(0);
        }

        RandomizeList(roundChecker);


        gameplayCam.SetActive(true);
        gameplayCam1.SetActive(true);
        yield return new WaitForSeconds(1.2f);
        yield return bulletArea.StartShowing(true);
        int roundCount = roundChecker.Count;
        while (roundCount != 0)//playerLifeCount != 3 || chooLifeCount != 3 || opp1LifeCount != 3 || opp2LifeCount != 3)
        {
            currentState.text = "YOUR TURN";
            fire.gameObject.SetActive(false);
            fireYourself.gameObject.SetActive(false);




            yield return new WaitForSeconds(2);

            
            currentPlayerGun.SetActive(true);
            fire.gameObject.SetActive(true);
            fireYourself.gameObject.SetActive(true);


            yield return new WaitUntil(() => fired || firedYourSelf);

            fire.gameObject.SetActive(false);
            fireYourself.gameObject.SetActive(false);
            int randomNum = 0;
            if (fired)
            {
                fired = false;
                randomNum = roundChecker[roundCount-1];
                playerAnim.SetTrigger(trigger);

                yield return new WaitForSeconds(1);
                SoundManager.Instance.PlaySound(SoundName.gunshot);


                yield return new WaitForSeconds(wait - 1);


                if (randomNum == 1)
                {
                    currentState.text = "Opponent Lost a life";
                    chooHeart[chooLifeCount].gameObject.SetActive(true);
                    chooLifeCount = chooLifeCount + 1;
                }
                else
                {
                    currentState.text = "Round Was Empty";
                }
                if (playerLifeCount == 3 || chooLifeCount == 3 || opp1LifeCount == 3 || opp2LifeCount == 3)
                    goto Here;
                currentPlayerGun.SetActive(false);
            }
            else if (firedYourSelf) 
            {
                firedYourSelf = false;
                randomNum = roundChecker[roundCount-1];
                playerAnim.SetTrigger(trigger);

                yield return new WaitForSeconds(1);
                SoundManager.Instance.PlaySound(SoundName.gunshot);


                yield return new WaitForSeconds(wait - 1);


                if (randomNum == 1)
                {
                    currentState.text = "You Lost a life";
                    playerHeart[playerLifeCount].gameObject.SetActive(true);
                    playerLifeCount = playerLifeCount + 1;
                }
                else
                {
                    currentState.text = "Round Was Empty";
                }
                if (playerLifeCount == 3 || chooLifeCount == 3 || opp1LifeCount == 3 || opp2LifeCount == 3)
                    goto Here;
                currentPlayerGun.SetActive(false);
            }

            roundCount = roundCount - 1;
            yield return new WaitForSeconds(3);
            currentState.text = "CHOO'S TURN";
            yield return new WaitForSeconds(1);

            currentPlayerchooGun.SetActive(true);

            
            chooAnim.SetTrigger(trigger);

            if (roundCount - 1 < 0)
                goto Here;

            randomNum = roundChecker[roundCount-1];


            yield return new WaitForSeconds(1);
            SoundManager.Instance.PlaySound(SoundName.gunshot);
            yield return new WaitForSeconds(wait-1);


            if (randomNum == 1)
            {
                currentState.text = "You Lost A Life";
                playerHeart[playerLifeCount].gameObject.SetActive(true);
                playerLifeCount = playerLifeCount + 1;
            }
            else
            {
                currentState.text = "Round Was Empty";
            }
            if (playerLifeCount == 3 || chooLifeCount == 3 || opp1LifeCount == 3 || opp2LifeCount == 3)
                goto Here;
            currentPlayerchooGun.SetActive(false);

            yield return new WaitForSeconds(3);

            yield return bulletArea.StartShowing(false);

            if (totalRounds < 4)
            {
                yield return OpenChooPropBox();
                yield return OpenPlayerPropBox();
            }
            totalRounds = totalRounds + 1;

            //currentPlayeropp2Gun.SetActive(true);

            //randomNum = Random.Range(0, 2);
            //opp1Anim.SetTrigger(trigger);


            //yield return new WaitForSeconds(1);
            //SoundManager.Instance.PlaySound(SoundName.gunshot);
            //yield return new WaitForSeconds(wait - 1);



            //if (randomNum == 0)
            //{
            //    opp2Heart[opp2LifeCount].gameObject.SetActive(true);
            //    opp2LifeCount = opp2LifeCount + 1;
            //}
            //if (playerLifeCount == 3 || chooLifeCount == 3 || opp1LifeCount == 3 || opp2LifeCount == 3)
            //    goto Here;
            //currentPlayeropp2Gun.SetActive(false);




            //yield return new WaitForSeconds(2);

            //currentPlayeropp1Gun.SetActive(true);


            //randomNum = Random.Range(0, 2);
            //opp2Anim.SetTrigger(trigger);


            //yield return new WaitForSeconds(1);
            //SoundManager.Instance.PlaySound(SoundName.gunshot);
            //yield return new WaitForSeconds(wait - 1);



            //if (randomNum == 0)
            //{
            //    opp1Heart[opp1LifeCount].gameObject.SetActive(true);
            //    opp1LifeCount = opp1LifeCount + 1;
            //}
            //if (playerLifeCount == 3 || chooLifeCount == 3 || opp1LifeCount == 3 || opp2LifeCount == 3)
            //    goto Here;

            //currentPlayeropp1Gun.SetActive(false);
            roundCount = roundCount - 1;
        }
        Here:
        if (playerLifeCount == 3)
        {
            playerAnim.SetTrigger("Die");
            ShowLevelFailWithDelay(4);
        }
        else if(chooLifeCount == 3)
        {
            chooAnim.SetTrigger("Die");
            ShowLevelComplteWithDelay(4);
        }
        else
        {
            StartGame();
        }
    }

    public GameObject PausePanel;
    public GameObject LevelCompletePanel;
    public GameObject LevelFailedPanel;
    public BulletArea bulletArea;

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
    public int playerPropPosNum;
    public BoxHeads playerBox;

    public int chooPropPosNum;
    public BoxHeads chooBox;
    public IEnumerator OpenChooPropBox()
    {
        chooBox.mainBox.anim.SetTrigger("Open");
        yield return new WaitForSeconds(1.5f);

        for (int i = 0; i < 2; i++)
        {
            int random = Random.Range(0, chooBox.mainBox.props.Length);
            yield return chooBox.mainBox.GetPropToTopPosition(random);

            yield return new WaitForSeconds(1);

            bool isReached = false;
            DOTween.To(() => chooBox.mainBox.props[random].position, x => chooBox.mainBox.props[random].position = x, chooBox.propPOsition.pos[chooPropPosNum].toPos.position, 1.5f).OnComplete(() =>
            {
                chooPropPosNum++;
                isReached = true;
            });
            yield return new WaitUntil(() => isReached);
            chooBox.propPOsition.pos[chooPropPosNum - 1].props[random].gameObject.SetActive(true);
            chooBox.propPOsition.pos[chooPropPosNum - 1].id = random;
            chooBox.mainBox.props[random].gameObject.SetActive(false);
            chooBox.mainBox.props[random].position = chooBox.mainBox.propStartPos[random];
        }
        chooBox.mainBox.anim.SetTrigger("Close");
        yield return new WaitForSeconds(2f);
    }
    public IEnumerator OpenPlayerPropBox()
    {
        playerBox.mainBox.anim.SetTrigger("Open");
        yield return new WaitForSeconds(2f);

        for (int i = 0; i < 2; i++)
        {
            int random = Random.Range(0, playerBox.mainBox.props.Length);
            yield return playerBox.mainBox.GetPropToTopPosition(random);

            yield return new WaitForSeconds(1);

            bool isReached = false;
            DOTween.To(() => playerBox.mainBox.props[random].position, x => playerBox.mainBox.props[random].position = x, playerBox.propPOsition.pos[playerPropPosNum].toPos.position, 1.5f).OnComplete(() =>
            {
                playerPropPosNum++;
                isReached = true;
            });
            yield return new WaitUntil(() => isReached);
            playerBox.propPOsition.pos[playerPropPosNum - 1].props[random].gameObject.SetActive(true);
            playerBox.propPOsition.pos[playerPropPosNum - 1].id = random;
            playerBox.mainBox.props[random].gameObject.SetActive(false);
            playerBox.mainBox.props[random].position = playerBox.mainBox.propStartPos[random];
        }
        playerBox.mainBox.anim.SetTrigger("Close");
        yield return new WaitForSeconds(2f);

    }

    [System.Serializable]
    public struct BoxHeads
    {
        public BoxHead mainBox;
        public PropPOsition propPOsition;
    }
}