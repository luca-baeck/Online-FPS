// Decompiled with JetBrains decompiler
// Type: PlayerController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.IO;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviourPunCallbacks, IDamageable
{
  [SerializeField]
  private float mouseSensitivity;
  [SerializeField]
  private float sprintSpeed;
  [SerializeField]
  private float walkSpeed;
  [SerializeField]
  private float jumpForce;
  [SerializeField]
  private float smoothTime;
  [SerializeField]
  private GameObject cameraHolder;
  public float throwForce = 20f;
  public float GranateCooldown;
  public GameObject Camera1;
  public GameObject GranateHolder;
  public GameObject NameHolder;
  public GranateReload granateReload;
  private bool granateReloading;
  private Animator animItem;
  private int killsCheck;
  [SerializeField]
  private Item[] items;
  private int itemIndex;
  private int previousItemIndex = -1;
  private SingleShot[] singleS;
  public GameObject DamageScreen;
  private float verticalLookRotation;
  private Rigidbody rb;
  private bool grounded;
  private Vector3 smoothMoveVelocity;
  private Vector3 moveAmount;
  public float HorizontalFloat;
  public float VerticalFloat;
  public TextMeshPro PlayerUsername;
  public string lastDamager;
  public Healthbar healthUI;
  public float GranatenAnzahl = 5f;
  private const float maxHealth = 100f;
  public float currentHealth = 100f;
  private const float maxSchild = 100f;
  public float currentSchild = 100f;
  private PhotonView PV;
  private PlayerManager manager;

  private void Awake()
  {
    this.killsCheck = OverviewEnable.kills;
    this.rb = this.GetComponent<Rigidbody>();
    this.PV = this.GetComponent<PhotonView>();
    this.singleS = Object.FindObjectsOfType<SingleShot>();
    this.granateReload = GameObject.Find("GranatenReload").GetComponent<GranateReload>();
    this.granateReloading = false;
    this.animItem = GameObject.Find("ItemHolder").GetComponent<Animator>();
    this.manager = PhotonView.Find((int) this.PV.InstantiationData[0]).GetComponent<PlayerManager>();
  }

  private void Start()
  {
    if (this.PV.IsMine)
    {
      this.GetComponent<MeshRenderer>().enabled = false;
      this.DamageScreen = GameObject.Find("Damaged");
      this.healthUI = GameObject.Find("Lebensanzeige").GetComponent<Healthbar>();
      this.DamageScreen.SetActive(false);
      this.EquipItem(0);
      this.PV.RPC("SyncUsername", RpcTarget.All, (object) PlayFab_Log.Username);
      this.PV.RPC("SyncSkin", RpcTarget.All, (object) SpindManager.Skin);
      this.granateReload.ReloadAnim(0.0f, this.GranatenAnzahl);
    }
    else
    {
      if (this.PV.IsMine)
        return;
      Object.Destroy((Object) this.GetComponentInChildren<Camera>().gameObject);
      Object.Destroy((Object) this.rb);
    }
  }

  [PunRPC]
  private void SyncSkin(string _skin)
  {
    if (_skin == null)
      return;
    Material material = UnityEngine.Resources.Load("SkinMaterials/" + _skin, typeof (Material)) as Material;
    this.gameObject.GetComponent<MeshRenderer>().material = material;
  }

  [PunRPC]
  private void SyncUsername(string username)
  {
    if (!this.PV.IsMine)
    {
      this.PlayerUsername.text = username;
    }
    else
    {
      if (!this.PV.IsMine)
        return;
      this.PlayerUsername.text = "";
    }
  }

  private void Update()
  {
    this.NameHolder.transform.LookAt(Camera.main.transform);
    if (!this.PV.IsMine)
      return;
    if (this.killsCheck != OverviewEnable.kills)
    {
      this.killsCheck = OverviewEnable.kills;
      this.currentHealth = 100f;
      if ((double) this.currentSchild < 50.0)
        this.currentSchild = 50f;
    }
    this.Look();
    this.Move();
    this.Jump();
    this.Sneak();
    this.Aim();
    if (!this.granateReloading && (double) this.GranatenAnzahl > 0.0)
      this.throwGranate();
    this.healthUI.SetHealth(this.currentSchild, this.currentHealth);
    if (Input.GetKeyDown(GetBinds.AR))
    {
      MonoBehaviour.print((object) "Waffe AR");
      this.EquipItem(2);
    }
    if (Input.GetKeyDown(GetBinds.Pump))
    {
      MonoBehaviour.print((object) "Waffe Pump");
      this.EquipItem(0);
    }
    if (Input.GetKeyDown(GetBinds.MP))
    {
      MonoBehaviour.print((object) "Waffe MP");
      this.EquipItem(1);
    }
    if ((double) Input.GetAxisRaw("Mouse ScrollWheel") > 0.0)
    {
      if (this.itemIndex >= this.items.Length - 1)
        this.EquipItem(0);
      else
        this.EquipItem(this.itemIndex + 1);
    }
    else if ((double) Input.GetAxisRaw("Mouse ScrollWheel") < 0.0)
    {
      if (this.itemIndex <= 0)
        this.EquipItem(this.items.Length - 1);
      else
        this.EquipItem(this.itemIndex - 1);
    }
    this.items[this.itemIndex].Use();
    if ((double) this.transform.position.y >= -30.0)
      return;
    this.Die("von dir selbst");
  }

  private void FixedUpdate()
  {
    if (!this.PV.IsMine)
      return;
    this.rb.MovePosition(this.rb.position + this.transform.TransformDirection(this.moveAmount) * Time.fixedDeltaTime);
  }

  private void throwGranate()
  {
    if (!Input.GetKeyDown(GetBinds.Granate))
      return;
    --this.GranatenAnzahl;
    this.granateReloading = true;
    this.Invoke("CoolDownGranate", this.GranateCooldown);
    PhotonNetwork.Instantiate(Path.Combine("Photon", "Granate"), this.GranateHolder.transform.position, this.Camera1.transform.rotation).GetComponent<Rigidbody>().AddForce(this.Camera1.transform.forward * this.throwForce, ForceMode.VelocityChange);
    if ((double) this.GranatenAnzahl > 0.0)
      this.granateReload.ReloadAnim(this.GranateCooldown, this.GranatenAnzahl);
    else
      this.granateReload.ReloadAnim(0.0f, this.GranatenAnzahl);
  }

  private void CoolDownGranate() => this.granateReloading = false;

  private void Aim()
  {
    if (!Input.GetKey(GetBinds.Zielen))
      return;
    this.walkSpeed *= 0.75f;
    this.sprintSpeed = this.walkSpeed;
  }

  private void Jump()
  {
    if (!Input.GetKeyDown(GetBinds.springen) || !this.grounded)
      return;
    this.rb.AddForce(this.transform.up * this.jumpForce);
  }

  private void Sneak()
  {
    if (Input.GetKey(GetBinds.ducken))
    {
      this.transform.localScale = new Vector3(0.8f, 0.7f, 0.8f);
      this.sprintSpeed = 5f;
      this.walkSpeed = 3f;
    }
    else
    {
      this.transform.localScale = new Vector3(0.8f, 1f, 0.8f);
      this.sprintSpeed = 8f;
      this.walkSpeed = 4f;
    }
  }

  private void Move()
  {
    if (Input.GetKey(GetBinds.links) && !Input.GetKey(GetBinds.rechts))
      this.HorizontalFloat = -1f;
    if (!Input.GetKey(GetBinds.links) && Input.GetKey(GetBinds.rechts))
      this.HorizontalFloat = 1f;
    if (Input.GetKey(GetBinds.links) && Input.GetKey(GetBinds.rechts) || !Input.GetKey(GetBinds.links) && !Input.GetKey(GetBinds.rechts))
      this.HorizontalFloat = 0.0f;
    if (Input.GetKey(GetBinds.Rückwärts) && !Input.GetKey(GetBinds.Vorwärts))
      this.VerticalFloat = -1f;
    if (!Input.GetKey(GetBinds.Rückwärts) && Input.GetKey(GetBinds.Vorwärts))
      this.VerticalFloat = 1f;
    if (Input.GetKey(GetBinds.Rückwärts) && Input.GetKey(GetBinds.Vorwärts) || !Input.GetKey(GetBinds.Rückwärts) && !Input.GetKey(GetBinds.Vorwärts))
      this.VerticalFloat = 0.0f;
    this.moveAmount = Vector3.SmoothDamp(this.moveAmount, new Vector3(this.HorizontalFloat, 0.0f, this.VerticalFloat).normalized * (Input.GetKey(GetBinds.sprinten) ? this.sprintSpeed : this.walkSpeed), ref this.smoothMoveVelocity, this.smoothTime);
    if (!((Object) this.animItem != (Object) null))
      return;
    if (Input.GetKey(GetBinds.sprinten) && ((double) this.HorizontalFloat != 0.0 || (double) this.VerticalFloat != 0.0))
    {
      MonoBehaviour.print((object) "sprinten");
      this.animItem.SetFloat("Idle_Speed", this.sprintSpeed * 0.05f);
    }
    if (!Input.GetKey(GetBinds.sprinten) && ((double) this.HorizontalFloat != 0.0 || (double) this.VerticalFloat != 0.0))
    {
      MonoBehaviour.print((object) "gehen");
      this.animItem.SetFloat("Idle_Speed", this.walkSpeed * 0.05f);
    }
    if (Input.GetKey(GetBinds.sprinten) && !Input.GetKey(GetBinds.sprinten) || (double) this.HorizontalFloat != 0.0 || (double) this.VerticalFloat != 0.0)
      return;
    MonoBehaviour.print((object) "stehen");
    this.animItem.SetFloat("Idle_Speed", 0.1f);
  }

  private void EquipItem(int _index)
  {
    bool flag = true;
    if (this.PV.IsMine)
    {
      foreach (SingleShot singleShot in this.singleS)
      {
        if (singleShot.reloading)
          flag = false;
      }
    }
    if (!flag || _index == this.previousItemIndex)
      return;
    this.itemIndex = _index;
    this.items[this.itemIndex].itemGameObject.SetActive(true);
    if (this.previousItemIndex != -1)
      this.items[this.previousItemIndex].itemGameObject.SetActive(false);
    this.previousItemIndex = this.itemIndex;
    if (!this.PV.IsMine)
      return;
    Hashtable propertiesToSet = new Hashtable();
    propertiesToSet.Add((object) "itemIndex", (object) this.itemIndex);
    PhotonNetwork.LocalPlayer.SetCustomProperties(propertiesToSet);
    MonoBehaviour.print((object) ("spawned index " + (object) this.itemIndex));
  }

  private void Look()
  {
    this.transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * this.mouseSensitivity * GetBinds.MouseX);
    this.verticalLookRotation += Input.GetAxisRaw("Mouse Y") * this.mouseSensitivity * GetBinds.MouseY;
    this.verticalLookRotation = Mathf.Clamp(this.verticalLookRotation, -85f, 90f);
    this.cameraHolder.transform.localEulerAngles = Vector3.left * this.verticalLookRotation;
  }

  public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
  {
    if (this.PV.IsMine || targetPlayer != this.PV.Owner)
      return;
    this.EquipItem((int) changedProps[(object) "itemIndex"]);
  }

  public void SetGroundedState(bool _grounded) => this.grounded = _grounded;

  public void TakeDamage(float damage) => this.PV.RPC("RPC_TakeDamage", RpcTarget.All, (object) damage, (object) PlayFab_Log.Username);

  [PunRPC]
  private void RPC_TakeDamage(float damage, string name)
  {
    if (!this.PV.IsMine)
      return;
    MonoBehaviour.print((object) ("took " + (object) damage + " damage"));
    this.DamageScreen.SetActive(true);
    this.Invoke("DamageScreenF", 0.1f);
    if ((double) damage >= (double) this.currentSchild)
    {
      damage -= this.currentSchild;
      this.currentSchild = 0.0f;
      this.currentHealth -= damage;
    }
    else if ((double) damage < (double) this.currentSchild)
      this.currentSchild -= damage;
    this.lastDamager = name;
    if ((double) this.currentHealth > 0.0)
      return;
    this.Die(this.lastDamager);
  }

  private void DamageScreenF() => this.DamageScreen.SetActive(false);

  private void Die(string name)
  {
    this.DamageScreen.SetActive(true);
    this.manager.Die(name);
  }
}
