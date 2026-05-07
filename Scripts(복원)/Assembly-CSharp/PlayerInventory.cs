using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000017 RID: 23
public class PlayerInventory : MonoBehaviour
{
	// Token: 0x0600008B RID: 139 RVA: 0x000052F8 File Offset: 0x000034F8
	private void Update()
	{
		WeaponCtrl componentInChildren = base.GetComponentInChildren<WeaponCtrl>();
		if (componentInChildren != null && !componentInChildren.coolDown && !this.swapCooldown && !base.GetComponent<Player>().isSliding && Input.GetKeyDown(KeyCode.Q))
		{
			componentInChildren.isAttack = false;
			GameManager.instance.isFirstWeapon = !GameManager.instance.isFirstWeapon;
			this.SwapWeapons();
			base.StartCoroutine(this.SwapCooldownCoroutine());
		}
		if (this.swapCooldownImage != null)
		{
			if (this.swapCooldown)
			{
				this.swapCooldownImage.fillAmount = this.swapCooldownTimer / this.swapCooldownTime;
				return;
			}
			this.swapCooldownImage.fillAmount = 0f;
		}
	}

	// Token: 0x0600008C RID: 140 RVA: 0x000053AB File Offset: 0x000035AB
	private IEnumerator SwapCooldownCoroutine()
	{
		this.swapCooldown = true;
		this.swapCooldownTimer = this.swapCooldownTime;
		while (this.swapCooldownTimer > 0f)
		{
			this.swapCooldownTimer -= Time.deltaTime;
			yield return null;
		}
		this.swapCooldown = false;
		this.swapCooldownTimer = 0f;
		yield break;
	}

	// Token: 0x0600008D RID: 141 RVA: 0x000053BC File Offset: 0x000035BC
	private void SwapWeapons()
	{
		if (this.equippedWeapons.Count >= 2)
		{
			this.activeWeaponIndex = (this.activeWeaponIndex + 1) % this.equippedWeapons.Count;
			if (GameManager.instance.isFirstWeapon)
			{
				this.suk = base.GetComponent<WeaponSuk>().firstWeaponSuk;
			}
			else
			{
				this.suk = base.GetComponent<WeaponSuk>().secondWeaponSuk;
			}
			for (int i = 0; i < this.equippedWeapons.Count; i++)
			{
				int num = this.equippedWeapons[i];
				if (num >= 0 && num < this.weapons.Length)
				{
					this.weapons[num].SetActive(i == this.activeWeaponIndex);
				}
				else
				{
					Debug.LogError("Invalid weapon index: " + num.ToString());
				}
			}
		}
	}

	// Token: 0x0600008E RID: 142 RVA: 0x00005484 File Offset: 0x00003684
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Sword") || other.CompareTag("Staff") || other.CompareTag("Bow") || other.CompareTag("Hammer"))
		{
			Item component = other.GetComponent<Item>();
			if (component == null)
			{
				Debug.LogError("No Item component found on the object.");
				return;
			}
			int value = component.value;
			if (this.weapons[0].activeSelf && this.weaponLevels[0] >= 5 && other.CompareTag("Staff"))
			{
				this.hasWeapons[0] = false;
				this.weapons[0].SetActive(false);
				this.equippedWeapons.Remove(0);
				this.hasWeapons[3] = true;
				this.weapons[3].SetActive(true);
				this.equippedWeapons.Add(3);
				this.activeWeaponIndex = this.equippedWeapons.IndexOf(3);
				WeaponCtrl componentInChildren = this.weapons[3].GetComponentInChildren<WeaponCtrl>();
				if (componentInChildren != null)
				{
					componentInChildren.damage = 150;
				}
				Object.Destroy(other.gameObject);
				return;
			}
			if (this.weapons[0].activeSelf && this.weaponLevels[0] >= 5 && other.CompareTag("Bow"))
			{
				this.hasWeapons[0] = false;
				this.weapons[0].SetActive(false);
				this.equippedWeapons.Remove(0);
				this.hasWeapons[5] = true;
				this.weapons[5].SetActive(true);
				this.equippedWeapons.Add(5);
				this.activeWeaponIndex = this.equippedWeapons.IndexOf(5);
				WeaponCtrl componentInChildren2 = this.weapons[5].GetComponentInChildren<WeaponCtrl>();
				if (componentInChildren2 != null)
				{
					componentInChildren2.damage = 150;
				}
				Object.Destroy(other.gameObject);
				return;
			}
			if (this.weapons[2].activeSelf && this.weaponLevels[2] >= 5 && other.CompareTag("Sword"))
			{
				this.hasWeapons[2] = false;
				this.weapons[2].SetActive(false);
				this.equippedWeapons.Remove(2);
				this.hasWeapons[3] = true;
				this.weapons[3].SetActive(true);
				this.equippedWeapons.Add(3);
				this.activeWeaponIndex = this.equippedWeapons.IndexOf(3);
				WeaponCtrl componentInChildren3 = this.weapons[3].GetComponentInChildren<WeaponCtrl>();
				if (componentInChildren3 != null)
				{
					componentInChildren3.damage = 150;
				}
				Object.Destroy(other.gameObject);
				return;
			}
			if (this.weapons[2].activeSelf && this.weaponLevels[2] >= 5 && other.CompareTag("Bow"))
			{
				this.hasWeapons[2] = false;
				this.weapons[2].SetActive(false);
				this.equippedWeapons.Remove(2);
				this.hasWeapons[4] = true;
				this.weapons[4].SetActive(true);
				this.equippedWeapons.Add(4);
				this.activeWeaponIndex = this.equippedWeapons.IndexOf(4);
				WeaponCtrl componentInChildren4 = this.weapons[4].GetComponentInChildren<WeaponCtrl>();
				if (componentInChildren4 != null)
				{
					componentInChildren4.damage = 40;
				}
				Object.Destroy(other.gameObject);
				return;
			}
			if (this.weapons[1].activeSelf && this.weaponLevels[1] >= 5 && other.CompareTag("Sword"))
			{
				this.hasWeapons[1] = false;
				this.weapons[1].SetActive(false);
				this.equippedWeapons.Remove(1);
				this.hasWeapons[5] = true;
				this.weapons[5].SetActive(true);
				this.equippedWeapons.Add(5);
				this.activeWeaponIndex = this.equippedWeapons.IndexOf(5);
				WeaponCtrl componentInChildren5 = this.weapons[5].GetComponentInChildren<WeaponCtrl>();
				if (componentInChildren5 != null)
				{
					componentInChildren5.damage = 150;
				}
				Object.Destroy(other.gameObject);
				return;
			}
			if (this.weapons[1].activeSelf && this.weaponLevels[1] >= 5 && other.CompareTag("Staff"))
			{
				this.hasWeapons[1] = false;
				this.weapons[1].SetActive(false);
				this.equippedWeapons.Remove(1);
				this.hasWeapons[4] = true;
				this.weapons[4].SetActive(true);
				this.equippedWeapons.Add(4);
				this.activeWeaponIndex = this.equippedWeapons.IndexOf(4);
				WeaponCtrl componentInChildren6 = this.weapons[4].GetComponentInChildren<WeaponCtrl>();
				if (componentInChildren6 != null)
				{
					componentInChildren6.damage = 150;
				}
				Object.Destroy(other.gameObject);
				return;
			}
			Debug.Log(this.activeWeaponIndex);
			if (this.weapons[1].activeSelf && other.CompareTag("Hammer"))
			{
				for (int i = 0; i < this.equippedWeapons.Count; i++)
				{
					int num = this.equippedWeapons[i];
					if (num != 1 && num != 7)
					{
						this.weapons[num].SetActive(false);
					}
				}
				Debug.Log("전설활");
				this.hasWeapons[1] = false;
				this.weapons[1].SetActive(false);
				this.equippedWeapons.Remove(1);
				this.hasWeapons[7] = true;
				this.weapons[7].SetActive(true);
				this.equippedWeapons.Add(7);
				this.activeWeaponIndex = this.equippedWeapons.IndexOf(7);
				WeaponCtrl componentInChildren7 = this.weapons[7].GetComponentInChildren<WeaponCtrl>();
				if (componentInChildren7 != null)
				{
					componentInChildren7.damage = 250;
				}
				Object.Destroy(other.gameObject);
				return;
			}
			if (this.weapons[0].activeSelf && other.CompareTag("Hammer"))
			{
				for (int j = 0; j < this.equippedWeapons.Count; j++)
				{
					int num2 = this.equippedWeapons[j];
					if (num2 != 0 && num2 != 6)
					{
						this.weapons[num2].SetActive(false);
					}
				}
				Debug.Log("전설검");
				this.hasWeapons[0] = false;
				this.weapons[0].SetActive(false);
				this.equippedWeapons.Remove(this.equippedWeapons[0]);
				this.hasWeapons[6] = true;
				this.weapons[6].SetActive(true);
				this.equippedWeapons.Add(6);
				this.activeWeaponIndex = this.equippedWeapons.IndexOf(6);
				WeaponCtrl componentInChildren8 = this.weapons[6].GetComponentInChildren<WeaponCtrl>();
				if (componentInChildren8 != null)
				{
					componentInChildren8.damage = 300;
				}
				Object.Destroy(other.gameObject);
				return;
			}
			if (this.weapons[2].activeSelf && other.CompareTag("Hammer"))
			{
				for (int k = 0; k < this.equippedWeapons.Count; k++)
				{
					int num3 = this.equippedWeapons[k];
					if (num3 != 2 && num3 != 8)
					{
						this.weapons[num3].SetActive(false);
					}
				}
				this.hasWeapons[2] = false;
				this.weapons[2].SetActive(false);
				this.equippedWeapons.Remove(2);
				this.hasWeapons[8] = true;
				this.weapons[8].SetActive(true);
				this.equippedWeapons.Add(8);
				this.activeWeaponIndex = this.equippedWeapons.IndexOf(8);
				WeaponCtrl componentInChildren9 = this.weapons[8].GetComponentInChildren<WeaponCtrl>();
				if (componentInChildren9 != null)
				{
					componentInChildren9.damage = 270;
				}
				Object.Destroy(other.gameObject);
				return;
			}
			if (value >= 0 && value < this.hasWeapons.Length)
			{
				if (this.hasWeapons[value])
				{
					this.weaponLevels[value]++;
					this.ApplyWeaponStats(value);
					Debug.Log("Weapon " + value.ToString() + " leveled up to level " + this.weaponLevels[value].ToString());
					Object.Destroy(other.gameObject);
					return;
				}
				this.hasWeapons[value] = true;
				this.weaponLevels[value] = 1;
				if (this.equippedWeapons.Count < 2)
				{
					this.equippedWeapons.Add(value);
					if (value >= 0 && value < this.weapons.Length)
					{
						this.weapons[value].SetActive(true);
						if (this.equippedWeapons.Count == 2)
						{
							this.weapons[value].SetActive(false);
						}
					}
					else
					{
						Debug.LogError("Invalid weapon index: " + value.ToString());
					}
				}
				else
				{
					this.hasWeapons[value] = false;
				}
				Object.Destroy(other.gameObject);
				return;
			}
			else
			{
				Debug.LogError("Invalid weapon index: " + value.ToString());
				Object.Destroy(other.gameObject);
			}
		}
	}

	// Token: 0x0600008F RID: 143 RVA: 0x00005D5C File Offset: 0x00003F5C
	private void ApplyWeaponStats(int weaponIndex)
	{
		WeaponCtrl componentInChildren = this.weapons[weaponIndex].GetComponentInChildren<WeaponCtrl>();
		if (componentInChildren != null)
		{
			if (weaponIndex == 0)
			{
				if (this.weaponLevels[weaponIndex] == 2)
				{
					componentInChildren.damage += 15;
					this.weaponDamages[weaponIndex] = componentInChildren.damage;
					return;
				}
				if (this.weaponLevels[weaponIndex] >= 3 && this.weaponLevels[weaponIndex] <= 5)
				{
					componentInChildren.damage += 30;
					this.weaponDamages[weaponIndex] = componentInChildren.damage;
					return;
				}
			}
			else if (weaponIndex == 1)
			{
				if (this.weaponLevels[weaponIndex] >= 2 && this.weaponLevels[weaponIndex] <= 3)
				{
					componentInChildren.damage += 10;
					this.weaponDamages[weaponIndex] = componentInChildren.damage;
					return;
				}
				if (this.weaponLevels[weaponIndex] >= 4 && this.weaponLevels[weaponIndex] <= 5)
				{
					componentInChildren.damage += 5;
					this.weaponDamages[weaponIndex] = componentInChildren.damage;
					return;
				}
			}
			else if (weaponIndex == 2)
			{
				if (this.weaponLevels[weaponIndex] == 2)
				{
					componentInChildren.damage += 25;
					this.weaponDamages[weaponIndex] = componentInChildren.damage;
					componentInChildren.weaponEffect = this.staffEffect2;
					return;
				}
				if (this.weaponLevels[weaponIndex] == 3)
				{
					componentInChildren.damage += 25;
					this.weaponDamages[weaponIndex] = componentInChildren.damage;
					componentInChildren.weaponEffect = this.staffEffect3;
					return;
				}
				if (this.weaponLevels[weaponIndex] == 4)
				{
					componentInChildren.damage += 25;
					this.weaponDamages[weaponIndex] = componentInChildren.damage;
					componentInChildren.weaponEffect = this.staffEffect4;
					return;
				}
				if (this.weaponLevels[weaponIndex] == 5)
				{
					componentInChildren.damage += 25;
					this.weaponDamages[weaponIndex] = componentInChildren.damage;
					componentInChildren.weaponEffect = this.staffEffect5;
				}
			}
		}
	}

	// Token: 0x040000F2 RID: 242
	[Header("Weapon Swap UI")]
	public Image swapCooldownImage;

	// Token: 0x040000F3 RID: 243
	public float swapCooldownTime = 2f;

	// Token: 0x040000F4 RID: 244
	private bool swapCooldown;

	// Token: 0x040000F5 RID: 245
	private float swapCooldownTimer;

	// Token: 0x040000F6 RID: 246
	public GameObject[] weapons;

	// Token: 0x040000F7 RID: 247
	public GameObject staffEffect2;

	// Token: 0x040000F8 RID: 248
	public GameObject staffEffect3;

	// Token: 0x040000F9 RID: 249
	public GameObject staffEffect4;

	// Token: 0x040000FA RID: 250
	public GameObject staffEffect5;

	// Token: 0x040000FB RID: 251
	public bool[] hasWeapons = new bool[9];

	// Token: 0x040000FC RID: 252
	public List<int> equippedWeapons = new List<int>();

	// Token: 0x040000FD RID: 253
	public int[] weaponLevels = new int[9];

	// Token: 0x040000FE RID: 254
	public int suk;

	// Token: 0x040000FF RID: 255
	private int[] weaponDamages = new int[9];

	// Token: 0x04000100 RID: 256
	private int activeWeaponIndex;
}
