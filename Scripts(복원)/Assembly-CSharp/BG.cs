using System;
using UnityEngine;

// Token: 0x0200000E RID: 14
public class BG : MonoBehaviour
{
	// Token: 0x0600005D RID: 93 RVA: 0x00004704 File Offset: 0x00002904
	private void Update()
	{
		base.transform.position += this.moveDirection * this.moveSpeed * Time.deltaTime;
		if (base.transform.position.x <= -this.scrollAmount)
		{
			base.transform.position = this.target.position - this.moveDirection * this.scrollAmount;
		}
	}

	// Token: 0x040000AD RID: 173
	[SerializeField]
	private Transform target;

	// Token: 0x040000AE RID: 174
	[SerializeField]
	private float scrollAmount;

	// Token: 0x040000AF RID: 175
	[SerializeField]
	private float moveSpeed;

	// Token: 0x040000B0 RID: 176
	[SerializeField]
	private Vector3 moveDirection;
}
