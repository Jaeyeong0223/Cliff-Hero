using System;
using UnityEngine;

// Token: 0x02000006 RID: 6
public class FireBreath : MonoBehaviour
{
	// Token: 0x06000030 RID: 48 RVA: 0x00003FBD File Offset: 0x000021BD
	private void Start()
	{
		this.bc = base.GetComponent<BoxCollider2D>();
		this.anim = base.GetComponent<Animator>();
		this.bc.enabled = false;
	}

	// Token: 0x06000031 RID: 49 RVA: 0x00003FE4 File Offset: 0x000021E4
	private void Update()
	{
		if (base.gameObject.CompareTag("LowBreath"))
		{
			this.bc = base.GetComponent<BoxCollider2D>();
			this.anim = base.GetComponent<Animator>();
		}
		else if (base.gameObject.CompareTag("HighBreath"))
		{
			this.bc = base.GetComponent<BoxCollider2D>();
			this.anim = base.GetComponent<Animator>();
		}
		if (base.GetComponentInParent<DragonController>().isHardMode)
		{
			this.fireBreathDamage = 35f;
			return;
		}
		this.fireBreathDamage = 20f;
	}

	// Token: 0x06000032 RID: 50 RVA: 0x0000406B File Offset: 0x0000226B
	private void FireAttackOn()
	{
		this.bc.enabled = true;
	}

	// Token: 0x06000033 RID: 51 RVA: 0x00004079 File Offset: 0x00002279
	private void FireAttackOff()
	{
		this.bc.enabled = false;
	}

	// Token: 0x06000034 RID: 52 RVA: 0x00004087 File Offset: 0x00002287
	private void FireAttackStart()
	{
		this.anim.SetBool("isFiring", true);
	}

	// Token: 0x06000035 RID: 53 RVA: 0x0000409C File Offset: 0x0000229C
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision != null && collision.gameObject.CompareTag("Player") && !this.isFired)
		{
			this.isFired = true;
			GameObject.Find("Player").GetComponent<Player>().PlayerTakeDamage(this.fireBreathDamage);
			GameObject.Find("Player").GetComponent<Player>().PlayerGetFire();
		}
	}

	// Token: 0x0400006B RID: 107
	public float fireBreathDamage = 20f;

	// Token: 0x0400006C RID: 108
	public bool isFired;

	// Token: 0x0400006D RID: 109
	public bool isFiring;

	// Token: 0x0400006E RID: 110
	public Animator anim;

	// Token: 0x0400006F RID: 111
	public BoxCollider2D bc;
}
