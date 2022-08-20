// Decompiled with JetBrains decompiler
// Type: BayatGames.SaveGameFree.SaveGameAuto
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using BayatGames.SaveGameFree.Encoders;
using BayatGames.SaveGameFree.Serializers;
using BayatGames.SaveGameFree.Types;
using System.Text;
using UnityEngine;

namespace BayatGames.SaveGameFree
{
  [AddComponentMenu("Save Game Free/Auto Save")]
  public class SaveGameAuto : MonoBehaviour
  {
    [Header("Settings")]
    [Space]
    [Tooltip("You must specify a value for this to be able to save it.")]
    public string positionIdentifier = "enter the position identifier";
    [Tooltip("You must specify a value for this to be able to save it.")]
    public string rotationIdentifier = "enter the rotation identifier";
    [Tooltip("You must specify a value for this to be able to save it.")]
    public string scaleIdentifier = "enter the scale identifier";
    [Tooltip("Encode the data?")]
    public bool encode;
    [Tooltip("If you leave it blank this will reset to it's default value.")]
    public string encodePassword = "";
    [Tooltip("Which serialization format?")]
    public SaveGameAuto.SaveFormat format = SaveGameAuto.SaveFormat.JSON;
    [Tooltip("If you leave it blank this will reset to it's default value.")]
    public ISaveGameSerializer serializer;
    [Tooltip("If you leave it blank this will reset to it's default value.")]
    public ISaveGameEncoder encoder;
    [Tooltip("If you leave it blank this will reset to it's default value.")]
    public Encoding encoding;
    [Tooltip("Where to save? (PersistentDataPath highly recommended).")]
    public SaveGamePath savePath;
    [Tooltip("Reset the empty fields to their default value.")]
    public bool resetBlanks = true;
    [Header("What to Save?")]
    [Space]
    [Tooltip("Save Position?")]
    public bool savePosition = true;
    [Tooltip("Save Rotation?")]
    public bool saveRotation = true;
    [Tooltip("Save Scale?")]
    public bool saveScale = true;
    [Header("Defaults")]
    [Space]
    [Tooltip("Default Position Value")]
    public Vector3 defaultPosition = Vector3.zero;
    [Tooltip("Default Rotation Value")]
    public Vector3 defaultRotation = Quaternion.identity.eulerAngles;
    [Tooltip("Default Scale Value")]
    public Vector3 defaultScale = Vector3.one;
    [Header("Save Events")]
    [Space]
    [Tooltip("Save on Awake()")]
    public bool saveOnAwake;
    [Tooltip("Save on Start()")]
    public bool saveOnStart;
    [Tooltip("Save on OnEnable()")]
    public bool saveOnEnable;
    [Tooltip("Save on OnDisable()")]
    public bool saveOnDisable = true;
    [Tooltip("Save on OnApplicationQuit()")]
    public bool saveOnApplicationQuit = true;
    [Tooltip("Save on OnApplicationPause()")]
    public bool saveOnApplicationPause;
    [Header("Load Events")]
    [Space]
    [Tooltip("Load on Awake()")]
    public bool loadOnAwake;
    [Tooltip("Load on Start()")]
    public bool loadOnStart = true;
    [Tooltip("Load on OnEnable()")]
    public bool loadOnEnable;

    protected virtual void Awake()
    {
      if (this.resetBlanks)
      {
        if (string.IsNullOrEmpty(this.encodePassword))
          this.encodePassword = SaveGame.EncodePassword;
        if (this.serializer == null)
          this.serializer = SaveGame.Serializer;
        if (this.encoder == null)
          this.encoder = SaveGame.Encoder;
        if (this.encoding == null)
          this.encoding = SaveGame.DefaultEncoding;
      }
      switch (this.format)
      {
        case SaveGameAuto.SaveFormat.XML:
          this.serializer = (ISaveGameSerializer) new SaveGameXmlSerializer();
          break;
        case SaveGameAuto.SaveFormat.JSON:
          this.serializer = (ISaveGameSerializer) new SaveGameJsonSerializer();
          break;
        case SaveGameAuto.SaveFormat.Binary:
          this.serializer = (ISaveGameSerializer) new SaveGameBinarySerializer();
          break;
      }
      if (this.loadOnAwake)
        this.Load();
      if (!this.saveOnAwake)
        return;
      this.Save();
    }

    protected virtual void Start()
    {
      if (this.loadOnStart)
        this.Load();
      if (!this.saveOnStart)
        return;
      this.Save();
    }

    protected virtual void OnEnable()
    {
      if (this.loadOnEnable)
        this.Load();
      if (!this.saveOnEnable)
        return;
      this.Save();
    }

    protected virtual void OnDisable()
    {
      if (!this.saveOnDisable)
        return;
      this.Save();
    }

    protected virtual void OnApplicationQuit()
    {
      if (!this.saveOnApplicationQuit)
        return;
      this.Save();
    }

    protected virtual void OnApplicationPause()
    {
      if (!this.saveOnApplicationPause)
        return;
      this.Save();
    }

    public virtual void Save()
    {
      if (this.savePosition)
        SaveGame.Save<Vector3Save>(this.positionIdentifier, (Vector3Save) this.transform.position, this.encode, this.encodePassword, this.serializer, this.encoder, this.encoding, this.savePath);
      if (this.saveRotation)
        SaveGame.Save<QuaternionSave>(this.rotationIdentifier, (QuaternionSave) this.transform.rotation, this.encode, this.encodePassword, this.serializer, this.encoder, this.encoding, this.savePath);
      if (!this.saveScale)
        return;
      SaveGame.Save<Vector3Save>(this.scaleIdentifier, (Vector3Save) this.transform.localScale, this.encode, this.encodePassword, this.serializer, this.encoder, this.encoding, this.savePath);
    }

    public virtual void Load()
    {
      if (this.savePosition)
        this.transform.position = (Vector3) SaveGame.Load<Vector3Save>(this.positionIdentifier, (Vector3Save) this.defaultPosition, this.encode, this.encodePassword, this.serializer, this.encoder, this.encoding, this.savePath);
      if (this.saveRotation)
        this.transform.rotation = (Quaternion) SaveGame.Load<QuaternionSave>(this.rotationIdentifier, (QuaternionSave) Quaternion.Euler(this.defaultRotation), this.encode, this.encodePassword, this.serializer, this.encoder, this.encoding, this.savePath);
      if (!this.saveScale)
        return;
      this.transform.localScale = (Vector3) SaveGame.Load<Vector3Save>(this.scaleIdentifier, (Vector3Save) this.defaultScale, this.encode, this.encodePassword, this.serializer, this.encoder, this.encoding, this.savePath);
    }

    public enum SaveFormat
    {
      XML,
      JSON,
      Binary,
    }
  }
}
