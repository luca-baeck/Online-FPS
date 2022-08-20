// Decompiled with JetBrains decompiler
// Type: SpindManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using BayatGames.SaveGameFree;
using Photon.Pun;
using Photon.Realtime;
using PlayFab;
using PlayFab.ClientModels;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpindManager : MonoBehaviourPunCallbacks
{
  public static string Skin;
  public GameObject Player;
  public GameObject Spind;
  public GameObject Laden;
  public GameObject SpindItemPrefab;
  public Transform content;

  private void Start()
  {
    if (SaveGame.Exists("Skin"))
      SpindManager.Skin = SaveGame.Load<string>("Skin");
    if (SpindManager.Skin != null)
    {
      Material material = Resources.Load("SkinMaterials/" + SpindManager.Skin, typeof (Material)) as Material;
      this.Player.GetComponent<MeshRenderer>().material = material;
    }
    this.Laden.SetActive(true);
    this.Spind.SetActive(false);
    this.GetInventory();
  }

  public void GetInventory() => PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), new Action<GetUserInventoryResult>(this.OnGetInventory), (Action<PlayFabError>) (error => SceneManager.LoadScene("Lobby")));

  public void OnGetInventory(GetUserInventoryResult result)
  {
    this.Laden.SetActive(false);
    this.Spind.SetActive(true);
    Debug.Log((object) "Received the following items:");
    MonoBehaviour.print((object) result.VirtualCurrency["CC"].ToString());
    foreach (ItemInstance itemInstance in result.Inventory)
    {
      Debug.Log((object) ("Items (" + itemInstance.DisplayName + "): " + itemInstance.ItemInstanceId));
      UnityEngine.Object.Instantiate<GameObject>(this.SpindItemPrefab, this.content).GetComponent<SpindItemInfo>().SetUp(itemInstance.ItemId);
    }
  }

  public void Rotate()
  {
    float num = 1250f;
    if (!Input.GetKey(KeyCode.Mouse0))
      return;
    this.Player.transform.Rotate(new Vector3(0.0f, Input.GetAxis("Mouse X"), 0.0f) * Time.deltaTime * num * -1f);
  }

  private void Update() => this.Rotate();

  public void Lobby() => SceneManager.LoadScene(nameof (Lobby));

  public override void OnDisconnected(DisconnectCause cause)
  {
    PlayFabClientAPI.ForgetAllCredentials();
    SceneManager.LoadScene("LogIn");
  }
}
