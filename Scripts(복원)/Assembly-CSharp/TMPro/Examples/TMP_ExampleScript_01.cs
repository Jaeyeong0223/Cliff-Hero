using System;
using UnityEngine;

namespace TMPro.Examples
{
	// Token: 0x02000034 RID: 52
	public class TMP_ExampleScript_01 : MonoBehaviour
	{
		// Token: 0x06000108 RID: 264 RVA: 0x00008A10 File Offset: 0x00006C10
		private void Awake()
		{
			if (this.ObjectType == TMP_ExampleScript_01.objectType.TextMeshPro)
			{
				this.m_text = (base.GetComponent<TextMeshPro>() ?? base.gameObject.AddComponent<TextMeshPro>());
			}
			else
			{
				this.m_text = (base.GetComponent<TextMeshProUGUI>() ?? base.gameObject.AddComponent<TextMeshProUGUI>());
			}
			this.m_text.font = Resources.Load<TMP_FontAsset>("Fonts & Materials/Anton SDF");
			this.m_text.fontSharedMaterial = Resources.Load<Material>("Fonts & Materials/Anton SDF - Drop Shadow");
			this.m_text.fontSize = 120f;
			this.m_text.text = "A <#0080ff>simple</color> line of text.";
			Vector2 preferredValues = this.m_text.GetPreferredValues(float.PositiveInfinity, float.PositiveInfinity);
			this.m_text.rectTransform.sizeDelta = new Vector2(preferredValues.x, preferredValues.y);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00008ADE File Offset: 0x00006CDE
		private void Update()
		{
			if (!this.isStatic)
			{
				this.m_text.SetText("The count is <#0080ff>{0}</color>", (float)(this.count % 1000));
				this.count++;
			}
		}

		// Token: 0x040001A0 RID: 416
		public TMP_ExampleScript_01.objectType ObjectType;

		// Token: 0x040001A1 RID: 417
		public bool isStatic;

		// Token: 0x040001A2 RID: 418
		private TMP_Text m_text;

		// Token: 0x040001A3 RID: 419
		private const string k_label = "The count is <#0080ff>{0}</color>";

		// Token: 0x040001A4 RID: 420
		private int count;

		// Token: 0x0200006B RID: 107
		public enum objectType
		{
			// Token: 0x0400029B RID: 667
			TextMeshPro,
			// Token: 0x0400029C RID: 668
			TextMeshProUGUI
		}
	}
}
