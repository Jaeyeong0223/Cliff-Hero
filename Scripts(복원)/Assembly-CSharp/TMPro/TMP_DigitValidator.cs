using System;

namespace TMPro
{
	// Token: 0x02000022 RID: 34
	[Serializable]
	public class TMP_DigitValidator : TMP_InputValidator
	{
		// Token: 0x060000BA RID: 186 RVA: 0x00006998 File Offset: 0x00004B98
		public override char Validate(ref string text, ref int pos, char ch)
		{
			if (ch >= '0' && ch <= '9')
			{
				text += ch.ToString();
				pos++;
				return ch;
			}
			return '\0';
		}
	}
}
