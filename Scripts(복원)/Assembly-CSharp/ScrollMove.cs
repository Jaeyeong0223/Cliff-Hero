using System;
using UnityEngine;

// Token: 0x02000009 RID: 9
public class ScrollMove : MonoBehaviour
{
	// Token: 0x06000041 RID: 65 RVA: 0x0000430D File Offset: 0x0000250D
	private void Update()
	{
		this.targetOffset += Time.deltaTime * this.scrollSpeed;
		base.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(this.targetOffset, 0f);
	}

	// Token: 0x0400007D RID: 125
	public float scrollSpeed;

	// Token: 0x0400007E RID: 126
	private float targetOffset;
}
