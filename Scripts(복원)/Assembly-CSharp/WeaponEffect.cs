using System;
using UnityEngine;

// Token: 0x0200001B RID: 27
public class WeaponEffect : MonoBehaviour
{
	// Token: 0x060000A8 RID: 168 RVA: 0x0000668D File Offset: 0x0000488D
	private void Start()
	{
		this.weaponCtrl = Object.FindObjectOfType<WeaponCtrl>();
		Object.Destroy(base.gameObject, 0.8f);
	}

	// Token: 0x060000A9 RID: 169 RVA: 0x000066AC File Offset: 0x000048AC
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Enemy") && base.gameObject != this.weaponCtrl.GetDontDestroyEffectInstance())
		{
			Object.Destroy(base.gameObject);
		}
		if (other.gameObject.CompareTag("Boss"))
		{
			other.GetComponent<DragonController>().BossTakeDamage(this.damage);
			Object.Destroy(base.gameObject, 0.2f);
		}
	}

	// Token: 0x0400011E RID: 286
	public int damage;

	// Token: 0x0400011F RID: 287
	private WeaponCtrl weaponCtrl;
}
