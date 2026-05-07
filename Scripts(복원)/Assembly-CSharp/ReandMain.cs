using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x02000008 RID: 8
public class ReandMain : MonoBehaviour
{
	// Token: 0x0600003D RID: 61 RVA: 0x000042E5 File Offset: 0x000024E5
	private void Start()
	{
		Cursor.visible = true;
	}

	// Token: 0x0600003E RID: 62 RVA: 0x000042ED File Offset: 0x000024ED
	public void OnRestartButton()
	{
		SceneManager.LoadScene("Play");
	}

	// Token: 0x0600003F RID: 63 RVA: 0x000042F9 File Offset: 0x000024F9
	public void OnMainsceneButton()
	{
		SceneManager.LoadScene("MainScene");
	}

	// Token: 0x0400007B RID: 123
	[Header("Button")]
	public Button restartButton;

	// Token: 0x0400007C RID: 124
	public Button mainButton;
}
