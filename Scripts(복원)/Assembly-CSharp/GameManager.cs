using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000012 RID: 18
public class GameManager : MonoBehaviour
{
	// Token: 0x0600006C RID: 108 RVA: 0x000049FF File Offset: 0x00002BFF
	private void Awake()
	{
		if (GameManager.instance == null)
		{
			GameManager.instance = this;
		}
	}

	// Token: 0x0600006D RID: 109 RVA: 0x00004A14 File Offset: 0x00002C14
	private void Update()
	{
		this.Progress();
	}

	// Token: 0x0600006E RID: 110 RVA: 0x00004A1C File Offset: 0x00002C1C
	private void Progress()
	{
		if (this.progressBar.gameObject.activeSelf)
		{
			this.progressBar.value += Time.deltaTime;
		}
	}

	// Token: 0x040000BF RID: 191
	public static GameManager instance;

	// Token: 0x040000C0 RID: 192
	public Slider progressBar;

	// Token: 0x040000C1 RID: 193
	public bool isGameOver;

	// Token: 0x040000C2 RID: 194
	public int firstWeaponEnemyDeadCount;

	// Token: 0x040000C3 RID: 195
	public int secondWeaponEnemyDeadCount;

	// Token: 0x040000C4 RID: 196
	public bool isFirstWeapon = true;
}
