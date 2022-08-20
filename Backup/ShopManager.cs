// Decompiled with JetBrains decompiler
// Type: ShopManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using Photon.Pun;
using Photon.Realtime;
using PlayFab;
using PlayFab.ClientModels;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopManager : MonoBehaviourPunCallbacks
{
  public Text WährungText;
  public GameObject Player;
  public GameObject Shop;
  public GameObject Laden;
  public static int CcAmount;
  public static GetUserInventoryResult Inventory;

  private void Start()
  {
    Time.timeScale = 1f;
    this.GetInventory();
    this.Laden.SetActive(true);
    this.Shop.SetActive(false);
  }

  public void GetInventory() => PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), new Action<GetUserInventoryResult>(this.OnGetInventory), (Action<PlayFabError>) (error => SceneManager.LoadScene("Lobby")));

  public override void OnDisconnected(DisconnectCause cause)
  {
    PlayFabClientAPI.ForgetAllCredentials();
    SceneManager.LoadScene("LogIn");
  }

  public void OnGetInventory(GetUserInventoryResult result)
  {
    this.Laden.SetActive(false);
    this.Shop.SetActive(true);
    Debug.Log((object) "Received the following items:");
    MonoBehaviour.print((object) result.VirtualCurrency["CC"].ToString());
    this.WährungText.text = result.VirtualCurrency["CC"].ToString() + " C-Coins";
    ShopManager.CcAmount = result.VirtualCurrency["CC"];
    foreach (ItemInstance itemInstance in result.Inventory)
      Debug.Log((object) ("Items (" + itemInstance.DisplayName + "): " + itemInstance.ItemInstanceId));
    ShopManager.Inventory = result;
  }

  private void Update() => this.Rotate();

  public void Lobby() => SceneManager.LoadScene(nameof (Lobby));

  public void Rotate()
  {
    float num = 1250f;
    if (!Input.GetKey(KeyCode.Mouse0))
      return;
    this.Player.transform.Rotate(new Vector3(0.0f, Input.GetAxis("Mouse X"), 0.0f) * Time.deltaTime * num * -1f);
  }
}
