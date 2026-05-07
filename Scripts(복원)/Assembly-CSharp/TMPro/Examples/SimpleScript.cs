using System;
using UnityEngine;

namespace TMPro.Examples
{
	// Token: 0x0200002D RID: 45
	public class SimpleScript : MonoBehaviour
	{
		// Token: 0x060000E9 RID: 233 RVA: 0x00008060 File Offset: 0x00006260
		private void Start()
		{
			this.m_textMeshPro = base.gameObject.AddComponent<TextMeshPro>();
			this.m_textMeshPro.autoSizeTextContainer = true;
			this.m_textMeshPro.fontSize = 48f;
			this.m_textMeshPro.alignment = TextAlignmentOptions.Center;
			this.m_textMeshPro.enableWordWrapping = false;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000080B6 File Offset: 0x000062B6
		private void Update()
		{
			this.m_textMeshPro.SetText("The <#0050FF>count is: </color>{0:2}", this.m_frame % 1000f);
			this.m_frame += 1f * Time.deltaTime;
		}

		// Token: 0x0400017D RID: 381
		private TextMeshPro m_textMeshPro;

		// Token: 0x0400017E RID: 382
		private const string label = "The <#0050FF>count is: </color>{0:2}";

		// Token: 0x0400017F RID: 383
		private float m_frame;
	}
}
