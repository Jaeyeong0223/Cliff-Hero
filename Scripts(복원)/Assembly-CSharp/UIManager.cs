using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x02000019 RID: 25
public class UIManager : MonoBehaviour
{
	// Token: 0x06000094 RID: 148 RVA: 0x00006011 File Offset: 0x00004211
	private void Start()
	{
		this.playerInventory = GameObject.Find("Player").GetComponent<PlayerInventory>();
		this.tutorial.SetActive(true);
		this.pausePanel.SetActive(false);
		Time.timeScale = 0f;
		Cursor.visible = true;
	}

	// Token: 0x06000095 RID: 149 RVA: 0x00006050 File Offset: 0x00004250
	public void OnCloseTutorial()
	{
		this.tutorial.SetActive(false);
		Time.timeScale = 1f;
		Cursor.visible = false;
	}

	// Token: 0x06000096 RID: 150 RVA: 0x0000606E File Offset: 0x0000426E
	public void OnResumeButtonClick()
	{
		this.pausePanel.SetActive(false);
		Time.timeScale = 1f;
		Cursor.visible = false;
	}

	// Token: 0x06000097 RID: 151 RVA: 0x0000608C File Offset: 0x0000428C
	public void OnHomeButtonClick()
	{
		SceneManager.LoadScene("MainScene");
	}

	// Token: 0x06000098 RID: 152 RVA: 0x00006098 File Offset: 0x00004298
	public void OnQuitButtonClick()
	{
		Application.Quit();
	}

	// Token: 0x06000099 RID: 153 RVA: 0x0000609F File Offset: 0x0000429F
	private void Update()
	{
		this.ChangeWeapon();
		this.PausePanel();
	}

	// Token: 0x0600009A RID: 154 RVA: 0x000060AD File Offset: 0x000042AD
	private void PausePanel()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			this.pausePanel.SetActive(true);
			Time.timeScale = 0f;
			Cursor.visible = true;
		}
	}

	// Token: 0x0600009B RID: 155 RVA: 0x000060D4 File Offset: 0x000042D4
	private void ChangeWeapon()
	{
		for (int i = 0; i < this.weapons.Length; i++)
		{
			this.mainWeapons[i].SetActive(this.weapons[i].activeSelf);
			this.skills[i].SetActive(this.weapons[i].activeSelf);
		}
		if (this.playerInventory.equippedWeapons.Count == 2)
		{
			this.subWeapons[0] = this.weapons[this.playerInventory.equippedWeapons[0]];
			this.subWeapons[1] = this.weapons[this.playerInventory.equippedWeapons[1]];
			if (this.subWeapons[1] == this.subWeapons[0])
			{
				for (int j = 0; j < this.weapons.Length; j++)
				{
					if (this.weapons[j] != this.subWeapons[0])
					{
						this.subWeapons[1] = this.weapons[j];
						break;
					}
				}
			}
		}
		for (int k = 0; k < this.subWeaponsIndex.Length; k++)
		{
			this.subWeaponsIndex[k].SetActive((this.subWeapons[0] == this.weapons[k] && this.subWeapons[1].activeSelf) || (this.subWeapons[1] == this.weapons[k] && this.subWeapons[0].activeSelf));
		}
	}

	// Token: 0x04000103 RID: 259
	public GameObject tutorial;

	// Token: 0x04000104 RID: 260
	public GameObject pausePanel;

	// Token: 0x04000105 RID: 261
	public Button startButton;

	// Token: 0x04000106 RID: 262
	public Button resumeButton;

	// Token: 0x04000107 RID: 263
	public Button homeButton;

	// Token: 0x04000108 RID: 264
	public Button quitButton;

	// Token: 0x04000109 RID: 265
	public GameObject[] weapons = new GameObject[9];

	// Token: 0x0400010A RID: 266
	public GameObject[] mainWeapons = new GameObject[9];

	// Token: 0x0400010B RID: 267
	public GameObject[] subWeapons = new GameObject[2];

	// Token: 0x0400010C RID: 268
	public GameObject[] subWeaponsIndex = new GameObject[9];

	// Token: 0x0400010D RID: 269
	public GameObject[] skills = new GameObject[9];

	// Token: 0x0400010E RID: 270
	private PlayerInventory playerInventory;
}
