using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x02000015 RID: 21
public class LoadingSecneController : MonoBehaviour
{
	// Token: 0x06000073 RID: 115 RVA: 0x00004A99 File Offset: 0x00002C99
	public void OnClickStartGame()
	{
		this.loadingPanel.SetActive(true);
		base.StartCoroutine(this.LoadPlayScene());
	}

	// Token: 0x06000074 RID: 116 RVA: 0x00004AB4 File Offset: 0x00002CB4
	private IEnumerator LoadPlayScene()
	{
		AsyncOperation op = SceneManager.LoadSceneAsync("Play");
		op.allowSceneActivation = false;
		while (!op.isDone)
		{
			float num = Mathf.Clamp01(op.progress / 0.9f);
			if (this.progressBar != null)
			{
				this.progressBar.value = num;
			}
			if (this.progressText != null)
			{
				this.progressText.text = (num * 100f).ToString("F0") + "%";
			}
			if (op.progress >= 0.9f)
			{
				yield return new WaitForSeconds(1f);
				op.allowSceneActivation = true;
			}
			yield return null;
		}
		yield break;
	}

	// Token: 0x040000C8 RID: 200
	[Header("Loading UI")]
	public GameObject loadingPanel;

	// Token: 0x040000C9 RID: 201
	public Slider progressBar;

	// Token: 0x040000CA RID: 202
	public Text progressText;
}
