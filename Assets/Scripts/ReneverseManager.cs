using Rene.Sdk;
using Rene.Sdk.Api.Game.Data;
using ReneVerse;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class ReneverseManager : MonoBehaviour
{

    public static bool LoginStatus = false;

    public static string EmailHandler;

    // The TextMeshPro Inputfield
    public GameObject Email;
    // The Timer Text Field which would update every second
    public TextMeshProUGUI Timer;

    // The parent panet which contains both sign in and countdown panel
    public GameObject LogInPanel;
    // The Countdown panel which contains the timer
    public GameObject CountdownPanel;

    public static List<Asset> NFTCounter = new();

    public static Dictionary<string, bool> UserAssets = new Dictionary<string, bool>();

    API ReneAPI;

    public void Start()
    {
        UserAssets["Megan"] = true;
        if (LoginStatus) LogInPanel.SetActive(false);

        // foreach (var pair in UserAssets)
        // {
        //     Debug.Log($"Employee with key {pair.Key}: Id={pair.Value}");
        // }
    }

    public async void SignIn()
    {
        await ConnectUser();
    }

    // public async void Mint(string _name) {
    //     await AssetMint(_name);
    // }

    public void Mint(string _name)
    {
        Debug.Log("minted success");
        UserAssets[_name] = true;
    }

    async Task ConnectUser()
    {
        ReneAPI = ReneAPIManager.API();
        EmailHandler = Email.GetComponent<TMP_InputField>().text;
        bool connected = await ReneAPI.Game().Connect(EmailHandler);
        Debug.Log(connected);
        if (!connected) return;
        StartCoroutine(ConnectReneService(ReneAPI));
    }

    private IEnumerator ConnectReneService(API reneApi)
    {
        CountdownPanel.SetActive(true);
        var counter = 30;
        var userConnected = false;
        var secondsToDecrement = 1;
        while (counter >= 0 && !userConnected)
        {
            Timer.text = counter.ToString();
            if (reneApi.IsAuthorized())
            {

                CountdownPanel.SetActive(false);
                LogInPanel.SetActive(false);


                // Will Fetch our assets from here
                yield return GetUserAssetsAsync(reneApi);


                userConnected = true;
                LoginStatus = true;
            }

            yield return new WaitForSeconds(secondsToDecrement);
            counter -= secondsToDecrement;
        }
        CountdownPanel.SetActive(false);
    }

    public static bool IsAssetLoaded = false;

    public static List<GameAsset> GameNFT = new();


    private async Task GetUserAssetsAsync(API reneApi)
    {

        AssetsResponse.AssetsData userAssets = await reneApi.Game().Assets();

        // userAssets?.Items.ForEach
        //     (asset =>
        //     {
        //         Debug.Log
        //         ($" - Asset Id '{asset.NftId}' Name '{asset.Metadata.Name}' Wallet '{asset.WalletAddress}' TempletId '{asset.AssetTemplateId}' GameId '{asset.GameId}' Description '{asset.Metadata.Description} TempletId {asset.AssetTemplateId}");

        //         // foreach (AssetsResponse.AssetsData.Asset.AssetMetadata.AssetAttribute attribute in asset.Metadata.Attributes)
        //         // {
        //         //     Debug.Log("DisplayType: " + attribute.DisplayType);
        //         //     Debug.Log("TraitType: " + attribute.TraitType);
        //         //     Debug.Log("Value: " + attribute.Value);
        //         //     Debug.Log("MaxValue: " + attribute.MaxValue);
        //         //     Debug.Log("");
        //         // }
        //     });
        userAssets?.Items.ForEach(asset =>
        {
            Asset thisAsset = new(asset.Metadata.Name, asset.Metadata.Description, asset.Metadata.Image, asset.AssetTemplateId);
            Debug.Log(asset.Metadata.Image);
            NFTCounter.Add(thisAsset);

        });
        IsAssetLoaded = true;


        AssetTemplatesResponse.AssetTemplatesData gameAssets = await reneApi.Game().AssetTemplates();
        gameAssets.Items.ForEach(asset =>
        {
            try
            {
                GameAsset newAsset = new(asset.AssetTemplateId, asset.Name, asset.Data.Description, asset.Files.Images[0].Url, asset.Data.Supply, asset.Data.Price);
                GameNFT.Add(newAsset);
            }
            catch (System.Exception)
            {
                try
                {
                    GameAsset newAssetReTry = new(asset.AssetTemplateId, asset.Name, asset.Data.Description, "", asset.Data.Supply, asset.Data.Price);
                    GameNFT.Add(newAssetReTry);
                }
                catch (System.Exception)
                {
                    Debug.Log("error");
                }
            }

            // Debug.Log($" AID {asset.AssetTemplateId} Name {asset.Name} {gameAssets.Items.Count}");
            // Debug.Log(asset.Data.Description);
        });
        Debug.Log(GameNFT.Count);

        // gameAssets.Items.ForEach(a => {
        //     Debug.Log(a.Name);
        // });

        // MEGAN e9914db4-b15f-47e8-90a1-61a8360865d7

        // tempData?.Items.ForEach(a => {
        //     a.Files.Images.ForEach(a=>{
        //         Debug.Log(a.UploadUrl.ToString());
        //         Debug.Log(a.Url.ToString());
        //         Debug.Log(a.Url);
        //     });
        //     Debug.Log(a.AssetTemplateId.ToString());
        // });

        // userid 029f961d-3f26-47d6-95d6-2520487f6f46

        userAssets?.Items.ForEach(asset =>
        {
            // Debug.Log(asset.Metadata.Image);
            UserAssets[asset.Metadata.Name.ToString()] = true;
        });
    }

    public async Task AssetMint(string _AssetName)
    {

        Debug.Log("mint Started");

        if (_AssetName == "Shooter" && !UserAssets.ContainsKey("Shooter"))
        {

            string assetTemplateId = "fb54a9f7-f93f-492a-8e4d-502827156b49";

            AssetMetadata.AssetAttribute Gender = new()
            {
                displayType = "text",
                traitType = "Gender",
                value = "Male"
            };

            AssetMetadata assetMetadata = new()
            {
                name = _AssetName,
                description = "Zombie Killer Game Male Character",
                imageFilename = "ShooterAim",
                animationFilename = null,
                attributes = new List<AssetMetadata.AssetAttribute>()
                {
                    Gender
                },
            };

            try
            {
                var Response = await ReneAPI.Game().AssetMint(assetTemplateId, assetMetadata, true);
                Debug.Log(Response);
                UserAssets[_AssetName] = true;
                Debug.Log("Asset Shooter Minted :)");
            }
            catch (Exception e)
            {
                Debug.Log(e);
                Debug.Log("Asset Shooter Mint Faoled!");
            }
        }
    }
}

public class Asset
{
    public string AssetName;
    public string Description;
    public string AssetUrl;
    public string TemplateID;
    public Asset(string assetName, string description, string assetUrl, string templateID)
    {
        AssetName = assetName;
        Description = description;
        AssetUrl = assetUrl;
        TemplateID = templateID;
    }
}

public class GameAsset
{
    public string AssetTemplateId;
    public string Name;
    public string Description;
    public string Url;
    public int? Supply;
    public double? Price;
    public GameAsset(string templateID, string assetName, string description, string assetUrl, int? supply, double? price)
    {
        AssetTemplateId = templateID;
        Name = assetName;
        Description = description;
        Url = assetUrl;
        Supply = supply;
        Price = price;
    }
}
// MEGAN e9914db4-b15f-47e8-90a1-61a8360865d7
// Shooter fb54a9f7-f93f-492a-8e4d-502827156b49