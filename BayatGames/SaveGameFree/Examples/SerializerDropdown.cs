// Decompiled with JetBrains decompiler
// Type: BayatGames.SaveGameFree.Examples.SerializerDropdown
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using BayatGames.SaveGameFree.Encoders;
using BayatGames.SaveGameFree.Serializers;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace BayatGames.SaveGameFree.Examples
{
  public class SerializerDropdown : Dropdown
  {
    private static SerializerDropdown m_Singleton;
    private static ISaveGameSerializer[] m_Serializers = new ISaveGameSerializer[3]
    {
      (ISaveGameSerializer) new SaveGameXmlSerializer(),
      (ISaveGameSerializer) new SaveGameJsonSerializer(),
      (ISaveGameSerializer) new SaveGameBinarySerializer()
    };
    protected ISaveGameSerializer m_ActiveSerializer;

    public static SerializerDropdown Singleton => SerializerDropdown.m_Singleton;

    public ISaveGameSerializer ActiveSerializer
    {
      get
      {
        if (this.m_ActiveSerializer == null)
          this.m_ActiveSerializer = (ISaveGameSerializer) new SaveGameJsonSerializer();
        return this.m_ActiveSerializer;
      }
    }

    protected override void Awake()
    {
      if ((Object) SerializerDropdown.m_Singleton != (Object) null)
      {
        Object.Destroy((Object) this.gameObject);
      }
      else
      {
        SerializerDropdown.m_Singleton = this;
        base.Awake();
        this.options = new List<Dropdown.OptionData>()
        {
          new Dropdown.OptionData("XML"),
          new Dropdown.OptionData("JSON"),
          new Dropdown.OptionData("Binary")
        };
        this.onValueChanged.AddListener(new UnityAction<int>(this.OnValueChanged));
        this.value = SaveGame.Load<int>("serializer", 0, false, "", (ISaveGameSerializer) new SaveGameJsonSerializer(), (ISaveGameEncoder) null, SaveGame.DefaultEncoding, SaveGame.SavePath);
      }
    }

    protected virtual void OnValueChanged(int index) => this.m_ActiveSerializer = SerializerDropdown.m_Serializers[index];

    protected virtual void OnApplicationQuit() => SaveGame.Save<int>("serializer", this.value, false, "", (ISaveGameSerializer) new SaveGameJsonSerializer(), (ISaveGameEncoder) null, SaveGame.DefaultEncoding, SaveGame.SavePath);
  }
}
