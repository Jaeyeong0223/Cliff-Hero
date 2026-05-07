using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x02000007 RID: 7
public class PickUI : MonoBehaviour
{
	// Token: 0x06000037 RID: 55 RVA: 0x00004114 File Offset: 0x00002314
	private void Start()
	{
		this.maleTextKnight.gameObject.SetActive(true);
		this.femaleTextKnight.gameObject.SetActive(false);
		this.maleTextWizard.gameObject.SetActive(true);
		this.femaleTextWizard.gameObject.SetActive(false);
		this.maleTextArcher.gameObject.SetActive(true);
		this.femaleTextArcher.gameObject.SetActive(false);
	}

	// Token: 0x06000038 RID: 56 RVA: 0x00004187 File Offset: 0x00002387
	public void OnKnightpickButton()
	{
		SceneManager.LoadScene("Stage1Scene");
	}

	// Token: 0x06000039 RID: 57 RVA: 0x00004194 File Offset: 0x00002394
	public void ChagesexButtonKnight()
	{
		if (this.isMale)
		{
			this.maleTextKnight.gameObject.SetActive(false);
			this.femaleTextKnight.gameObject.SetActive(true);
		}
		else
		{
			this.maleTextKnight.gameObject.SetActive(true);
			this.femaleTextKnight.gameObject.SetActive(false);
		}
		this.isMale = !this.isMale;
	}

	// Token: 0x0600003A RID: 58 RVA: 0x00004200 File Offset: 0x00002400
	public void ChagesexButtonWizard()
	{
		if (this.isMale)
		{
			this.maleTextWizard.gameObject.SetActive(false);
			this.femaleTextWizard.gameObject.SetActive(true);
		}
		else
		{
			this.maleTextWizard.gameObject.SetActive(true);
			this.femaleTextWizard.gameObject.SetActive(false);
		}
		this.isMale = !this.isMale;
	}

	// Token: 0x0600003B RID: 59 RVA: 0x0000426C File Offset: 0x0000246C
	public void ChagesexButtonArcher()
	{
		if (this.isMale)
		{
			this.maleTextArcher.gameObject.SetActive(false);
			this.femaleTextArcher.gameObject.SetActive(true);
		}
		else
		{
			this.maleTextArcher.gameObject.SetActive(true);
			this.femaleTextArcher.gameObject.SetActive(false);
		}
		this.isMale = !this.isMale;
	}

	// Token: 0x04000070 RID: 112
	[Header("Button")]
	public Button changesexButtonKnight;

	// Token: 0x04000071 RID: 113
	public Button changesexButtonWizard;

	// Token: 0x04000072 RID: 114
	public Button changesexButtonArcher;

	// Token: 0x04000073 RID: 115
	public Button knightpickButton;

	// Token: 0x04000074 RID: 116
	[Header("Text")]
	public Text maleTextKnight;

	// Token: 0x04000075 RID: 117
	public Text femaleTextKnight;

	// Token: 0x04000076 RID: 118
	public Text maleTextWizard;

	// Token: 0x04000077 RID: 119
	public Text femaleTextWizard;

	// Token: 0x04000078 RID: 120
	public Text maleTextArcher;

	// Token: 0x04000079 RID: 121
	public Text femaleTextArcher;

	// Token: 0x0400007A RID: 122
	private bool isMale = true;
}
