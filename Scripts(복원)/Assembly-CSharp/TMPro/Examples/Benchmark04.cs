using System;
using UnityEngine;

namespace TMPro.Examples
{
	// Token: 0x02000029 RID: 41
	public class Benchmark04 : MonoBehaviour
	{
		// Token: 0x060000DB RID: 219 RVA: 0x0000755C File Offset: 0x0000575C
		private void Start()
		{
			this.m_Transform = base.transform;
			float num = 0f;
			float num2 = Camera.main.orthographicSize = (float)(Screen.height / 2);
			float num3 = (float)Screen.width / (float)Screen.height;
			for (int i = this.MinPointSize; i <= this.MaxPointSize; i += this.Steps)
			{
				if (this.SpawnType == 0)
				{
					GameObject gameObject = new GameObject("Text - " + i.ToString() + " Pts");
					if (num > num2 * 2f)
					{
						return;
					}
					gameObject.transform.position = this.m_Transform.position + new Vector3(num3 * -num2 * 0.975f, num2 * 0.975f - num, 0f);
					TextMeshPro textMeshPro = gameObject.AddComponent<TextMeshPro>();
					textMeshPro.rectTransform.pivot = new Vector2(0f, 0.5f);
					textMeshPro.enableWordWrapping = false;
					textMeshPro.extraPadding = true;
					textMeshPro.isOrthographic = true;
					textMeshPro.fontSize = (float)i;
					textMeshPro.text = i.ToString() + " pts - Lorem ipsum dolor sit...";
					textMeshPro.color = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
					num += (float)i;
				}
			}
		}

		// Token: 0x04000151 RID: 337
		public int SpawnType;

		// Token: 0x04000152 RID: 338
		public int MinPointSize = 12;

		// Token: 0x04000153 RID: 339
		public int MaxPointSize = 64;

		// Token: 0x04000154 RID: 340
		public int Steps = 4;

		// Token: 0x04000155 RID: 341
		private Transform m_Transform;
	}
}
