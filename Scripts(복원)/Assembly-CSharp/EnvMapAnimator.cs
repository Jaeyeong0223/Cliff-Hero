using System;
using System.Collections;
using TMPro;
using UnityEngine;

// Token: 0x02000020 RID: 32
public class EnvMapAnimator : MonoBehaviour
{
	// Token: 0x060000B5 RID: 181 RVA: 0x000068F2 File Offset: 0x00004AF2
	private void Awake()
	{
		this.m_textMeshPro = base.GetComponent<TMP_Text>();
		this.m_material = this.m_textMeshPro.fontSharedMaterial;
	}

	// Token: 0x060000B6 RID: 182 RVA: 0x00006911 File Offset: 0x00004B11
	private IEnumerator Start()
	{
		Matrix4x4 matrix = default(Matrix4x4);
		for (;;)
		{
			matrix.SetTRS(Vector3.zero, Quaternion.Euler(Time.time * this.RotationSpeeds.x, Time.time * this.RotationSpeeds.y, Time.time * this.RotationSpeeds.z), Vector3.one);
			this.m_material.SetMatrix("_EnvMatrix", matrix);
			yield return null;
		}
		yield break;
	}

	// Token: 0x04000127 RID: 295
	public Vector3 RotationSpeeds;

	// Token: 0x04000128 RID: 296
	private TMP_Text m_textMeshPro;

	// Token: 0x04000129 RID: 297
	private Material m_material;
}
