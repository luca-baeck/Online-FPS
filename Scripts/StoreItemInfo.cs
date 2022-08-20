// Decompiled with JetBrains decompiler
// Type: StoreItemInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using PlayFab;
using PlayFab.ClientModels;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoreItemInfo : MonoBehaviour
{
  public GameObject imBesitz;
  public Text NameT;
  public Text PriceT;
  public GameObject PlayerShop;
  public Material skin;
  public string name;
  private int Price;
  private string Currency;
  public GameObject canvas;
  private string _nameR;
  private string _priceR;
  private string _currencyR;

  private void Start()
  {
    this.PlayerShop = GameObject.FindWithTag("PlayerShop");
    this.canvas = GameObject.Find("Canvas");
  }

  public void SetUp(string _name, string _price, string _currency)
  {
    this._nameR = _name;
    this._priceR = _price;
    this._currencyR = _currency;
    this.Invoke("RealSetUp", 1f);
  }

  private void RealSetUp()
  {
    this.name = this._nameR;
    this.NameT.text = this._nameR;
    MonoBehaviour.print((object) (this._priceR + this._currencyR));
    this.Price = int.Parse(this._priceR);
    this.Currency = this._currencyR;
    this.PriceT.text = "Preis: " + this._priceR + this._currencyR;
    this.skin = UnityEngine.Resources.Load("SkinMaterials/" + this.name, typeof (Material)) as Material;
    this.imBesitz.SetActive(false);
    foreach (ItemInstance itemInstance in ShopManager.Inventory.Inventory)
    {
      if (itemInstance.ItemId == this.name)
      {
        this.imBesitz.SetActive(true);
        break;
      }
      this.imBesitz.SetActive(false);
    }
  }

  public void PreView() => this.PlayerShop.GetComponent<MeshRenderer>().material = this.skin;

  public void Buy()
  {
    this.canvas.SetActive(false);
    this.gameObject.SetActive(false);
    PlayFabClientAPI.PurchaseItem(new PurchaseItemRequest()
    {
      ItemId = this.name,
      VirtualCurrency = this.Currency,
      Price = this.Price
    }, (Action<PurchaseItemResult>) (result => this.OnPurchaseItemResult(result)), (Action<PlayFabError>) (error => this.OnPurchaseItemError(error)));
  }

  private void OnPurchaseItemResult(PurchaseItemResult result) => this.Invoke("LoadLobby", 1f);

  private void OnPurchaseItemError(PlayFabError error)
  {
    MonoBehaviour.print((object) error);
    this.Invoke("LoadLobby", 1f);
  }

  private void LoadLobby()
  {
    this.gameObject.SetActive(true);
    SceneManager.LoadScene("Shop");
  }
}
