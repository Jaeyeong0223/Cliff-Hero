using System;
using UnityEngine;

// Token: 0x02000013 RID: 19
public class Item : MonoBehaviour
{
	// Token: 0x040000C5 RID: 197
	public Item.Type type;

	// Token: 0x040000C6 RID: 198
	public int value;

	// Token: 0x02000050 RID: 80
	public enum Type
	{
		// Token: 0x0400022F RID: 559
		Weapon,
		// Token: 0x04000230 RID: 560
		Potion,
		// Token: 0x04000231 RID: 561
		Hammer
	}
}
