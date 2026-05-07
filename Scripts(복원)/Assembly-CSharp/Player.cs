using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x02000016 RID: 22
public class Player : MonoBehaviour
{
	// Token: 0x06000076 RID: 118 RVA: 0x00004ACC File Offset: 0x00002CCC
	private void Start()
	{
		this.rb = base.GetComponent<Rigidbody2D>();
		this.anim = this.standing.GetComponent<Animator>();
		this.bowanim = this.bowRun.GetComponent<Animator>();
		this.staffanim = this.staffRun.GetComponent<Animator>();
		this.magicSwordanim = this.magicSwordRun.GetComponent<Animator>();
		this.magicBowanim = this.magicBowRun.GetComponent<Animator>();
		this.bigBowanim = this.bigBowRun.GetComponent<Animator>();
		this.L_Swordanim = this.L_SwordRun.GetComponent<Animator>();
		this.L_Bowanim = this.L_BowRun.GetComponent<Animator>();
		this.L_Staffanim = this.L_StaffRun.GetComponent<Animator>();
		this.playerInventory = base.GetComponent<PlayerInventory>();
		this.currentHp = this.maxHp;
	}

	// Token: 0x06000077 RID: 119 RVA: 0x00004B96 File Offset: 0x00002D96
	private void Update()
	{
		this.Move();
		this.Jump();
		this.Slide();
		this.Attack();
		this.HpSlider();
		this.HpController();
		this.DeadScene();
	}

	// Token: 0x06000078 RID: 120 RVA: 0x00004BC2 File Offset: 0x00002DC2
	private void DeadScene()
	{
		if (this.currentHp <= 0f)
		{
			SceneManager.LoadScene("DeadScene");
		}
	}

	// Token: 0x06000079 RID: 121 RVA: 0x00004BDB File Offset: 0x00002DDB
	private void HpSlider()
	{
		this.hpSlider.value = Mathf.Lerp(this.hpSlider.value, this.currentHp / this.maxHp, Time.deltaTime * 3f);
	}

	// Token: 0x0600007A RID: 122 RVA: 0x00004C10 File Offset: 0x00002E10
	private void Move()
	{
		base.transform.Translate(Vector2.right * this.moveSpeed * Time.deltaTime);
		if (base.transform.position.x != this.xTransform.position.x)
		{
			base.transform.position = new Vector3(this.xTransform.position.x, base.transform.position.y, base.transform.position.z);
		}
	}

	// Token: 0x0600007B RID: 123 RVA: 0x00004CAC File Offset: 0x00002EAC
	private void Jump()
	{
		if (this.isSliding)
		{
			return;
		}
		for (int i = 0; i < this.weapons.Length; i++)
		{
			if (this.weapons[i].activeSelf && Input.GetKeyDown(KeyCode.W) && this.jumpCnt < 2)
			{
				this.rb.AddForce(Vector2.up * this.jumpForce, ForceMode2D.Impulse);
				this.jumpCnt++;
				this.GetAnimatorByIndex(i).SetBool("isJump", true);
				this.GetAnimatorByIndex(i).SetInteger("JumpCnt", this.jumpCnt);
			}
		}
	}

	// Token: 0x0600007C RID: 124 RVA: 0x00004D4C File Offset: 0x00002F4C
	private Animator GetAnimatorByIndex(int index)
	{
		switch (index)
		{
		case 0:
			return this.anim;
		case 1:
			return this.bowanim;
		case 2:
			return this.staffanim;
		case 3:
			return this.magicSwordanim;
		case 4:
			return this.magicBowanim;
		case 5:
			return this.bigBowanim;
		case 6:
			return this.L_Swordanim;
		case 7:
			return this.L_Bowanim;
		case 8:
			return this.L_Staffanim;
		default:
			return this.anim;
		}
	}

	// Token: 0x0600007D RID: 125 RVA: 0x00004DCC File Offset: 0x00002FCC
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Enemy"))
		{
			this.currentHp -= 10f;
		}
		if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Ground2"))
		{
			this.jumpCnt = 0;
			for (int i = 0; i < 9; i++)
			{
				this.GetAnimatorByIndex(i).SetInteger("JumpCnt", this.jumpCnt);
				this.GetAnimatorByIndex(i).SetBool("isJump", false);
			}
		}
	}

	// Token: 0x0600007E RID: 126 RVA: 0x00004E60 File Offset: 0x00003060
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("DeadLine"))
		{
			SceneManager.LoadScene("DeadScene");
		}
		if (collision.gameObject.CompareTag("Ground2"))
		{
			this.SetLayerAll(3);
		}
		if (collision.gameObject.CompareTag("Object"))
		{
			this.currentHp -= 5f;
		}
		if (collision.gameObject.CompareTag("FireBall"))
		{
			this.PlayerTakeDamage(15f);
			this.PlayerGetFire();
			Object.Destroy(collision.gameObject);
		}
		if (collision.gameObject.CompareTag("Potion"))
		{
			this.currentHp += 150f;
			if (this.currentHp >= this.maxHp)
			{
				this.currentHp = this.maxHp;
			}
			Object.Destroy(collision.gameObject);
		}
	}

	// Token: 0x0600007F RID: 127 RVA: 0x00004F3D File Offset: 0x0000313D
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Ground2"))
		{
			this.SetLayerAll(0);
		}
	}

	// Token: 0x06000080 RID: 128 RVA: 0x00004F58 File Offset: 0x00003158
	private void SetLayerAll(int layer)
	{
		this.standing.layer = layer;
		this.bowRun.layer = layer;
		this.staffRun.layer = layer;
		this.magicSwordRun.layer = layer;
		this.magicBowRun.layer = layer;
		this.bigBowRun.layer = layer;
		this.L_SwordRun.layer = layer;
		this.L_BowRun.layer = layer;
		this.L_StaffRun.layer = layer;
	}

	// Token: 0x06000081 RID: 129 RVA: 0x00004FD4 File Offset: 0x000031D4
	private void Slide()
	{
		for (int i = 0; i < this.weapons.Length; i++)
		{
			GameObject runObject = this.GetRunObject(i);
			GameObject slideObject = this.GetSlideObject(i);
			if (this.weapons[i].activeSelf)
			{
				if (Input.GetKey(KeyCode.S))
				{
					this.isSliding = true;
					runObject.SetActive(false);
					slideObject.SetActive(true);
				}
				else
				{
					this.isSliding = false;
					runObject.SetActive(true);
					slideObject.SetActive(false);
				}
			}
		}
	}

	// Token: 0x06000082 RID: 130 RVA: 0x00005048 File Offset: 0x00003248
	private GameObject GetRunObject(int index)
	{
		switch (index)
		{
		case 0:
			return this.standing;
		case 1:
			return this.bowRun;
		case 2:
			return this.staffRun;
		case 3:
			return this.magicSwordRun;
		case 4:
			return this.magicBowRun;
		case 5:
			return this.bigBowRun;
		case 6:
			return this.L_SwordRun;
		case 7:
			return this.L_BowRun;
		case 8:
			return this.L_StaffRun;
		default:
			return this.standing;
		}
	}

	// Token: 0x06000083 RID: 131 RVA: 0x000050C8 File Offset: 0x000032C8
	private GameObject GetSlideObject(int index)
	{
		switch (index)
		{
		case 0:
			return this.sliding;
		case 1:
			return this.bowSliding;
		case 2:
			return this.staffSliding;
		case 3:
			return this.magicSwordSliding;
		case 4:
			return this.magicBowSliding;
		case 5:
			return this.bigBowSliding;
		case 6:
			return this.L_SwordSliding;
		case 7:
			return this.L_BowSliding;
		case 8:
			return this.L_StaffSliding;
		default:
			return this.sliding;
		}
	}

	// Token: 0x06000084 RID: 132 RVA: 0x00005146 File Offset: 0x00003346
	public virtual void PlayerTakeDamage(float damage)
	{
		this.currentHp -= damage;
	}

	// Token: 0x06000085 RID: 133 RVA: 0x00005156 File Offset: 0x00003356
	public virtual void PlayerGetFire()
	{
		this.PlayerTakeDamage(2f);
		this.fireDamageCount--;
		base.StartCoroutine(this.FireDamageTimer(this.fireDamageCount));
	}

	// Token: 0x06000086 RID: 134 RVA: 0x00005184 File Offset: 0x00003384
	private IEnumerator FireDamageTimer(int _damageCount)
	{
		yield return new WaitForSeconds(1f);
		if (_damageCount > 0)
		{
			this.PlayerGetFire();
		}
		else
		{
			this.fireDamageCount = 5;
		}
		yield break;
	}

	// Token: 0x06000087 RID: 135 RVA: 0x0000519C File Offset: 0x0000339C
	private void Attack()
	{
		for (int i = 0; i < this.weapons.Length; i++)
		{
			if (this.weapons[i].activeSelf && !this.isSliding)
			{
				this.SetActiveWeaponObjects(i);
				Animator animatorByIndex = this.GetAnimatorByIndex(i);
				if (Input.GetButtonDown("Fire1") && !base.GetComponentInChildren<WeaponCtrl>().isAttack)
				{
					base.GetComponentInChildren<WeaponCtrl>().Attack();
					animatorByIndex.SetTrigger("Attack");
				}
			}
		}
	}

	// Token: 0x06000088 RID: 136 RVA: 0x00005214 File Offset: 0x00003414
	private void SetActiveWeaponObjects(int activeIndex)
	{
		GameObject[] array = new GameObject[]
		{
			this.standing,
			this.bowRun,
			this.staffRun,
			this.magicSwordRun,
			this.magicBowRun,
			this.bigBowRun,
			this.L_SwordRun,
			this.L_BowRun,
			this.L_StaffRun
		};
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(i == activeIndex);
		}
	}

	// Token: 0x06000089 RID: 137 RVA: 0x00005294 File Offset: 0x00003494
	private void HpController()
	{
		if (this.currentHp <= 0f)
		{
			this.currentHp = 0f;
			SceneManager.LoadScene("DeadScene");
		}
	}

	// Token: 0x040000CB RID: 203
	private PlayerInventory playerInventory;

	// Token: 0x040000CC RID: 204
	private Animator anim;

	// Token: 0x040000CD RID: 205
	private Animator bowanim;

	// Token: 0x040000CE RID: 206
	private Animator staffanim;

	// Token: 0x040000CF RID: 207
	private Animator magicSwordanim;

	// Token: 0x040000D0 RID: 208
	private Animator magicBowanim;

	// Token: 0x040000D1 RID: 209
	private Animator bigBowanim;

	// Token: 0x040000D2 RID: 210
	private Animator L_Swordanim;

	// Token: 0x040000D3 RID: 211
	private Animator L_Bowanim;

	// Token: 0x040000D4 RID: 212
	private Animator L_Staffanim;

	// Token: 0x040000D5 RID: 213
	private Rigidbody2D rb;

	// Token: 0x040000D6 RID: 214
	[SerializeField]
	private Transform xTransform;

	// Token: 0x040000D7 RID: 215
	public GameObject standing;

	// Token: 0x040000D8 RID: 216
	public GameObject sliding;

	// Token: 0x040000D9 RID: 217
	public GameObject bowRun;

	// Token: 0x040000DA RID: 218
	public GameObject bowSliding;

	// Token: 0x040000DB RID: 219
	public GameObject staffRun;

	// Token: 0x040000DC RID: 220
	public GameObject staffSliding;

	// Token: 0x040000DD RID: 221
	public GameObject magicSwordRun;

	// Token: 0x040000DE RID: 222
	public GameObject magicSwordSliding;

	// Token: 0x040000DF RID: 223
	public GameObject magicBowRun;

	// Token: 0x040000E0 RID: 224
	public GameObject magicBowSliding;

	// Token: 0x040000E1 RID: 225
	public GameObject bigBowRun;

	// Token: 0x040000E2 RID: 226
	public GameObject bigBowSliding;

	// Token: 0x040000E3 RID: 227
	public GameObject L_SwordRun;

	// Token: 0x040000E4 RID: 228
	public GameObject L_SwordSliding;

	// Token: 0x040000E5 RID: 229
	public GameObject L_BowRun;

	// Token: 0x040000E6 RID: 230
	public GameObject L_BowSliding;

	// Token: 0x040000E7 RID: 231
	public GameObject L_StaffRun;

	// Token: 0x040000E8 RID: 232
	public GameObject L_StaffSliding;

	// Token: 0x040000E9 RID: 233
	public GameObject[] weapons = new GameObject[9];

	// Token: 0x040000EA RID: 234
	public Slider hpSlider;

	// Token: 0x040000EB RID: 235
	public float moveSpeed = 10f;

	// Token: 0x040000EC RID: 236
	public float jumpForce = 5f;

	// Token: 0x040000ED RID: 237
	private int jumpCnt;

	// Token: 0x040000EE RID: 238
	private float currentHp;

	// Token: 0x040000EF RID: 239
	private float maxHp = 300f;

	// Token: 0x040000F0 RID: 240
	public bool isSliding;

	// Token: 0x040000F1 RID: 241
	private int fireDamageCount = 5;
}
