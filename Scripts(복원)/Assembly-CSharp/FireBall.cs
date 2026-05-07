using System;
using UnityEngine;

// Token: 0x02000011 RID: 17
public class FireBall : MonoBehaviour
{
	// Token: 0x0600006A RID: 106 RVA: 0x000049C5 File Offset: 0x00002BC5
	private void Update()
	{
		base.transform.Translate(Vector3.left * this.skillSpeed * Time.deltaTime);
	}

	// Token: 0x040000BE RID: 190
	public float skillSpeed = 5f;
}
