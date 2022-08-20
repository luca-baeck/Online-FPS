// Decompiled with JetBrains decompiler
// Type: SpindItemInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using BayatGames.SaveGameFree;
using UnityEngine;
using UnityEngine.UI;

public class SpindItemInfo : MonoBehaviour
{
  public Text nameT;
  public GameObject PlayerShop;
  public Material skin;
  public string name;
  public GameObject Selected;

  private void Start()
  {
    this.Selected.SetActive(false);
    this.PlayerShop = GameObject.FindWithTag("PlayerShop");
  }

  public void SetUp(string _name)
  {
    MonoBehaviour.print((object) _name);
    this.nameT.text = _name;
    this.name = _name;
    this.skin = UnityEngine.Resources.Load("SkinMaterials/" + this.name, typeof (Material)) as Material;
  }

  private void Update()
  {
    if (this.name == SpindManager.Skin)
      this.Selected.SetActive(true);
    else
      this.Selected.SetActive(false);
  }

  public void Auswaehlen()
  {
    SaveGame.Save<string>("Skin", this.name);
    this.PlayerShop.GetComponent<MeshRenderer>().material = this.skin;
    SpindManager.Skin = this.name;
  }
}
