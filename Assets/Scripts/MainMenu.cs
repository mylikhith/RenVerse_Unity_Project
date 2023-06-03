using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject selectWeapons;
    public GameObject mainMenu;
    public static string PlayScene = "ZombieLand";

    public void OnSelectWeapon()
    {
        selectWeapons.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void OnPlayButton()
    {
        SceneManager.LoadScene(PlayScene);
    }

    public void OnQuitButton()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}
