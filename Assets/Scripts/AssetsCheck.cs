using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssetsCheck : MonoBehaviour
{

    public string AssetName;
    public int mint;

    public GameObject mintbtn;
    public GameObject selectbtn;
    public bool AKGun = true;
    public static bool ShortGun = false;
    public static bool AugGun = false;

    // public Button select;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (ReneverseManager.UserAssets.ContainsKey(AssetName))
        {
            for (int i = 0; i < mint; i++)
            {
                mintbtn.SetActive(false);
                selectbtn.SetActive(true);
            }
        }

        if (ReneverseManager.UserAssets.ContainsKey("AugGun"))
        {
            AugGun = true;
        }
        if (ReneverseManager.UserAssets.ContainsKey("ShotGun"))
        {
            ShortGun = true;
        }
    }
}
