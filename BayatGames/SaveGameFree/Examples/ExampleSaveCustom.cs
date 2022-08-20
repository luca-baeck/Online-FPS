// Decompiled with JetBrains decompiler
// Type: BayatGames.SaveGameFree.Examples.ExampleSaveCustom
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BayatGames.SaveGameFree.Examples
{
  public class ExampleSaveCustom : MonoBehaviour
  {
    public ExampleSaveCustom.CustomData customData;
    public bool loadOnStart = true;
    public InputField scoreInputField;
    public InputField highScoreInputField;
    public string identifier = "exampleSaveCustom";

    private void Start()
    {
      if (!this.loadOnStart)
        return;
      this.Load();
    }

    public void SetScore(string score) => this.customData.score = int.Parse(score);

    public void SetHighScore(string highScore) => this.customData.highScore = int.Parse(highScore);

    public void Save() => SaveGame.Save<ExampleSaveCustom.CustomData>(this.identifier, this.customData, SerializerDropdown.Singleton.ActiveSerializer);

    public void Load()
    {
      this.customData = SaveGame.Load<ExampleSaveCustom.CustomData>(this.identifier, new ExampleSaveCustom.CustomData(), SerializerDropdown.Singleton.ActiveSerializer);
      this.scoreInputField.text = this.customData.score.ToString();
      this.highScoreInputField.text = this.customData.highScore.ToString();
    }

    [Serializable]
    public struct Level
    {
      public bool unlocked;
      public bool completed;

      public Level(bool unlocked, bool completed)
      {
        this.unlocked = unlocked;
        this.completed = completed;
      }
    }

    [Serializable]
    public class CustomData
    {
      public int score;
      public int highScore;
      public List<ExampleSaveCustom.Level> levels;

      public CustomData()
      {
        this.score = 0;
        this.highScore = 0;
        this.levels = new List<ExampleSaveCustom.Level>()
        {
          new ExampleSaveCustom.Level(true, false),
          new ExampleSaveCustom.Level(false, false),
          new ExampleSaveCustom.Level(false, true),
          new ExampleSaveCustom.Level(true, false)
        };
      }
    }
  }
}
