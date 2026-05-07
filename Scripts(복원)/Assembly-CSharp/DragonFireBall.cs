using System;
using UnityEngine;

// Token: 0x02000003 RID: 3
public class DragonFireBall : MonoBehaviour
{
	// Token: 0x06000020 RID: 32 RVA: 0x00003514 File Offset: 0x00001714
	private void Start()
	{
		base.transform.localScale = new Vector3(-4f, 4f, 4f);
		Object.Destroy(base.gameObject, 3f);
	}

	// Token: 0x06000021 RID: 33 RVA: 0x00003545 File Offset: 0x00001745
	private void Update()
	{
		if (base.GetComponentInParent<DragonController>().isHardMode)
		{
			this.fireBallDamage = 15;
		}
		base.transform.Translate(Vector2.right * 5f * Time.deltaTime);
	}

	// Token: 0x06000022 RID: 34 RVA: 0x00003588 File Offset: 0x00001788
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			GameObject.Find("Player").GetComponent<Player>().PlayerTakeDamage((float)this.fireBallDamage);
			if (base.GetComponentInParent<DragonController>().isHardMode)
			{
				collision.GetComponent<Player>().PlayerGetFire();
			}
			Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04000053 RID: 83
	public int fireBallDamage = 10;
}
