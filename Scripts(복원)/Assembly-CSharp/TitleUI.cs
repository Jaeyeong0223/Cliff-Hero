using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x0200000B RID: 11
public class TitleUI : MonoBehaviour
{
	// Token: 0x06000046 RID: 70 RVA: 0x00004388 File Offset: 0x00002588
	private void Start()
	{
		this.graphicsButton.GetComponent<Image>().enabled = true;
		this.operationButton.GetComponent<Image>().enabled = false;
		this.soundButton.GetComponent<Image>().enabled = false;
		this.SoundVolume();
		if (this.loadingPanel != null)
		{
			this.loadingPanel.SetActive(false);
		}
	}

	// Token: 0x06000047 RID: 71 RVA: 0x000043E8 File Offset: 0x000025E8
	private void Update()
	{
		if (Input.GetKeyUp(KeyCode.Escape))
		{
			this.HideOptionPopup();
		}
	}

	// Token: 0x06000048 RID: 72 RVA: 0x000043F9 File Offset: 0x000025F9
	private void SoundVolume()
	{
		this.volume.value = this.main.volume;
		this.volume.onValueChanged.AddListener(new UnityAction<float>(this.OnVolumeChange));
	}

	// Token: 0x06000049 RID: 73 RVA: 0x0000442D File Offset: 0x0000262D
	private void OnVolumeChange(float value)
	{
		this.main.volume = value;
	}

	// Token: 0x0600004A RID: 74 RVA: 0x0000443B File Offset: 0x0000263B
	private void HideOptionPopup()
	{
		this.optionPopup.SetActive(false);
	}

	// Token: 0x0600004B RID: 75 RVA: 0x00004449 File Offset: 0x00002649
	public void OnStartButton()
	{
		if (this.loadingPanel != null)
		{
			this.loadingPanel.SetActive(true);
		}
		base.StartCoroutine(this.LoadSceneProcess("Play"));
	}

	// Token: 0x0600004C RID: 76 RVA: 0x00004477 File Offset: 0x00002677
	private IEnumerator LoadSceneProcess(string sceneName)
	{
		AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
		operation.allowSceneActivation = false;
		this.displayedProgress = 0f;
		while (!operation.isDone)
		{
			float target = Mathf.Clamp01(operation.progress / 0.9f);
			this.displayedProgress = Mathf.MoveTowards(this.displayedProgress, target, Time.deltaTime * 0.5f);
			if (this.progressBar != null)
			{
				this.progressBar.value = this.displayedProgress;
			}
			if (this.progressText != null)
			{
				this.progressText.text = (this.displayedProgress * 100f).ToString("F0") + "%";
			}
			if (operation.progress >= 0.9f)
			{
				this.displayedProgress = Mathf.MoveTowards(this.displayedProgress, 1f, Time.deltaTime * 0.5f);
				if (this.progressBar != null)
				{
					this.progressBar.value = this.displayedProgress;
				}
				if (this.progressText != null)
				{
					this.progressText.text = (this.displayedProgress * 100f).ToString("F0") + "%";
				}
				if (this.displayedProgress >= 0.99f)
				{
					operation.allowSceneActivation = true;
				}
			}
			yield return null;
		}
		yield break;
	}

	// Token: 0x0600004D RID: 77 RVA: 0x0000448D File Offset: 0x0000268D
	public void OnRestartButton()
	{
		SceneManager.LoadScene("Play");
	}

	// Token: 0x0600004E RID: 78 RVA: 0x00004499 File Offset: 0x00002699
	public void OnMainsceneButton()
	{
		SceneManager.LoadScene("MainScene");
	}

	// Token: 0x0600004F RID: 79 RVA: 0x000044A5 File Offset: 0x000026A5
	public void OnGraphicsButton()
	{
		this.OnGraphicsPanel();
		this.graphicsButton.GetComponent<Image>().enabled = true;
		this.operationButton.GetComponent<Image>().enabled = false;
		this.soundButton.GetComponent<Image>().enabled = false;
	}

	// Token: 0x06000050 RID: 80 RVA: 0x000044E0 File Offset: 0x000026E0
	public void OnOperationButton()
	{
		this.OnOperationPanel();
		this.graphicsButton.GetComponent<Image>().enabled = false;
		this.operationButton.GetComponent<Image>().enabled = true;
		this.soundButton.GetComponent<Image>().enabled = false;
	}

	// Token: 0x06000051 RID: 81 RVA: 0x0000451B File Offset: 0x0000271B
	public void OnSoundButton()
	{
		this.OnSoundPanel();
		this.graphicsButton.GetComponent<Image>().enabled = false;
		this.operationButton.GetComponent<Image>().enabled = false;
		this.soundButton.GetComponent<Image>().enabled = true;
	}

	// Token: 0x06000052 RID: 82 RVA: 0x00004556 File Offset: 0x00002756
	public void OnGraphicsPanel()
	{
		this.graphicsPanel.SetActive(true);
		this.operationPanel.SetActive(false);
		this.soundPanel.SetActive(false);
	}

	// Token: 0x06000053 RID: 83 RVA: 0x0000457C File Offset: 0x0000277C
	public void OnOperationPanel()
	{
		this.graphicsPanel.SetActive(false);
		this.operationPanel.SetActive(true);
		this.soundPanel.SetActive(false);
	}

	// Token: 0x06000054 RID: 84 RVA: 0x000045A2 File Offset: 0x000027A2
	public void OnSoundPanel()
	{
		this.graphicsPanel.SetActive(false);
		this.operationPanel.SetActive(false);
		this.soundPanel.SetActive(true);
	}

	// Token: 0x06000055 RID: 85 RVA: 0x000045C8 File Offset: 0x000027C8
	public void SaveAndExitButton()
	{
		this.HideOptionPopup();
	}

	// Token: 0x06000056 RID: 86 RVA: 0x000045D0 File Offset: 0x000027D0
	public void QuitButton()
	{
		Application.Quit();
	}

	// Token: 0x04000084 RID: 132
	[Header("Panel")]
	public GameObject optionPopup;

	// Token: 0x04000085 RID: 133
	public GameObject graphicsPanel;

	// Token: 0x04000086 RID: 134
	public GameObject operationPanel;

	// Token: 0x04000087 RID: 135
	public GameObject soundPanel;

	// Token: 0x04000088 RID: 136
	[Header("로딩 패널")]
	public GameObject loadingPanel;

	// Token: 0x04000089 RID: 137
	public Slider progressBar;

	// Token: 0x0400008A RID: 138
	public Text progressText;

	// Token: 0x0400008B RID: 139
	[Header("Button")]
	public Button startButton;

	// Token: 0x0400008C RID: 140
	public Button graphicsButton;

	// Token: 0x0400008D RID: 141
	public Button operationButton;

	// Token: 0x0400008E RID: 142
	public Button soundButton;

	// Token: 0x0400008F RID: 143
	public Button saveandExit;

	// Token: 0x04000090 RID: 144
	public Button quitButton;

	// Token: 0x04000091 RID: 145
	[Header("Dropdown")]
	public Dropdown language;

	// Token: 0x04000092 RID: 146
	public Dropdown screenModeDropdown;

	// Token: 0x04000093 RID: 147
	[Header("Text")]
	public Text screenRatio;

	// Token: 0x04000094 RID: 148
	public Text window;

	// Token: 0x04000095 RID: 149
	public Text languageText;

	// Token: 0x04000096 RID: 150
	public Text graphic;

	// Token: 0x04000097 RID: 151
	public Text attack;

	// Token: 0x04000098 RID: 152
	public Text jump;

	// Token: 0x04000099 RID: 153
	public Text sliding;

	// Token: 0x0400009A RID: 154
	public Text replacementofarms;

	// Token: 0x0400009B RID: 155
	public Text skill;

	// Token: 0x0400009C RID: 156
	public Text mastervolume;

	// Token: 0x0400009D RID: 157
	public Text backgroundsound;

	// Token: 0x0400009E RID: 158
	public Text soundeffect;

	// Token: 0x0400009F RID: 159
	public Text graphics;

	// Token: 0x040000A0 RID: 160
	public Text operation;

	// Token: 0x040000A1 RID: 161
	public Text sound;

	// Token: 0x040000A2 RID: 162
	public Text saveandexit;

	// Token: 0x040000A3 RID: 163
	public Text setting;

	// Token: 0x040000A4 RID: 164
	public Text start;

	// Token: 0x040000A5 RID: 165
	[Header("Audio")]
	public AudioSource main;

	// Token: 0x040000A6 RID: 166
	public Slider volume;

	// Token: 0x040000A7 RID: 167
	private float displayedProgress;
}
