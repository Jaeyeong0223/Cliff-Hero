using System;
using UnityEngine;

// Token: 0x0200000D RID: 13
public class AnimationCtrl : MonoBehaviour
{
	// Token: 0x0600005A RID: 90 RVA: 0x000046C1 File Offset: 0x000028C1
	private void Start()
	{
		this.anim = base.GetComponent<Animator>();
	}

	// Token: 0x0600005B RID: 91 RVA: 0x000046CF File Offset: 0x000028CF
	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			this.anim.SetBool("Sword", true);
			return;
		}
		this.anim.SetBool("Sword", false);
	}

	// Token: 0x040000AC RID: 172
	private Animator anim;
}
