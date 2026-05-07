using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200001A RID: 26
public class WeaponCtrl : MonoBehaviour
{
	// Token: 0x0600009D RID: 157 RVA: 0x00006298 File Offset: 0x00004498
	private void Start()
	{
		this.playerInventory = base.GetComponent<PlayerInventory>();
		if (this.playerInventory == null)
		{
			Debug.LogWarning("PlayerInventory 스크립트가 없습니다!");
		}
		if (this.cooldownImage != null)
		{
			this.cooldownImage.fillAmount = 0f;
		}
	}

	// Token: 0x0600009E RID: 158 RVA: 0x000062E8 File Offset: 0x000044E8
	private void Update()
	{
		this.Skill();
		this.BowSkill();
		this.StaffSkill();
		if (this.coolDown && this.cooldownImage != null)
		{
			this.cooldownImage.fillAmount -= Time.deltaTime / this.coolTime;
			if (this.cooldownImage.fillAmount <= 0f)
			{
				this.cooldownImage.fillAmount = 0f;
				this.coolDown = false;
			}
		}
	}

	// Token: 0x0600009F RID: 159 RVA: 0x00006364 File Offset: 0x00004564
	public void Attack()
	{
		if (!this.isAttack)
		{
			this.isAttack = true;
			if (this.dontDestroyEffectPrefab == null && this.weaponEffect != null)
			{
				GameObject gameObject = Object.Instantiate<GameObject>(this.weaponEffect, base.transform.position, base.transform.rotation);
				int num = (this.playerInventory != null) ? this.playerInventory.suk : 1;
				num = Mathf.Clamp(num, 1, 5);
				float d = Mathf.Pow(2f, (float)(num - 1));
				gameObject.transform.localScale *= d;
				Rigidbody2D component = gameObject.GetComponent<Rigidbody2D>();
				if (component != null)
				{
					component.velocity = Vector2.right * 20f;
				}
				WeaponEffect component2 = gameObject.GetComponent<WeaponEffect>();
				if (component2 != null)
				{
					component2.damage = this.damage;
				}
			}
			else if (this.weaponEffect == null && this.dontDestroyEffectPrefab != null)
			{
				GameObject gameObject = Object.Instantiate<GameObject>(this.dontDestroyEffectPrefab, base.transform.position, base.transform.rotation);
				Rigidbody2D component3 = gameObject.GetComponent<Rigidbody2D>();
				if (component3 != null)
				{
					component3.velocity = Vector2.right * 7f;
				}
				this.dontDestroyEffectInstance = gameObject;
			}
			base.StartCoroutine(this.DelayAttack());
		}
	}

	// Token: 0x060000A0 RID: 160 RVA: 0x000064D3 File Offset: 0x000046D3
	private IEnumerator DelayAttack()
	{
		yield return new WaitForSeconds(this.attackDelay);
		this.isAttack = false;
		yield break;
	}

	// Token: 0x060000A1 RID: 161 RVA: 0x000064E4 File Offset: 0x000046E4
	private void Skill()
	{
		if (Input.GetKeyDown(KeyCode.R) && !this.coolDown && this.skillEffect != null)
		{
			this.coolDown = true;
			this.isAttack = true;
			this.isSkillAttack = true;
			this.skillEffect.SetActive(true);
			if (this.cooldownImage != null)
			{
				this.cooldownImage.fillAmount = 1f;
			}
			base.StartCoroutine(this.SkillTime());
		}
	}

	// Token: 0x060000A2 RID: 162 RVA: 0x0000655C File Offset: 0x0000475C
	private IEnumerator SkillTime()
	{
		yield return new WaitForSeconds(this.skillTime);
		this.skillEffect.SetActive(false);
		this.isAttack = false;
		yield break;
	}

	// Token: 0x060000A3 RID: 163 RVA: 0x0000656C File Offset: 0x0000476C
	private void BowSkill()
	{
		if (Input.GetKeyDown(KeyCode.R) && this.skillEffect == null && !this.coolDown)
		{
			this.isBowSkill = true;
			this.coolDown = true;
			this.attackDelay = 0.3f;
			base.StartCoroutine(this.BowSkillTime());
		}
	}

	// Token: 0x060000A4 RID: 164 RVA: 0x000065BE File Offset: 0x000047BE
	private IEnumerator BowSkillTime()
	{
		yield return new WaitForSeconds(this.skillTime);
		this.isBowSkill = false;
		this.isAttack = false;
		this.attackDelay = 1f;
		yield break;
	}

	// Token: 0x060000A5 RID: 165 RVA: 0x000065D0 File Offset: 0x000047D0
	private void StaffSkill()
	{
		if ((GameObject.Find("staff") || GameObject.Find("bigBow")) && Input.GetKeyDown(KeyCode.R))
		{
			this.isFlyAttack = true;
			Rigidbody2D component = Object.Instantiate<GameObject>(this.skillEffect, base.transform.position, base.transform.rotation).GetComponent<Rigidbody2D>();
			if (component != null)
			{
				component.velocity = Vector2.right * 20f;
			}
		}
	}

	// Token: 0x060000A6 RID: 166 RVA: 0x00006654 File Offset: 0x00004854
	public GameObject GetDontDestroyEffectInstance()
	{
		return this.dontDestroyEffectInstance;
	}

	// Token: 0x0400010F RID: 271
	[Header("Weapon Settings")]
	public GameObject weaponEffect;

	// Token: 0x04000110 RID: 272
	public GameObject dontDestroyEffectPrefab;

	// Token: 0x04000111 RID: 273
	public GameObject skillEffect;

	// Token: 0x04000112 RID: 274
	public float attackDelay = 1f;

	// Token: 0x04000113 RID: 275
	public float skillTime = 2f;

	// Token: 0x04000114 RID: 276
	public float coolTime = 4f;

	// Token: 0x04000115 RID: 277
	public int damage = 10;

	// Token: 0x04000116 RID: 278
	public bool isAttack;

	// Token: 0x04000117 RID: 279
	public bool isSkillAttack;

	// Token: 0x04000118 RID: 280
	public bool isFlyAttack;

	// Token: 0x04000119 RID: 281
	[Header("Cooldown UI")]
	public Image cooldownImage;

	// Token: 0x0400011A RID: 282
	public bool coolDown;

	// Token: 0x0400011B RID: 283
	private GameObject dontDestroyEffectInstance;

	// Token: 0x0400011C RID: 284
	private bool isBowSkill;

	// Token: 0x0400011D RID: 285
	private PlayerInventory playerInventory;
}
