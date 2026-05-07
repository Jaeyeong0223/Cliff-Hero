using System;
using UnityEngine;

// Token: 0x02000014 RID: 20
public class ItemAnim : MonoBehaviour
{
	// Token: 0x06000071 RID: 113 RVA: 0x00004A5E File Offset: 0x00002C5E
	private void Start()
	{
		this.rb = base.GetComponent<Rigidbody2D>();
		this.rb.AddForce(Vector2.up * 10f * Time.deltaTime, ForceMode2D.Impulse);
	}

	// Token: 0x040000C7 RID: 199
	private Rigidbody2D rb;
}
