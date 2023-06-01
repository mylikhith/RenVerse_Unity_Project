using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectWeapon : MonoBehaviour
{
    public GameObject selectCharacter;
    public GameObject selectWeapons;
    public GameObject mainMenu;
    public GameObject NFTPANEL;

    public void OnBackButton() {
        selectCharacter.SetActive(false);
        selectWeapons.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void SelectCharacterBtn() {
        selectCharacter.SetActive(true);
        selectWeapons.SetActive(false);
        mainMenu.SetActive(false);
    }

    public void LoadNFT()
    {
        NFTPANEL.SetActive(true);
    }
}
