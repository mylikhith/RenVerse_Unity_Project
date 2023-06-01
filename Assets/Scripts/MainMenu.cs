using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject selectWeapons;
    public GameObject mainMenu;
    public static string PlayScene = "ZombieLand";
    // public GameObject[] btns;

    public void OnSelectWeapon()
    {
        selectWeapons.SetActive(true);
        mainMenu.SetActive(false);

        // foreach (var i in ReneverseManager.UserAssets)
        // {
        //     foreach (var j in ReneverseManager.GameNFT)
        //     {
        //         if(i.Key == j.Name.ToString()){
        //             Debug.Log($"000000000000000000000000000000{i.Key}");
        //         }
        //     }
        // }
        // Debug.Log("foer loop");
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
