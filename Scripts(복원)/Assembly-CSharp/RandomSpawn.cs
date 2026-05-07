using System;
using UnityEngine;

// Token: 0x02000018 RID: 24
public class RandomSpawn : MonoBehaviour
{
	// Token: 0x06000091 RID: 145 RVA: 0x00005F84 File Offset: 0x00004184
	private void Start()
	{
		this.SpawnWeapons();
	}

	// Token: 0x06000092 RID: 146 RVA: 0x00005F8C File Offset: 0x0000418C
	private void SpawnWeapons()
	{
		int num = Random.Range(0, 100);
		int num2 = Random.Range(0, 3);
		if (num >= 5 && num <= 100)
		{
			Object.Instantiate<GameObject>(this.weapons[num2], base.transform.position, base.transform.rotation);
		}
		if (num <= 4)
		{
			Object.Instantiate<GameObject>(this.goldHammer, base.transform.position, base.transform.rotation);
		}
	}

	// Token: 0x04000101 RID: 257
	public GameObject[] weapons = new GameObject[3];

	// Token: 0x04000102 RID: 258
	public GameObject goldHammer;
}
