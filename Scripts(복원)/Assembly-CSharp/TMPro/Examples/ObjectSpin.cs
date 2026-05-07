using System;
using UnityEngine;

namespace TMPro.Examples
{
	// Token: 0x0200002B RID: 43
	public class ObjectSpin : MonoBehaviour
	{
		// Token: 0x060000E2 RID: 226 RVA: 0x00007E78 File Offset: 0x00006078
		private void Awake()
		{
			this.m_transform = base.transform;
			this.m_initial_Rotation = this.m_transform.rotation.eulerAngles;
			this.m_initial_Position = this.m_transform.position;
			Light component = base.GetComponent<Light>();
			this.m_lightColor = ((component != null) ? component.color : Color.black);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00007EE4 File Offset: 0x000060E4
		private void Update()
		{
			if (this.Motion == ObjectSpin.MotionType.Rotation)
			{
				this.m_transform.Rotate(0f, this.SpinSpeed * Time.deltaTime, 0f);
				return;
			}
			if (this.Motion == ObjectSpin.MotionType.BackAndForth)
			{
				this.m_time += this.SpinSpeed * Time.deltaTime;
				this.m_transform.rotation = Quaternion.Euler(this.m_initial_Rotation.x, Mathf.Sin(this.m_time) * (float)this.RotationRange + this.m_initial_Rotation.y, this.m_initial_Rotation.z);
				return;
			}
			this.m_time += this.SpinSpeed * Time.deltaTime;
			float x = 15f * Mathf.Cos(this.m_time * 0.95f);
			float z = 10f;
			float y = 0f;
			this.m_transform.position = this.m_initial_Position + new Vector3(x, y, z);
			this.m_prevPOS = this.m_transform.position;
			this.frames++;
		}

		// Token: 0x0400016F RID: 367
		public float SpinSpeed = 5f;

		// Token: 0x04000170 RID: 368
		public int RotationRange = 15;

		// Token: 0x04000171 RID: 369
		private Transform m_transform;

		// Token: 0x04000172 RID: 370
		private float m_time;

		// Token: 0x04000173 RID: 371
		private Vector3 m_prevPOS;

		// Token: 0x04000174 RID: 372
		private Vector3 m_initial_Rotation;

		// Token: 0x04000175 RID: 373
		private Vector3 m_initial_Position;

		// Token: 0x04000176 RID: 374
		private Color32 m_lightColor;

		// Token: 0x04000177 RID: 375
		private int frames;

		// Token: 0x04000178 RID: 376
		public ObjectSpin.MotionType Motion;

		// Token: 0x02000062 RID: 98
		public enum MotionType
		{
			// Token: 0x04000262 RID: 610
			Rotation,
			// Token: 0x04000263 RID: 611
			BackAndForth,
			// Token: 0x04000264 RID: 612
			Translation
		}
	}
}
