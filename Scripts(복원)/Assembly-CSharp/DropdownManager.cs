using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000004 RID: 4
public class DropdownManager : MonoBehaviour
{
	// Token: 0x06000024 RID: 36 RVA: 0x000035F8 File Offset: 0x000017F8
	private void Start()
	{
		this.screenOption1.text = "테두리 화면";
		this.screenOption2.text = "창 모드";
		this.screenOption3.text = "전체 화면";
		this.screenRatioDropdown.GetComponentInChildren<Text>().text = "화면 비율";
		this.screenRatioDropdown.options.Clear();
		this.screenRatioDropdown.options.Add(this.screenOption1);
		this.screenRatioDropdown.options.Add(this.screenOption2);
		this.screenRatioDropdown.options.Add(this.screenOption3);
		this.languageOption1.text = "한국어";
		this.languageOption2.text = "영어";
		this.languageOption3.text = "일본어";
		this.languageDropdown.GetComponentInChildren<Text>().text = "언어";
		this.languageDropdown.options.Clear();
		this.languageDropdown.options.Add(this.languageOption1);
		this.languageDropdown.options.Add(this.languageOption2);
		this.languageDropdown.options.Add(this.languageOption3);
		this.graphicOption1.text = "높음";
		this.graphicOption2.text = "중간";
		this.graphicOption3.text = "낮음";
		this.graphicDropdown.GetComponentInChildren<Text>().text = "그래픽";
		this.graphicDropdown.options.Clear();
		this.graphicDropdown.options.Add(this.graphicOption1);
		this.graphicDropdown.options.Add(this.graphicOption2);
		this.graphicDropdown.options.Add(this.graphicOption3);
		this.DropdownOptionController();
	}

	// Token: 0x06000025 RID: 37 RVA: 0x000037D0 File Offset: 0x000019D0
	private void Update()
	{
		this.DropdownOptionController();
	}

	// Token: 0x06000026 RID: 38 RVA: 0x000037D8 File Offset: 0x000019D8
	private void DropdownOptionController()
	{
		switch (base.GetComponent<TitleUI>().language.value)
		{
		case 0:
			this.screenOption1.text = "테두리 화면";
			this.screenOption2.text = "창 모드";
			this.screenOption3.text = "전체 화면";
			switch (this.screenRatioDropdown.value)
			{
			case 0:
				this.screenRatioDropdown.GetComponentInChildren<Text>().text = this.screenOption1.text;
				break;
			case 1:
				this.screenRatioDropdown.GetComponentInChildren<Text>().text = this.screenOption2.text;
				break;
			case 2:
				this.screenRatioDropdown.GetComponentInChildren<Text>().text = this.screenOption3.text;
				break;
			}
			this.languageOption1.text = "한국어";
			this.languageOption2.text = "영어";
			this.languageOption3.text = "일본어";
			switch (this.languageDropdown.value)
			{
			case 0:
				this.languageDropdown.GetComponentInChildren<Text>().text = this.languageOption1.text;
				break;
			case 1:
				this.languageDropdown.GetComponentInChildren<Text>().text = this.languageOption2.text;
				break;
			case 2:
				this.languageDropdown.GetComponentInChildren<Text>().text = this.languageOption3.text;
				break;
			}
			this.graphicOption1.text = "높음";
			this.graphicOption2.text = "중간";
			this.graphicOption3.text = "낮음";
			switch (this.graphicDropdown.value)
			{
			case 0:
				this.graphicDropdown.GetComponentInChildren<Text>().text = this.graphicOption1.text;
				return;
			case 1:
				this.graphicDropdown.GetComponentInChildren<Text>().text = this.graphicOption2.text;
				return;
			case 2:
				this.graphicDropdown.GetComponentInChildren<Text>().text = this.graphicOption3.text;
				return;
			default:
				return;
			}
			break;
		case 1:
			this.screenOption1.text = "Border Screen";
			this.screenOption2.text = "Window Mode";
			this.screenOption3.text = "Full Screen";
			switch (this.screenRatioDropdown.value)
			{
			case 0:
				this.screenRatioDropdown.GetComponentInChildren<Text>().text = this.screenOption1.text;
				break;
			case 1:
				this.screenRatioDropdown.GetComponentInChildren<Text>().text = this.screenOption2.text;
				break;
			case 2:
				this.screenRatioDropdown.GetComponentInChildren<Text>().text = this.screenOption3.text;
				break;
			}
			this.languageOption1.text = "Korean";
			this.languageOption2.text = "English";
			this.languageOption3.text = "Japanese";
			switch (this.languageDropdown.value)
			{
			case 0:
				this.languageDropdown.GetComponentInChildren<Text>().text = this.languageOption1.text;
				break;
			case 1:
				this.languageDropdown.GetComponentInChildren<Text>().text = this.languageOption2.text;
				break;
			case 2:
				this.languageDropdown.GetComponentInChildren<Text>().text = this.languageOption3.text;
				break;
			}
			this.graphicOption1.text = "High";
			this.graphicOption2.text = "the middle";
			this.graphicOption3.text = "Low";
			switch (this.graphicDropdown.value)
			{
			case 0:
				this.graphicDropdown.GetComponentInChildren<Text>().text = this.graphicOption1.text;
				return;
			case 1:
				this.graphicDropdown.GetComponentInChildren<Text>().text = this.graphicOption2.text;
				return;
			case 2:
				this.graphicDropdown.GetComponentInChildren<Text>().text = this.graphicOption3.text;
				return;
			default:
				return;
			}
			break;
		case 2:
			this.screenOption1.text = "枠画面";
			this.screenOption2.text = "ウィンドウモード";
			this.screenOption3.text = "全画面";
			switch (this.screenRatioDropdown.value)
			{
			case 0:
				this.screenRatioDropdown.GetComponentInChildren<Text>().text = this.screenOption1.text;
				break;
			case 1:
				this.screenRatioDropdown.GetComponentInChildren<Text>().text = this.screenOption2.text;
				break;
			case 2:
				this.screenRatioDropdown.GetComponentInChildren<Text>().text = this.screenOption3.text;
				break;
			}
			this.languageOption1.text = "韓国語";
			this.languageOption2.text = "英語";
			this.languageOption3.text = "日本語";
			switch (this.languageDropdown.value)
			{
			case 0:
				this.languageDropdown.GetComponentInChildren<Text>().text = this.languageOption1.text;
				break;
			case 1:
				this.languageDropdown.GetComponentInChildren<Text>().text = this.languageOption2.text;
				break;
			case 2:
				this.languageDropdown.GetComponentInChildren<Text>().text = this.languageOption3.text;
				break;
			}
			this.graphicOption1.text = "高";
			this.graphicOption2.text = "中間";
			this.graphicOption3.text = "低";
			switch (this.graphicDropdown.value)
			{
			case 0:
				this.graphicDropdown.GetComponentInChildren<Text>().text = this.graphicOption1.text;
				return;
			case 1:
				this.graphicDropdown.GetComponentInChildren<Text>().text = this.graphicOption2.text;
				return;
			case 2:
				this.graphicDropdown.GetComponentInChildren<Text>().text = this.graphicOption3.text;
				return;
			default:
				return;
			}
			break;
		default:
			return;
		}
	}

	// Token: 0x04000054 RID: 84
	public Dropdown screenRatioDropdown;

	// Token: 0x04000055 RID: 85
	private Dropdown.OptionData screenOption1 = new Dropdown.OptionData("테두리 화면");

	// Token: 0x04000056 RID: 86
	private Dropdown.OptionData screenOption2 = new Dropdown.OptionData("창 모드");

	// Token: 0x04000057 RID: 87
	private Dropdown.OptionData screenOption3 = new Dropdown.OptionData("전체 화면");

	// Token: 0x04000058 RID: 88
	public Dropdown languageDropdown;

	// Token: 0x04000059 RID: 89
	private Dropdown.OptionData languageOption1 = new Dropdown.OptionData("한국어");

	// Token: 0x0400005A RID: 90
	private Dropdown.OptionData languageOption2 = new Dropdown.OptionData("영어");

	// Token: 0x0400005B RID: 91
	private Dropdown.OptionData languageOption3 = new Dropdown.OptionData("일본어");

	// Token: 0x0400005C RID: 92
	public Dropdown graphicDropdown;

	// Token: 0x0400005D RID: 93
	private Dropdown.OptionData graphicOption1 = new Dropdown.OptionData("높음");

	// Token: 0x0400005E RID: 94
	private Dropdown.OptionData graphicOption2 = new Dropdown.OptionData("중간");

	// Token: 0x0400005F RID: 95
	private Dropdown.OptionData graphicOption3 = new Dropdown.OptionData("낮음");
}
