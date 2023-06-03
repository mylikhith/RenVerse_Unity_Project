using Rene.Sdk;
using Rene.Sdk.Api.Game.Data;
using ReneVerse;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using Rene.Sdk.Api.User.Data;
using Rene.Sdk.Http;

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
    public static bool IsAssetLoaded = false;
    public static List<GameAsset> GameNFT = new();
    public ErrorHandler errorHandler;

    ReneAPICreds _reneAPICreds;
    public static API ReneAPI;

    public void Start()
    {
        UserAssets["Megan"] = true;
        _reneAPICreds = ScriptableObject.CreateInstance<ReneAPICreds>();
        if (LoginStatus) LogInPanel.SetActive(false);
    }

    public async void SignIn()
    {
        await ConnectUser();
    }

    public async void Mint(string _name)
    {
        await AssetMint(_name);
    }

    private Coroutine connectCoroutine;

    async Task ConnectUser()
    {
        ReneAPI = ReneAPIManager.API();
        EmailHandler = Email.GetComponent<TMP_InputField>().text;

        Action<GraphQLHttpRequestException> onGraphQlHttpRequestException = null;
        int waitToConnectForSeconds = 60;
        bool connected = await ReneAPI.Game().Connect(EmailHandler, onGraphQlHttpRequestException, waitToConnectForSeconds);

        Debug.Log(connected);
        if (!connected) return;

        if (connectCoroutine != null)
        {
            StopCoroutine(connectCoroutine);
            connectCoroutine = null;
        }

        connectCoroutine = StartCoroutine(ConnectReneService(ReneAPI));

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

                yield return GetUserAssetsAsync(reneApi);

                userConnected = true;
                LoginStatus = true;
            }

            yield return new WaitForSeconds(secondsToDecrement);
            counter -= secondsToDecrement;
        }
        CountdownPanel.SetActive(false);
    }

    private async Task GetUserAssetsAsync(API reneApi)
    {

        AssetsResponse.AssetsData userAssets = await reneApi.Game().Assets();
        // Debug.Log(userAssets.Items.Count);

        userAssets?.Items.ForEach(asset =>
        {
            Asset thisAsset = new(asset.Metadata.Name, asset.Metadata.Description, asset.Metadata.Image, asset.AssetTemplateId);
            NFTCounter.Add(thisAsset);
            // Debug.Log($"User assets Name {asset.Metadata.Name} TempletId {asset.AssetTemplateId} NFTID {asset.NftId}");
        });
        IsAssetLoaded = true;

        AssetTemplatesResponse.AssetTemplatesData gameAssets = await reneApi.Game().AssetTemplates();
        gameAssets.Items.ForEach(asset =>
        {
            // Debug.Log($"Game assets Name {asset.Name} TempletId {asset.AssetTemplateId}");
            try
            {
                // Debug.Log($"game assets = Name {asset.Name} TempletId {asset.AssetTemplateId} NFTID");
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
        });
        // Debug.Log(GameNFT.Count);

        userAssets?.Items.ForEach(asset =>
        {
            UserAssets[asset.Metadata.Name.ToString()] = true;
        });
    }

    public async Task AssetMint(string _AssetName)
    {
        Debug.Log("mint Started");

        if (_AssetName == "Shooter" && !UserAssets.ContainsKey("Shooter"))
        {
            string assetTemplateId = "fb54a9f7-f93f-492a-8e4d-502827156b49";

            try
            {
                var Response = await ReneAPI.Game().AssetMint(assetTemplateId);
                Debug.Log(Response);
                UserAssets[_AssetName] = true;
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }

            await GetUserAssetsAsync(ReneAPI);

        }

        if (_AssetName == "AugGun" && !UserAssets.ContainsKey("AugGun"))
        {
            string assetTemplateId = "49fc8a77-6026-4301-89e7-c5d2af2f130d";

            try
            {
                var Response = await ReneAPI.Game().AssetMint(assetTemplateId);
                Debug.Log(Response);
                UserAssets[_AssetName] = true;
                errorHandler.ErrorMessage("Asset AugGun Minted :)");
            }
            catch (Exception e)
            {
                errorHandler.ErrorMessage("Asset AugGun Mint Failed!");
                Debug.Log(e);
            }

            await GetUserAssetsAsync(ReneAPI);
        }

        if (_AssetName == "ShotGun" && !UserAssets.ContainsKey("ShotGun"))
        {

            string assetTemplateId = "b79c6cb9-a60f-4764-9baf-ceae7f064ee3";

            try
            {
                var Response = await ReneAPI.Game().AssetMint(assetTemplateId);
                Debug.Log(Response);
                UserAssets[_AssetName] = true;
                errorHandler.ErrorMessage("Asset ShotGun Minted :)");
            }
            catch (Exception e)
            {
                errorHandler.ErrorMessage("Asset ShotGun Mint Failed!");
                Debug.Log(e);
            }

            await GetUserAssetsAsync(ReneAPI);
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

