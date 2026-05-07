using System;
using System.Collections.Generic;
using UnityEngine;

namespace PositionOrder
{
	// Token: 0x02000045 RID: 69
	public class PositionOrderer
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000153 RID: 339 RVA: 0x0000A90F File Offset: 0x00008B0F
		// (set) Token: 0x06000154 RID: 340 RVA: 0x0000A917 File Offset: 0x00008B17
		public float Distance_X { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000155 RID: 341 RVA: 0x0000A920 File Offset: 0x00008B20
		// (set) Token: 0x06000156 RID: 342 RVA: 0x0000A928 File Offset: 0x00008B28
		public float Distance_Y { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000157 RID: 343 RVA: 0x0000A931 File Offset: 0x00008B31
		// (set) Token: 0x06000158 RID: 344 RVA: 0x0000A939 File Offset: 0x00008B39
		public float Distance_Z { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000159 RID: 345 RVA: 0x0000A942 File Offset: 0x00008B42
		// (set) Token: 0x0600015A RID: 346 RVA: 0x0000A94A File Offset: 0x00008B4A
		public List<Transform> Transforms { get; set; }

		// Token: 0x0600015B RID: 347 RVA: 0x0000A953 File Offset: 0x00008B53
		public PositionOrderer()
		{
			this.Transforms = new List<Transform>();
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000A968 File Offset: 0x00008B68
		public void ApplyLineOrder(LineAnchor anchor, int customIndex = 0)
		{
			if (!this.IsCountSafe(PositionOrderer.WarningRequest.TransformOnly, 0, 0))
			{
				return;
			}
			int num = 0;
			if (anchor == LineAnchor.Custom)
			{
				if (!this.TrySetCustomIndex(customIndex, out num))
				{
					return;
				}
			}
			else
			{
				num = this.GetStandardIndexFromLineAnchor(anchor);
			}
			int count = this.Transforms.Count;
			Vector3 position = this.Transforms[num].position;
			Vector3 zero = Vector3.zero;
			for (int i = 0; i < count; i++)
			{
				zero.Set((float)(i - num) * this.Distance_X, (float)(i - num) * this.Distance_Y, (float)(i - num) * this.Distance_Z);
				this.Transforms[i].position = position + zero;
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000AA14 File Offset: 0x00008C14
		public void ApplyTableOrder(TableAnchor anchor, Axis2D axis, int col, int customIndex = 0)
		{
			if (!this.IsCountSafe(PositionOrderer.WarningRequest.Column, col, 0))
			{
				return;
			}
			int num = 0;
			if (anchor == TableAnchor.Custom)
			{
				if (!this.TrySetCustomIndex(customIndex, out num))
				{
					return;
				}
			}
			else
			{
				num = this.GetStandardIndexFromTableAnchor(anchor, col);
			}
			int num2 = num % col;
			int num3 = num / col;
			Vector3 position = this.Transforms[num].position;
			Vector3 zero = Vector3.zero;
			for (int i = 0; i < this.Transforms.Count; i++)
			{
				int num4 = i % col;
				int num5 = i / col;
				this.SetDistanceByAxis2D(ref zero, axis, (float)(num4 - num2), (float)(num3 - num5));
				this.Transforms[i].position = position + zero;
			}
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000AAC0 File Offset: 0x00008CC0
		private void SetDistanceByAxis2D(ref Vector3 vector, Axis2D axis, float col, float row)
		{
			switch (axis)
			{
			case Axis2D.XY:
				vector.Set(col * this.Distance_X, row * this.Distance_Y, 0f);
				return;
			case Axis2D.XZ:
				vector.Set(col * this.Distance_X, 0f, row * this.Distance_Z);
				return;
			case Axis2D.ZY:
				vector.Set(0f, row * this.Distance_Y, col * this.Distance_Z);
				return;
			default:
				return;
			}
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000AB38 File Offset: 0x00008D38
		public void ApplyCubeOrder(CubeAnchor anchor, int col, int row, int customIndex = 0)
		{
			if (!this.IsCountSafe(PositionOrderer.WarningRequest.ColumnAndRow, col, row))
			{
				return;
			}
			int num = 0;
			if (anchor == CubeAnchor.Custom)
			{
				if (!this.TrySetCustomIndex(customIndex, out num))
				{
					return;
				}
			}
			else
			{
				num = this.GetStandardIndexFromCubeAnchor(anchor, col, row);
			}
			int num2 = col * row;
			int num3 = num / num2;
			int num4 = num % col;
			int num5 = num % num2 / col;
			Vector3 position = this.Transforms[num].position;
			Vector3 zero = Vector3.zero;
			for (int i = 0; i < this.Transforms.Count; i++)
			{
				int num6 = i / num2;
				int num7 = i % col;
				int num8 = i % num2 / col;
				zero.Set((float)(num7 - num4) * this.Distance_X, (float)(num3 - num6) * this.Distance_Y, (float)(num5 - num8) * this.Distance_Z);
				this.Transforms[i].position = position + zero;
			}
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000AC10 File Offset: 0x00008E10
		private bool IsCountSafe(PositionOrderer.WarningRequest warning, int col = 0, int row = 0)
		{
			if (this.Transforms.Count < 2)
			{
				Debug.LogWarning("Transform count in list is too small.");
				return false;
			}
			if (warning == PositionOrderer.WarningRequest.Column || warning == PositionOrderer.WarningRequest.ColumnAndRow)
			{
				if (col < 2)
				{
					Debug.LogWarning("Column count is too small.");
					return false;
				}
				if (warning == PositionOrderer.WarningRequest.ColumnAndRow && row < 2)
				{
					Debug.LogWarning("Row count is too small.");
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000AC64 File Offset: 0x00008E64
		private bool TrySetCustomIndex(int customIndex, out int result)
		{
			if (customIndex >= 0 && customIndex < this.Transforms.Count)
			{
				result = customIndex;
				return true;
			}
			Debug.LogError("Custom index out of range: " + customIndex.ToString());
			result = -1;
			return false;
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000AC98 File Offset: 0x00008E98
		private int GetStandardIndexFromLineAnchor(LineAnchor anchor)
		{
			int count = this.Transforms.Count;
			switch (anchor)
			{
			case LineAnchor.Start:
				return 0;
			case LineAnchor.Center:
				return count / 2;
			case LineAnchor.End:
				return count - 1;
			default:
				return -1;
			}
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000ACD0 File Offset: 0x00008ED0
		private int GetStandardIndexFromTableAnchor(TableAnchor anchor, int col)
		{
			int count = this.Transforms.Count;
			int num = Mathf.CeilToInt((float)count / (float)col);
			switch (anchor)
			{
			case TableAnchor.TopLeft:
				return 0;
			case TableAnchor.TopMiddle:
				return col / 2;
			case TableAnchor.TopRight:
				return col - 1;
			case TableAnchor.MiddleLeft:
				return col * (num / 2);
			case TableAnchor.Center:
				return count / 2;
			case TableAnchor.MiddleRight:
				return col * (num / 2) + (col - 1);
			case TableAnchor.BottomLeft:
				return num * (col - 1);
			case TableAnchor.BottomMiddle:
				return num * (col - 1) + num / 2;
			case TableAnchor.BottomRight:
				return count - 1;
			default:
				return -1;
			}
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000AD54 File Offset: 0x00008F54
		private int GetStandardIndexFromCubeAnchor(CubeAnchor anchor, int col, int row)
		{
			int count = this.Transforms.Count;
			int num = col * row;
			int num2 = num * (Mathf.CeilToInt((float)count / (float)num) / 2);
			int num3 = num / col;
			switch (anchor)
			{
			case CubeAnchor.Up:
				return num / 2;
			case CubeAnchor.Forward:
				return num2 + col / 2;
			case CubeAnchor.Left:
				return num2 + num3;
			case CubeAnchor.Center:
				return count / 2;
			case CubeAnchor.Right:
				return num2 + num3 + (col - 1);
			case CubeAnchor.Back:
				return num2 + (num - (col / 2 + 1));
			case CubeAnchor.Down:
				return count - 1 - num / 2;
			default:
				return -1;
			}
		}

		// Token: 0x0400020A RID: 522
		public const int MIN_COUNT = 2;

		// Token: 0x02000076 RID: 118
		private enum WarningRequest
		{
			// Token: 0x040002CF RID: 719
			TransformOnly,
			// Token: 0x040002D0 RID: 720
			Column,
			// Token: 0x040002D1 RID: 721
			ColumnAndRow
		}
	}
}
