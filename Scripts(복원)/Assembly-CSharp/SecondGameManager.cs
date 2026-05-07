using System;
using UnityEngine;

// Token: 0x0200000A RID: 10
public class SecondGameManager : MonoBehaviour
{
	// Token: 0x06000043 RID: 67 RVA: 0x00004350 File Offset: 0x00002550
	private void Start()
	{
		this.isGameOver = false;
		this.phase = SecondGameManager.Phase.Fighting;
		if (this.isGameOver)
		{
			base.GetComponent<DragonController>().enabled = false;
		}
	}

	// Token: 0x06000044 RID: 68 RVA: 0x00004374 File Offset: 0x00002574
	private void Update()
	{
	}

	// Token: 0x0400007F RID: 127
	public bool isGameOver;

	// Token: 0x04000080 RID: 128
	public int firstWeaponEnemyDeadCount;

	// Token: 0x04000081 RID: 129
	public int secondWeaponEnemyDeadCount;

	// Token: 0x04000082 RID: 130
	public bool isFirstWeapon = true;

	// Token: 0x04000083 RID: 131
	public SecondGameManager.Phase phase;

	// Token: 0x0200004D RID: 77
	public enum Phase
	{
		// Token: 0x04000224 RID: 548
		Running,
		// Token: 0x04000225 RID: 549
		Fighting
	}
}
