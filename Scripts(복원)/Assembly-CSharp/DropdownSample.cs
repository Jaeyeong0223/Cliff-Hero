using System;
using TMPro;
using UnityEngine;

// Token: 0x0200001F RID: 31
public class DropdownSample : MonoBehaviour
{
	// Token: 0x060000B3 RID: 179 RVA: 0x00006888 File Offset: 0x00004A88
	public void OnButtonClick()
	{
		this.text.text = ((this.dropdownWithPlaceholder.value > -1) ? ("Selected values:\n" + this.dropdownWithoutPlaceholder.value.ToString() + " - " + this.dropdownWithPlaceholder.value.ToString()) : "Error: Please make a selection");
	}

	// Token: 0x04000124 RID: 292
	[SerializeField]
	private TextMeshProUGUI text;

	// Token: 0x04000125 RID: 293
	[SerializeField]
	private TMP_Dropdown dropdownWithoutPlaceholder;

	// Token: 0x04000126 RID: 294
	[SerializeField]
	private TMP_Dropdown dropdownWithPlaceholder;
}
