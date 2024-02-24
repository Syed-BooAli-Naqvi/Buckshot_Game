using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuHandler : MonoBehaviour
{
    public GameObject SettingsPanel;
    public string PrivacyURL;
    public string RateUsURL;
    public string MoreGamesURL;
    public void PlayGame()
    {
        LoadingScript.Instance.loadscene(2);
    }


    public void opensettings()
    {
        SettingsPanel.SetActive(true);
    }


    public void closesettings()
    {
        SettingsPanel.SetActive(false);
    }

    public void openprivacy()
    {
        Application.OpenURL(PrivacyURL);
    }
    public void openrateus()
    {
        Application.OpenURL(RateUsURL);

    }
    public void openmoregames()
    {
        Application.OpenURL(MoreGamesURL);

    }
}
