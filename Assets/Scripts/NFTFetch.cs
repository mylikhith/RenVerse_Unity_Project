using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Rene.Sdk;
using Rene.Sdk.Api.Game.Data;
using ReneVerse;

public class NFTFetch : MonoBehaviour
{
    [Header("Asset Details")]
    public GameAsset NFT;

    [Header("UI Inputs")]
    public Text TemplateID;
    public Text Name;
    public Text Description;
    public Image image;
    public Text supplyText;
    public Text priceText;
    public GameObject NFTPANEL;
    public GameObject TrnasferMenu;
    public static string assetIdHandler;
    public static string userIdHandler;
    public GameObject assetId;
    public GameObject userId;
    public Text NftId;
    public Text GameId;
    public static string AssetName;

    API ReneAPI;

    public void NftTransfer()
    {
        Transfer();
    }

    public void Load(int index)
    {
        LoadNft(index);
    }


    public async void LoadNft(int index)
    {
        GameAsset NFTInfo = ReneverseManager.GameNFT[index];
        AssetName = NFTInfo.Name;
        Initialize(NFTInfo);

        API reneApi = ReneAPIManager.API();
        AssetsResponse.AssetsData userAssets = await reneApi.Game().Assets();

        GameId.text = userAssets.Items[0].GameId;
        foreach (var pair in ReneverseManager.UserAssets)
        {
            Debug.Log($"Employee with key {pair.Key}: Id={pair.Value}");
            if (pair.Key == NFTInfo.Name)
            {
                TrnasferMenu.SetActive(true);
                userAssets?.Items.ForEach
            (asset =>
            {
                if (NFTInfo.Name == asset.Metadata.Name)
                {
                    NftId.text = asset.NftId;
                    Debug.Log($"Nft id{asset.NftId} {asset.GameId} {asset.Metadata.Name}");
                }
                else { GameId.text = asset.GameId; }
            });
            }
        }
    }

    public void Back()
    {
        NFTPANEL.SetActive(false);
        TrnasferMenu.SetActive(false);
        Reset();
    }

    //Initialize the UI content
    public void Initialize(GameAsset asset)
    {
        #region Setting Asset Details
        NFT = asset;
        #endregion

        Name.text = asset.Name;
        Description.text = asset.Description;
        TemplateID.text = asset.AssetTemplateId;
        supplyText.text = asset.Supply.HasValue ? asset.Supply.Value.ToString() : "N/A";
        priceText.text = asset.Price.HasValue ? asset.Price.Value.ToString() : "N/A";
        StartCoroutine(FetchImage(asset.Url));
    }

    public void Reset()
    {
        Name.text = "";
        Description.text = "";
        TemplateID.text = "";
        supplyText.text = "N/A";
        priceText.text = "N/A";
        NftId.text = "";
        image.overrideSprite = Sprite.Create(Texture2D.blackTexture, new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f));
    }

    //Fetching Image Texture from URL
    IEnumerator FetchImage(string ImageLink)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(ImageLink);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);

            string ErrorImagelink = "https://i0.wp.com/whatvwant.com/wp-content/uploads/2015/09/Unable-to-load.png";
            
            UnityWebRequest wwwErrorImg = UnityWebRequestTexture.GetTexture(ErrorImagelink);
            yield return wwwErrorImg.SendWebRequest();

            Texture2D myTexture = ((DownloadHandlerTexture)wwwErrorImg.downloadHandler).texture;
            Sprite newSprite = Sprite.Create(myTexture, new Rect(0, 0, myTexture.width, myTexture.height), new Vector2(.5f, .5f));

            image.overrideSprite = newSprite;
        }
        else
        {
            Texture2D myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            Sprite newSprite = Sprite.Create(myTexture, new Rect(0, 0, myTexture.width, myTexture.height), new Vector2(.5f, .5f));

            image.overrideSprite = newSprite;
        }
    }

    public async void Transfer()
    {
        assetIdHandler = assetId.GetComponent<TMP_InputField>().text;
        userIdHandler = userId.GetComponent<TMP_InputField>().text;
        ReneAPI = ReneAPIManager.API();
        try
        {
            var Response = await ReneAPI.Game().AssetTransfer(assetIdHandler, userIdHandler);
            Debug.Log(Response);
            ReneverseManager.UserAssets[AssetName] = false;
            TrnasferMenu.SetActive(false);

        }
        catch (System.Exception)
        {
            Debug.Log("Transfer Failed");
        }
    }
}