using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    public GameObject[] players, selectors;
    public string[] playerNames;
    public Text playerNameTxt;
    public float[] attack, defence, speed, damage;
    public Image attackFill, defenceFill, speedFill, damageFill;

    void OnEnable()
    {
        SelectCharacter(PlayerPrefs.GetInt(SharedPrefs.CharacterIndex));
    }
    public void SelectCharacter(int num) 
    {
        for (int i = 0; i < players.Length; i++)
        {
            selectors[i].SetActive(false);
            players[i].SetActive(false);
        }
        playerNameTxt.text = playerNames[num];
        players[num].SetActive(true);
        selectors[num].SetActive(true);
        attackFill.fillAmount = attack[num];
        defenceFill.fillAmount = defence[num];
        speedFill.fillAmount = speed[num];
        damageFill.fillAmount = damage[num];
        PlayerPrefs.SetInt(SharedPrefs.CharacterIndex, num);
    }
}
