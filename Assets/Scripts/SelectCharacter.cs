using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectCharacter : MonoBehaviour
{
    public GameObject selectCharacter;
    public GameObject selectWeapons;
    public GameObject mainMenu;
    public GameObject NFTPANEL;
    public Text selectCh1;
    public Text selectCh2;


    public void OnBackButton()
    {
        selectCharacter.SetActive(false);
        mainMenu.SetActive(false);
        selectWeapons.SetActive(true);
    }

    public void MainMenuBtn()
    {
        selectCharacter.SetActive(false);
        selectWeapons.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void OnCharacter1()
    {
        // SceneManager.LoadScene("ZombieLand");
        selectCh1.text = "Selected";
        selectCh1.color = Color.white;
        selectCh2.text = "Select";
        selectCh2.color = Color.black;
        MainMenu.PlayScene = "ZombieLand";
    }

    public void OnCharacter2()
    {
        // SceneManager.LoadScene("ZombieLand1");
        selectCh2.text = "Selected";
        selectCh2.color = Color.white;
        selectCh1.text = "Select";
        selectCh1.color = Color.black;
        MainMenu.PlayScene = "ZombieLand1";
    }

    public void LoadNFT()
    {
        NFTPANEL.SetActive(true);
    }
}
