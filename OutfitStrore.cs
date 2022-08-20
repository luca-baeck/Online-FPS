// Decompiled with JetBrains decompiler
// Type: OutfitStrore
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections.Generic;
using UnityEngine;

public class OutfitStrore : MonoBehaviour
{
  public GameObject StoreItem;
  public Transform content;

  private void Start() => PlayFabClientAPI.GetStoreItems(new GetStoreItemsRequest()
  {
    StoreId = "OutfitsStore"
  }, new Action<GetStoreItemsResult>(this.Success), new Action<PlayFabError>(this.Fail));

  private void Fail(PlayFabError obj) => PlayFabClientAPI.GetStoreItems(new GetStoreItemsRequest()
  {
    StoreId = "OutfitsStore"
  }, new Action<GetStoreItemsResult>(this.Success), new Action<PlayFabError>(this.Fail));

  private void Success(GetStoreItemsResult obj)
  {
    foreach (PlayFab.ClientModels.StoreItem storeItem in obj.Store)
    {
      foreach (KeyValuePair<string, uint> virtualCurrencyPrice in storeItem.VirtualCurrencyPrices)
      {
        if (virtualCurrencyPrice.Key == "CC")
        {
          MonoBehaviour.print((object) (storeItem.ItemId + " Preis: " + (object) virtualCurrencyPrice.Value + virtualCurrencyPrice.Key));
          UnityEngine.Object.Instantiate<GameObject>(this.StoreItem, this.content).GetComponent<StoreItemInfo>().SetUp(storeItem.ItemId, virtualCurrencyPrice.Value.ToString(), virtualCurrencyPrice.Key.ToString());
        }
      }
    }
  }
}
