using System;
using UnityEngine;

namespace TMPro.Examples
{
	// Token: 0x02000032 RID: 50
	public class TextMeshSpawner : MonoBehaviour
	{
		// Token: 0x06000102 RID: 258 RVA: 0x000086A2 File Offset: 0x000068A2
		private void Awake()
		{
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000086A4 File Offset: 0x000068A4
		private void Start()
		{
			for (int i = 0; i < this.NumberOfNPC; i++)
			{
				if (this.SpawnType == 0)
				{
					GameObject gameObject = new GameObject();
					gameObject.transform.position = new Vector3(Random.Range(-95f, 95f), 0.5f, Random.Range(-95f, 95f));
					TextMeshPro textMeshPro = gameObject.AddComponent<TextMeshPro>();
					textMeshPro.fontSize = 96f;
					textMeshPro.text = "!";
					textMeshPro.color = new Color32(byte.MaxValue, byte.MaxValue, 0, byte.MaxValue);
					this.floatingText_Script = gameObject.AddComponent<TextMeshProFloatingText>();
					this.floatingText_Script.SpawnType = 0;
				}
				else
				{
					GameObject gameObject2 = new GameObject();
					gameObject2.transform.position = new Vector3(Random.Range(-95f, 95f), 0.5f, Random.Range(-95f, 95f));
					TextMesh textMesh = gameObject2.AddComponent<TextMesh>();
					textMesh.GetComponent<Renderer>().sharedMaterial = this.TheFont.material;
					textMesh.font = this.TheFont;
					textMesh.anchor = TextAnchor.LowerCenter;
					textMesh.fontSize = 96;
					textMesh.color = new Color32(byte.MaxValue, byte.MaxValue, 0, byte.MaxValue);
					textMesh.text = "!";
					this.floatingText_Script = gameObject2.AddComponent<TextMeshProFloatingText>();
					this.floatingText_Script.SpawnType = 1;
				}
			}
		}

		// Token: 0x04000196 RID: 406
		public int SpawnType;

		// Token: 0x04000197 RID: 407
		public int NumberOfNPC = 12;

		// Token: 0x04000198 RID: 408
		public Font TheFont;

		// Token: 0x04000199 RID: 409
		private TextMeshProFloatingText floatingText_Script;
	}
}
