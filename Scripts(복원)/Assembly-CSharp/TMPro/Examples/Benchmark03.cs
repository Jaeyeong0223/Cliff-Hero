using System;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;

namespace TMPro.Examples
{
	// Token: 0x02000028 RID: 40
	public class Benchmark03 : MonoBehaviour
	{
		// Token: 0x060000D8 RID: 216 RVA: 0x0000731B File Offset: 0x0000551B
		private void Awake()
		{
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00007320 File Offset: 0x00005520
		private void Start()
		{
			TMP_FontAsset tmp_FontAsset = null;
			switch (this.Benchmark)
			{
			case Benchmark03.BenchmarkType.TMP_SDF_MOBILE:
				tmp_FontAsset = TMP_FontAsset.CreateFontAsset(this.SourceFont, 90, 9, GlyphRenderMode.SDFAA, 256, 256, AtlasPopulationMode.Dynamic, true);
				break;
			case Benchmark03.BenchmarkType.TMP_SDF__MOBILE_SSD:
				tmp_FontAsset = TMP_FontAsset.CreateFontAsset(this.SourceFont, 90, 9, GlyphRenderMode.SDFAA, 256, 256, AtlasPopulationMode.Dynamic, true);
				tmp_FontAsset.material.shader = Shader.Find("TextMeshPro/Mobile/Distance Field SSD");
				break;
			case Benchmark03.BenchmarkType.TMP_SDF:
				tmp_FontAsset = TMP_FontAsset.CreateFontAsset(this.SourceFont, 90, 9, GlyphRenderMode.SDFAA, 256, 256, AtlasPopulationMode.Dynamic, true);
				tmp_FontAsset.material.shader = Shader.Find("TextMeshPro/Distance Field");
				break;
			case Benchmark03.BenchmarkType.TMP_BITMAP_MOBILE:
				tmp_FontAsset = TMP_FontAsset.CreateFontAsset(this.SourceFont, 90, 9, GlyphRenderMode.SMOOTH, 256, 256, AtlasPopulationMode.Dynamic, true);
				break;
			}
			for (int i = 0; i < this.NumberOfSamples; i++)
			{
				Benchmark03.BenchmarkType benchmark = this.Benchmark;
				if (benchmark > Benchmark03.BenchmarkType.TMP_BITMAP_MOBILE)
				{
					if (benchmark == Benchmark03.BenchmarkType.TEXTMESH_BITMAP)
					{
						TextMesh textMesh = new GameObject
						{
							transform = 
							{
								position = new Vector3(0f, 1.2f, 0f)
							}
						}.AddComponent<TextMesh>();
						textMesh.GetComponent<Renderer>().sharedMaterial = this.SourceFont.material;
						textMesh.font = this.SourceFont;
						textMesh.anchor = TextAnchor.MiddleCenter;
						textMesh.fontSize = 130;
						textMesh.color = new Color32(byte.MaxValue, byte.MaxValue, 0, byte.MaxValue);
						textMesh.text = "@";
					}
				}
				else
				{
					TextMeshPro textMeshPro = new GameObject
					{
						transform = 
						{
							position = new Vector3(0f, 1.2f, 0f)
						}
					}.AddComponent<TextMeshPro>();
					textMeshPro.font = tmp_FontAsset;
					textMeshPro.fontSize = 128f;
					textMeshPro.text = "@";
					textMeshPro.alignment = TextAlignmentOptions.Center;
					textMeshPro.color = new Color32(byte.MaxValue, byte.MaxValue, 0, byte.MaxValue);
					if (this.Benchmark == Benchmark03.BenchmarkType.TMP_BITMAP_MOBILE)
					{
						textMeshPro.fontSize = 132f;
					}
				}
			}
		}

		// Token: 0x0400014E RID: 334
		public int NumberOfSamples = 100;

		// Token: 0x0400014F RID: 335
		public Benchmark03.BenchmarkType Benchmark;

		// Token: 0x04000150 RID: 336
		public Font SourceFont;

		// Token: 0x02000060 RID: 96
		public enum BenchmarkType
		{
			// Token: 0x04000258 RID: 600
			TMP_SDF_MOBILE,
			// Token: 0x04000259 RID: 601
			TMP_SDF__MOBILE_SSD,
			// Token: 0x0400025A RID: 602
			TMP_SDF,
			// Token: 0x0400025B RID: 603
			TMP_BITMAP_MOBILE,
			// Token: 0x0400025C RID: 604
			TEXTMESH_BITMAP
		}
	}
}
