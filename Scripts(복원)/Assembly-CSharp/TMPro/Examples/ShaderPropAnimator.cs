using System;
using System.Collections;
using UnityEngine;

namespace TMPro.Examples
{
	// Token: 0x0200002C RID: 44
	public class ShaderPropAnimator : MonoBehaviour
	{
		// Token: 0x060000E5 RID: 229 RVA: 0x00008018 File Offset: 0x00006218
		private void Awake()
		{
			this.m_Renderer = base.GetComponent<Renderer>();
			this.m_Material = this.m_Renderer.material;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00008037 File Offset: 0x00006237
		private void Start()
		{
			base.StartCoroutine(this.AnimateProperties());
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00008046 File Offset: 0x00006246
		private IEnumerator AnimateProperties()
		{
			this.m_frame = Random.Range(0f, 1f);
			for (;;)
			{
				float value = this.GlowCurve.Evaluate(this.m_frame);
				this.m_Material.SetFloat(ShaderUtilities.ID_GlowPower, value);
				this.m_frame += Time.deltaTime * Random.Range(0.2f, 0.3f);
				yield return new WaitForEndOfFrame();
			}
			yield break;
		}

		// Token: 0x04000179 RID: 377
		private Renderer m_Renderer;

		// Token: 0x0400017A RID: 378
		private Material m_Material;

		// Token: 0x0400017B RID: 379
		public AnimationCurve GlowCurve;

		// Token: 0x0400017C RID: 380
		public float m_frame;
	}
}
