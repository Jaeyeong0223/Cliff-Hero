using System;
using UnityEngine;

// Token: 0x0200000C RID: 12
public class WeaponSuk : MonoBehaviour
{
	// Token: 0x06000058 RID: 88 RVA: 0x000045E0 File Offset: 0x000027E0
	private void Update()
	{
		if (GameManager.instance.isFirstWeapon && GameManager.instance.firstWeaponEnemyDeadCount >= this.firstSukUpCount && this.firstSukUpCount <= 5)
		{
			GameManager.instance.firstWeaponEnemyDeadCount = 0;
			this.firstWeaponSuk++;
			this.firstSukUpCount++;
			base.GetComponent<PlayerInventory>().suk = this.firstWeaponSuk;
		}
		if (!GameManager.instance.isFirstWeapon && GameManager.instance.secondWeaponEnemyDeadCount >= this.secondSukUpCount && this.secondSukUpCount <= 5)
		{
			GameManager.instance.secondWeaponEnemyDeadCount = 0;
			this.secondWeaponSuk++;
			this.secondSukUpCount++;
			base.GetComponent<PlayerInventory>().suk = this.secondWeaponSuk;
		}
	}

	// Token: 0x040000A8 RID: 168
	public int firstWeaponSuk;

	// Token: 0x040000A9 RID: 169
	public int secondWeaponSuk;

	// Token: 0x040000AA RID: 170
	private int firstSukUpCount = 1;

	// Token: 0x040000AB RID: 171
	private int secondSukUpCount = 1;
}
