using System;
using UnityEngine;

// Token: 0x0200000F RID: 15
public class CameraMove : MonoBehaviour
{
	// Token: 0x0600005F RID: 95 RVA: 0x0000478F File Offset: 0x0000298F
	private void Update()
	{
		base.transform.Translate(Vector2.right * this.speed * Time.deltaTime);
	}

	// Token: 0x040000B1 RID: 177
	public float speed;
}
