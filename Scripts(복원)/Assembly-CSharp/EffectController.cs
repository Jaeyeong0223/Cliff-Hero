using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000005 RID: 5
public class EffectController : MonoBehaviour
{
	// Token: 0x06000028 RID: 40 RVA: 0x00003E74 File Offset: 0x00002074
	private void Start()
	{
		this.armAttackEffectPrefab = Object.Instantiate<GameObject>(this.armAttackEffect, this.armAttackTransform.position, this.armAttackTransform.rotation);
		this.armAttackEffectPrefab.SetActive(false);
		this.armAttackEffectPrefab.transform.SetParent(base.transform);
		this.flightSmokeEffectPrefab = Object.Instantiate<GameObject>(this.flightSmokeEffect, this.flightSmokeTransform.position, this.flightSmokeTransform.rotation);
		this.flightSmokeEffectPrefab.SetActive(false);
		this.flightSmokeEffectPrefab.transform.SetParent(base.transform);
	}

	// Token: 0x06000029 RID: 41 RVA: 0x00003F13 File Offset: 0x00002113
	private void ArmAttackEffectOn()
	{
		this.armAttackEffectPrefab.SetActive(true);
	}

	// Token: 0x0600002A RID: 42 RVA: 0x00003F21 File Offset: 0x00002121
	private void ArmAttackEffectOff()
	{
		this.armAttackEffectPrefab.SetActive(false);
	}

	// Token: 0x0600002B RID: 43 RVA: 0x00003F2F File Offset: 0x0000212F
	private void BiteAttackEffect()
	{
		this.biteAttackEffectPrefab = Object.Instantiate<GameObject>(this.biteAttackEffect, this.biteAttackTransform.position, this.biteAttackTransform.rotation);
		Object.Destroy(this.biteAttackEffectPrefab, 0.1f);
	}

	// Token: 0x0600002C RID: 44 RVA: 0x00003F68 File Offset: 0x00002168
	private void FlightSmokeEffectOn()
	{
		this.isFlightSmokeEffectOn = true;
		this.flightSmokeEffectPrefab.SetActive(true);
		base.StartCoroutine(this.FlightSmokeEffectDelay());
		this.isFlyingAttacking = true;
	}

	// Token: 0x0600002D RID: 45 RVA: 0x00003F91 File Offset: 0x00002191
	private void FlightSmokeEffectOff()
	{
		this.flightSmokeEffectPrefab.SetActive(false);
		this.isFlyingAttacking = false;
	}

	// Token: 0x0600002E RID: 46 RVA: 0x00003FA6 File Offset: 0x000021A6
	private IEnumerator FlightSmokeEffectDelay()
	{
		yield return new WaitForSeconds(0.3f);
		this.isFlightSmokeEffectOn = !this.isFlightSmokeEffectOn;
		this.flightSmokeEffectPrefab.SetActive(this.isFlightSmokeEffectOn);
		if (this.isFlyingAttacking)
		{
			base.StartCoroutine(this.FlightSmokeEffectDelay());
		}
		else
		{
			this.flightSmokeEffectPrefab.SetActive(false);
		}
		yield break;
	}

	// Token: 0x04000060 RID: 96
	[SerializeField]
	private GameObject armAttackEffect;

	// Token: 0x04000061 RID: 97
	[SerializeField]
	private Transform armAttackTransform;

	// Token: 0x04000062 RID: 98
	[SerializeField]
	private GameObject biteAttackEffect;

	// Token: 0x04000063 RID: 99
	[SerializeField]
	private Transform biteAttackTransform;

	// Token: 0x04000064 RID: 100
	[SerializeField]
	private GameObject flightSmokeEffect;

	// Token: 0x04000065 RID: 101
	[SerializeField]
	private Transform flightSmokeTransform;

	// Token: 0x04000066 RID: 102
	private GameObject armAttackEffectPrefab;

	// Token: 0x04000067 RID: 103
	private GameObject biteAttackEffectPrefab;

	// Token: 0x04000068 RID: 104
	private GameObject flightSmokeEffectPrefab;

	// Token: 0x04000069 RID: 105
	private bool isFlightSmokeEffectOn;

	// Token: 0x0400006A RID: 106
	private bool isFlyingAttacking;
}
