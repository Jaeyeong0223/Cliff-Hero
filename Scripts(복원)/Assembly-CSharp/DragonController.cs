using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x02000002 RID: 2
public class DragonController : MonoBehaviour
{
	// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	private void Awake()
	{
		this.phase = DragonController.Phase.First30SecPhase;
		this.isHardMode = false;
		this.hpSlider.gameObject.SetActive(false);
		base.gameObject.transform.position = this.originalPos.transform.position;
		this.fireBreathObject = Object.Instantiate<GameObject>(this.fireBreath, this.firePos.transform.position, this.firePos.transform.rotation);
		this.fireBreathObject.transform.SetParent(base.transform);
		this.fireBreathObject.SetActive(false);
		this.lowerFireBreathObject = Object.Instantiate<GameObject>(this.lowerFireBreath, this.lowerFirePos.transform.position, this.lowerFirePos.transform.rotation);
		this.lowerFireBreathObject.transform.SetParent(base.transform);
		this.lowerFireBreathObject.SetActive(false);
		this.landEffectPrefab = Object.Instantiate<GameObject>(this.landEffect, this.landEffectPos.position, this.landEffectPos.rotation);
		this.landEffectPrefab.transform.SetParent(base.transform);
		this.landEffectPrefab.SetActive(false);
		this.biteAttack.enabled = false;
		this.armAttack.enabled = false;
		this.isOriginalPos = false;
	}

	// Token: 0x06000002 RID: 2 RVA: 0x000021AC File Offset: 0x000003AC
	private void Start()
	{
		this.attackCooltimePA = 0.1f;
		this.target = GameObject.FindGameObjectWithTag("Player").transform;
		this.phase = DragonController.Phase.First30SecPhase;
		this.progressSlider.gameObject.SetActive(false);
		base.StartCoroutine(this.VictorySceneLoad());
	}

	// Token: 0x06000003 RID: 3 RVA: 0x000021FE File Offset: 0x000003FE
	private void Update()
	{
		if (!this.isBossDeading)
		{
			this.PosController();
			this.DashController();
			this.TimerController();
			this.PatternController();
			this.PhaseController();
			this.DashToPos();
		}
		this.HpController();
	}

	// Token: 0x06000004 RID: 4 RVA: 0x00002232 File Offset: 0x00000432
	private IEnumerator VictorySceneLoad()
	{
		yield return new WaitUntil(() => base.gameObject.transform.position.y <= -4f && this.isBossDead);
		SceneManager.LoadScene("VictoryScene");
		yield break;
	}

	// Token: 0x06000005 RID: 5 RVA: 0x00002244 File Offset: 0x00000444
	private void HpController()
	{
		this.hpSlider.value = Mathf.Lerp(this.hpSlider.value, this.currentHp / this.maxHp, Time.deltaTime * 20f);
		if (this.didHoldBack && !this.isAttacking)
		{
			this.anim.SetBool("IsDamaged", true);
			this.didHoldBack = false;
			this.PhaseChanger();
			this.isBreakingIn = false;
		}
		if (this.isBossDead && !this.isAttacking)
		{
			this.isBossDeading = true;
			this.anim.SetBool("IsDead", this.isBossDead);
			base.transform.Translate(Vector3.down * 2f * Time.deltaTime);
		}
	}

	// Token: 0x06000006 RID: 6 RVA: 0x0000230C File Offset: 0x0000050C
	private void PhaseChanger()
	{
		if (this.phase != DragonController.Phase.First30SecPhase)
		{
			this.isPhaseChanged = true;
		}
		else
		{
			this.hpSlider.gameObject.SetActive(true);
		}
		if (this.phase != DragonController.Phase.BossPhase)
		{
			this.phaseChanged++;
			this.isBreakingIn = !this.isBreakingIn;
			this.phaseChangeTimer = 31f;
		}
		if (this.phaseChanged % 2 == 0)
		{
			this.timerSlider.value = 0f;
			base.StartCoroutine(this.NonePhaseOneShoting());
		}
		else if (this.phaseChanged % 2 == 1)
		{
			this.fireBallFirePosPrefab = this.fireBallFirePos;
		}
		switch (this.phaseChanged)
		{
		case 1:
			this.currentHp = this.firstPhaseHp;
			return;
		case 2:
		case 4:
		case 6:
			break;
		case 3:
			this.currentHp = this.secondPhaseHp;
			return;
		case 5:
			this.currentHp = this.thirdPhaseHp;
			return;
		case 7:
			this.currentHp = this.bossHp;
			GameObject.Find("Main Camera").GetComponent<AudioSource>().clip = this.bossBGM;
			GameObject.Find("Main Camera").GetComponent<AudioSource>().Play();
			break;
		default:
			return;
		}
	}

	// Token: 0x06000007 RID: 7 RVA: 0x00002439 File Offset: 0x00000639
	private IEnumerator NonePhaseOneShoting()
	{
		yield return new WaitForSeconds(4f);
		if (this.phase == DragonController.Phase.NonePhase)
		{
			this.rowBreathRotZOffset = 7;
			this.fireBallFirePosPrefab = this.nonePhaseFireBallFirePos;
			this.anim.SetBool("IsNonePhaseOneShoting", true);
		}
		yield break;
	}

	// Token: 0x06000008 RID: 8 RVA: 0x00002448 File Offset: 0x00000648
	private void NonePhaseOneShotAgain()
	{
		base.StartCoroutine(this.NonePhaseOneShoting());
	}

	// Token: 0x06000009 RID: 9 RVA: 0x00002457 File Offset: 0x00000657
	private void NonePhaseOneShotAnimFinish()
	{
		if (this.phase == DragonController.Phase.NonePhase)
		{
			this.anim.SetTrigger("IsNonePhaseTrigger");
		}
		this.anim.SetBool("IsNonePhaseOneShoting", false);
	}

	// Token: 0x0600000A RID: 10 RVA: 0x00002484 File Offset: 0x00000684
	private void TimerController()
	{
		this.dashCoolTime += Time.deltaTime;
		this.highBreathCoolTime += Time.deltaTime;
		this.lowerBreathCoolTime += Time.deltaTime;
		this.rowFireAttackCoolTime += Time.deltaTime;
		this.armAttackCoolTime += Time.deltaTime;
		this.biteAttackCoolTime += Time.deltaTime;
		this.phaseChangeTimer -= Time.deltaTime;
		if (this.phase == DragonController.Phase.First30SecPhase && this.phaseChangeTimer <= 0f)
		{
			this.PhaseChanger();
			return;
		}
		if (this.isBreakingIn && this.phaseChangeTimer <= 0f && !this.isAttacking && this.isOriginalPos && !this.didHoldBack && this.phase != DragonController.Phase.BossPhase)
		{
			this.PhaseChanger();
			return;
		}
		if (this.phase == DragonController.Phase.NonePhase && this.phaseChangeTimer <= 0f)
		{
			this.PhaseChanger();
		}
	}

	// Token: 0x0600000B RID: 11 RVA: 0x00002584 File Offset: 0x00000784
	private void PatternController()
	{
		if (!this.isBreakingIn && this.phaseChanged != 0)
		{
			this.phase = DragonController.Phase.NonePhase;
		}
		if (this.isBreakingIn && this.phaseChanged == 1)
		{
			this.phase = DragonController.Phase.FirstPhase;
		}
		if (this.isBreakingIn && this.phaseChanged == 3)
		{
			this.phase = DragonController.Phase.SecondPhase;
		}
		if (this.isBreakingIn && this.phaseChanged == 5)
		{
			this.phase = DragonController.Phase.ThirdPhase;
		}
		if (this.isBreakingIn && this.phaseChanged == 7)
		{
			this.phase = DragonController.Phase.BossPhase;
		}
	}

	// Token: 0x0600000C RID: 12 RVA: 0x00002608 File Offset: 0x00000808
	private IEnumerator LandEffectOff()
	{
		yield return new WaitForSeconds(0.3f);
		this.landEffectPrefab.SetActive(false);
		yield break;
	}

	// Token: 0x0600000D RID: 13 RVA: 0x00002618 File Offset: 0x00000818
	private void PosController()
	{
		if (this.phase != DragonController.Phase.NonePhase && !this.isAttacking && !this.isDashing && this.isBreakingIn && !this.isDashFinished && !this.isPhaseChanged && !this.isOriginalPos)
		{
			base.gameObject.transform.position = Vector3.MoveTowards(base.gameObject.transform.position, this.originalPos.position, 20f * Time.deltaTime);
		}
		if (base.gameObject.transform.position == this.originalPos.position && !this.isOriginalPos && this.phase != DragonController.Phase.First30SecPhase && this.phase != DragonController.Phase.NonePhase)
		{
			Debug.Log("original Pos");
			this.dashDamaged = false;
			this.landEffectPrefab.SetActive(true);
			base.StartCoroutine(this.LandEffectOff());
			base.StartCoroutine(this.RandomAttack());
			this.isOriginalPos = true;
		}
		if (this.isOriginalPos && this.phase != DragonController.Phase.First30SecPhase && this.phase != DragonController.Phase.NonePhase)
		{
			base.gameObject.transform.position = this.originalPos.transform.position;
		}
		if (this.isDashFinished && !this.isAttacking)
		{
			base.gameObject.transform.position = this.goingToOriginalPos.transform.position;
			this.isDashFinished = false;
			this.spriteRenderer.enabled = true;
			this.isPhaseChanged = false;
		}
		if (this.isDashFinished && this.isAttacking)
		{
			base.gameObject.transform.Translate(Vector2.left * 14f * Time.deltaTime);
		}
		if (this.phase != DragonController.Phase.First30SecPhase)
		{
			if (this.isPhaseChangedWhenNonePhase && this.isPhaseChanged)
			{
				base.gameObject.transform.Translate(Vector2.left * 14f * Time.deltaTime);
				if (this.isOutOfCamera)
				{
					this.hpSlider.gameObject.SetActive(true);
					base.gameObject.transform.position = this.goingToOriginalPos.transform.position;
					this.isPhaseChanged = false;
					this.spriteRenderer.enabled = true;
					this.isPhaseChangedWhenNonePhase = false;
					this.isOutOfCamera = false;
					return;
				}
			}
			else if (!this.isPhaseChangedWhenNonePhase && this.isPhaseChanged && this.phase != DragonController.Phase.BossPhase)
			{
				base.gameObject.transform.Translate(Vector2.right * 14f * Time.deltaTime);
				if (this.isOutOfCamera)
				{
					this.hpSlider.gameObject.SetActive(false);
					base.gameObject.transform.position = this.goingToNonePhasePos.transform.position;
					this.anim.SetBool("IsDamaged", false);
					this.isPhaseChanged = false;
					this.spriteRenderer.enabled = true;
					this.isPhaseChangedWhenNonePhase = true;
					this.isOutOfCamera = false;
				}
			}
		}
	}

	// Token: 0x0600000E RID: 14 RVA: 0x0000293C File Offset: 0x00000B3C
	private void OnBecameInvisible()
	{
		if (this.isAttacking && this.isDashFinished)
		{
			this.spriteRenderer.enabled = false;
			this.isAttacking = false;
		}
		if (this.isPhaseChanged && !this.isPhaseChangedWhenNonePhase)
		{
			this.spriteRenderer.enabled = false;
			this.isOutOfCamera = true;
			this.anim.SetBool("IsNonePhase", true);
			this.anim.SetTrigger("IsNonePhaseTrigger");
			return;
		}
		if (this.isPhaseChanged && this.isPhaseChangedWhenNonePhase)
		{
			this.spriteRenderer.enabled = false;
			this.isOutOfCamera = true;
			this.anim.SetBool("IsNonePhase", false);
		}
	}

	// Token: 0x0600000F RID: 15 RVA: 0x000029E8 File Offset: 0x00000BE8
	private void PhaseController()
	{
		switch (this.phase)
		{
		case DragonController.Phase.First30SecPhase:
			base.gameObject.transform.position = this.first30SecPos.transform.position;
			this.timerSlider.value = 30f - this.phaseChangeTimer;
			this.isBreakingIn = false;
			return;
		case DragonController.Phase.NonePhase:
			if (!this.isPhaseChanged)
			{
				base.gameObject.transform.position = Vector3.MoveTowards(base.gameObject.transform.position, this.nonePhasePos.position, 14f * Time.deltaTime);
			}
			this.timerSlider.value = 31f - this.phaseChangeTimer;
			this.isOriginalPos = false;
			this.isBreakingIn = false;
			return;
		case DragonController.Phase.FirstPhase:
			this.lowAttackRanRange = 1;
			this.highAttackRanRange = 3;
			this.attackCooltimePA = 0.1f;
			this.maxHp = this.firstPhaseHp;
			return;
		case DragonController.Phase.SecondPhase:
			this.lowAttackRanRange = 1;
			this.highAttackRanRange = 3;
			this.attackCooltimePA = 0.1f;
			this.maxHp = this.secondPhaseHp;
			return;
		case DragonController.Phase.ThirdPhase:
			this.lowAttackRanRange = 1;
			this.highAttackRanRange = 3;
			this.attackCooltimePA = 0.1f;
			this.maxHp = this.thirdPhaseHp;
			return;
		case DragonController.Phase.BossPhase:
			this.attackCooltimePA = 0.1f;
			this.maxHp = this.bossHp;
			this.progressSlider.gameObject.SetActive(true);
			this.timerSlider.gameObject.SetActive(false);
			if (this.didntHoldBackCount >= 2)
			{
				if (this.currentHp <= this.bossHp && this.currentHp > this.bossHp * 0.75f)
				{
					this.lowAttackRanRange = 1;
					this.highAttackRanRange = 3;
				}
				if (this.currentHp <= this.bossHp * 0.75f && this.currentHp > this.bossHp * 0.5f)
				{
					this.lowerFireBreathObject.GetComponentInChildren<FireBreath>().fireBreathDamage = 35f;
					this.fireBreathObject.GetComponentInChildren<FireBreath>().fireBreathDamage = 35f;
					this.lowAttackRanRange = 3;
					this.highAttackRanRange = 6;
				}
				if (this.currentHp <= this.bossHp * 0.5f && this.currentHp > this.bossHp * 0.25f)
				{
					this.lowAttackRanRange = 0;
					this.highAttackRanRange = 6;
					this.dashCoolTime = 20f;
					this.highBreathCoolTime = 30f;
					this.lowerBreathCoolTime = 30f;
					this.rowFireAttackCoolTime = 60f;
					this.armAttackCoolTime = 50f;
					this.biteAttackCoolTime = 10f;
				}
				if (this.currentHp <= this.bossHp * 0.25f && this.currentHp > this.bossHp * 0.1f)
				{
					this.isHardMode = true;
				}
				if (this.currentHp <= this.bossHp * 0.1f)
				{
					this.attackCooltimePA = 0.3f;
					return;
				}
			}
			else
			{
				this.isHardMode = false;
				if (this.currentHp <= this.bossHp && this.currentHp > this.bossHp * 0.75f)
				{
					this.lowAttackRanRange = 3;
					this.highAttackRanRange = 5;
				}
				if (this.currentHp <= this.bossHp * 0.75f && this.currentHp > this.bossHp * 0.5f)
				{
					this.lowAttackRanRange = 1;
					this.highAttackRanRange = 5;
				}
				if (this.currentHp <= this.bossHp * 0.5f && this.currentHp > this.bossHp * 0.25f)
				{
					this.lowAttackRanRange = 0;
					this.highAttackRanRange = 5;
				}
				if (this.currentHp <= this.bossHp * 0.25f)
				{
					this.lowAttackRanRange = 0;
					this.highAttackRanRange = 6;
				}
			}
			return;
		default:
			return;
		}
	}

	// Token: 0x06000010 RID: 16 RVA: 0x00002D99 File Offset: 0x00000F99
	private void IsArmAttackingSetTrue()
	{
		this.targetPos = this.target.transform.position;
		this.offset = new Vector3(3f, 2.5f, 0f);
	}

	// Token: 0x06000011 RID: 17 RVA: 0x00002DCB File Offset: 0x00000FCB
	private IEnumerator RandomAttack()
	{
		yield return new WaitForSeconds(2f);
		if (!this.isAttacking && this.phase != DragonController.Phase.NonePhase && !this.isBossDead)
		{
			this.isAttacking = true;
			for (;;)
			{
				int num = Random.Range(this.lowAttackRanRange, this.highAttackRanRange);
				Debug.Log(num);
				switch (num)
				{
				case 0:
					if (this.armAttackCoolTime >= 20f * this.attackCooltimePA)
					{
						goto Block_5;
					}
					new WaitForSeconds(2f);
					continue;
				case 1:
					if (this.lowerBreathCoolTime >= 30f * this.attackCooltimePA)
					{
						goto Block_6;
					}
					new WaitForSeconds(2f);
					continue;
				case 2:
					if (this.highBreathCoolTime >= 30f * this.attackCooltimePA)
					{
						goto Block_7;
					}
					new WaitForSeconds(2f);
					continue;
				case 3:
					if (this.dashCoolTime >= 20f * this.attackCooltimePA)
					{
						goto Block_8;
					}
					new WaitForSeconds(2f);
					continue;
				case 4:
					if (this.biteAttackCoolTime >= 10f * this.attackCooltimePA)
					{
						goto Block_9;
					}
					new WaitForSeconds(2f);
					continue;
				case 5:
					if (this.rowFireAttackCoolTime >= 60f * this.attackCooltimePA)
					{
						goto Block_10;
					}
					new WaitForSeconds(2f);
					continue;
				case 6:
					goto IL_313;
				case 7:
					goto IL_33A;
				case 8:
					if (this.dashCoolTime >= 20f * this.attackCooltimePA)
					{
						goto Block_11;
					}
					new WaitForSeconds(2f);
					continue;
				}
				break;
			}
			goto IL_3B7;
			Block_5:
			this.isOriginalPos = false;
			this.offset = new Vector3(6f, 0f, 0f);
			this.targetPos = this.highFirePos.transform.position;
			this.anim.SetBool("IsArmAttacking", true);
			this.isArmAttacking = true;
			this.armAttackCoolTime = 0f;
			goto IL_3B7;
			Block_6:
			this.isOriginalPos = false;
			this.anim.SetBool("IsLowBreathing", true);
			this.isLowFiring = true;
			this.lowerBreathCoolTime = 0f;
			goto IL_3B7;
			Block_7:
			this.isOriginalPos = false;
			this.anim.SetBool("IsHighBreathing", true);
			this.isHighFiring = true;
			this.highBreathCoolTime = 0f;
			goto IL_3B7;
			Block_8:
			this.isOriginalPos = false;
			this.dashStartPos = this.dashBottomRight;
			this.dashEndPos = this.dashBottomLeft;
			this.isDashing = true;
			this.dashCoolTime = 0f;
			goto IL_3B7;
			Block_9:
			this.isOriginalPos = false;
			this.offset = new Vector3(10.6f, 2.6f, 0f);
			this.targetPos = this.target.transform.position;
			this.isBiting = true;
			this.anim.SetBool("IsBiting", true);
			this.biteAttackCoolTime = 0f;
			goto IL_3B7;
			Block_10:
			this.isOriginalPos = false;
			this.anim.SetBool("IsRowBreathing", true);
			this.rowBreathRotZOffset = 7;
			this.isHighFiring = true;
			this.rowFireAttackCoolTime = 0f;
			goto IL_3B7;
			IL_313:
			this.fireBallFirePosPrefab = this.attackPhaseGroundOneShotFireBallFirePos;
			this.rowBreathRotZOffset = -7;
			this.anim.SetBool("IsGroundOneShoting", true);
			goto IL_3B7;
			IL_33A:
			this.fireBallFirePosPrefab = this.fireBallFirePos;
			this.rowBreathRotZOffset = 7;
			this.anim.SetBool("IsOneShoting", true);
			goto IL_3B7;
			Block_11:
			this.isOriginalPos = false;
			this.dashStartPos = this.dashUpsideRight;
			this.dashEndPos = this.dashUpsideLeft;
			this.isDashing = true;
			this.dashCoolTime = 0f;
		}
		IL_3B7:
		yield break;
	}

	// Token: 0x06000012 RID: 18 RVA: 0x00002DDA File Offset: 0x00000FDA
	private void DashController()
	{
		if (this.readyToDash && this.isDashing)
		{
			this.dashTimer += Time.deltaTime;
			if (this.dashTimer >= 2f)
			{
				this.DashingToPlayer(this.dashEndPos);
			}
		}
	}

	// Token: 0x06000013 RID: 19 RVA: 0x00002E18 File Offset: 0x00001018
	private void DashingToPlayer(Transform _dashEndPos)
	{
		this.anim.SetBool("IsDashing", true);
		base.gameObject.transform.position = Vector3.Lerp(base.gameObject.transform.position, _dashEndPos.transform.position, 0.005f);
		if (Mathf.Abs(_dashEndPos.position.x - base.gameObject.transform.position.x) <= 0.3f && !this.isDashFinished)
		{
			this.readyToDash = false;
			this.isDashFinished = true;
			this.isDashing = false;
			this.dashTimer = 0f;
			this.dashDamaged = false;
			this.anim.SetBool("IsDashing", false);
		}
	}

	// Token: 0x06000014 RID: 20 RVA: 0x00002ED8 File Offset: 0x000010D8
	private void LowBreathAttack()
	{
		this.lowerFireBreathObject.SetActive(true);
	}

	// Token: 0x06000015 RID: 21 RVA: 0x00002EE6 File Offset: 0x000010E6
	private void HighBreathAttack()
	{
		this.fireBreathObject.SetActive(true);
	}

	// Token: 0x06000016 RID: 22 RVA: 0x00002EF4 File Offset: 0x000010F4
	private void AttackFinish()
	{
		this.fireBreathObject.GetComponentInChildren<FireBreath>().isFired = false;
		this.lowerFireBreathObject.GetComponentInChildren<FireBreath>().isFired = false;
		this.fireBreathObject.SetActive(false);
		this.lowerFireBreathObject.SetActive(false);
		this.biteAttack.enabled = false;
		this.armAttack.enabled = false;
		this.isArmAttacking = false;
		this.isBiting = false;
	}

	// Token: 0x06000017 RID: 23 RVA: 0x00002F64 File Offset: 0x00001164
	private void AttackAnimFinish()
	{
		this.anim.SetBool("IsHighBreathing", false);
		this.anim.SetBool("IsLowBreathing", false);
		this.anim.SetBool("IsBiting", false);
		this.anim.SetBool("IsArmAttacking", false);
		this.anim.SetBool("IsRowBreathing", false);
		this.anim.SetBool("IsOneShoting", false);
		this.anim.SetBool("IsGroundOneShoting", false);
		this.isBited = false;
		this.isArmAttacked = false;
		this.isHighFiring = false;
		this.isLowFiring = false;
		this.isAttacking = false;
	}

	// Token: 0x06000018 RID: 24 RVA: 0x0000300C File Offset: 0x0000120C
	private void RowBreath()
	{
		Vector2 vector = this.target.transform.position - base.transform.position;
		float num = Mathf.Atan2(vector.y, vector.x) * 57.29578f;
		this.fireBallFirePosPrefab.transform.rotation = Quaternion.Euler(0f, 0f, num + (float)this.rowBreathRotZOffset);
		this.rowFireBallPrefab = Object.Instantiate<GameObject>(this.fireBall, this.fireBallFirePosPrefab.position, this.fireBallFirePosPrefab.rotation);
		this.rowFireBallPrefab.transform.SetParent(base.transform);
	}

	// Token: 0x06000019 RID: 25 RVA: 0x000030BC File Offset: 0x000012BC
	private void DashToPos()
	{
		if (this.isBiting || this.isArmAttacking)
		{
			base.gameObject.transform.position = Vector3.Lerp(base.gameObject.transform.position, this.targetPos + this.offset, 5f * Time.deltaTime);
		}
		if (this.isHighFiring)
		{
			base.gameObject.transform.position = Vector3.Lerp(base.gameObject.transform.position, this.highFirePos.transform.position, 5f * Time.deltaTime);
		}
		if (this.isLowFiring)
		{
			base.gameObject.transform.position = Vector3.Lerp(base.gameObject.transform.position, this.lowFirePos.transform.position, 5f * Time.deltaTime);
		}
		if (this.isDashing && this.dashTimer < 2f)
		{
			base.gameObject.transform.position = Vector3.MoveTowards(base.gameObject.transform.position, this.dashStartPos.position, 14f * Time.deltaTime);
			if (base.gameObject.transform.position == this.dashStartPos.position)
			{
				this.readyToDash = true;
			}
		}
	}

	// Token: 0x0600001A RID: 26 RVA: 0x00003223 File Offset: 0x00001423
	private void ArmAttack()
	{
		this.armAttack.enabled = true;
	}

	// Token: 0x0600001B RID: 27 RVA: 0x00003231 File Offset: 0x00001431
	private void Bite()
	{
		this.biteAttack.enabled = true;
	}

	// Token: 0x0600001C RID: 28 RVA: 0x00003240 File Offset: 0x00001440
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			if (this.isDashing && !this.dashDamaged)
			{
				this.dashDamaged = true;
				collision.GetComponent<Player>().PlayerTakeDamage(20f);
			}
			if (this.isBiting && !this.isBited)
			{
				this.isBited = true;
				collision.GetComponent<Player>().PlayerTakeDamage(10f);
			}
			if (this.isArmAttacking && !this.isArmAttacked)
			{
				this.isArmAttacked = true;
				collision.GetComponent<Player>().PlayerTakeDamage(25f);
			}
		}
		if (collision.CompareTag("Skill"))
		{
			if (GameObject.Find("Player").GetComponentInChildren<WeaponCtrl>().isSkillAttack)
			{
				this.currentHp -= collision.gameObject.GetComponent<WeaponSkill>().damage;
				GameObject.Find("Player").GetComponentInChildren<WeaponCtrl>().isSkillAttack = false;
			}
			if (GameObject.Find("Player").GetComponentInChildren<WeaponCtrl>().isFlyAttack)
			{
				this.currentHp -= collision.gameObject.GetComponent<WeaponSkill>().damage;
				GameObject.Find("Player").GetComponentInChildren<WeaponCtrl>().isFlyAttack = false;
				Object.Destroy(collision.gameObject);
			}
		}
	}

	// Token: 0x0600001D RID: 29 RVA: 0x00003380 File Offset: 0x00001580
	public virtual void BossTakeDamage(int damage)
	{
		if (this.phase != DragonController.Phase.NonePhase && this.phase != DragonController.Phase.First30SecPhase)
		{
			this.currentHp -= (float)damage;
			if (this.currentHp <= 0f && this.phase != DragonController.Phase.BossPhase && !this.didHoldBack)
			{
				this.currentHp = 0f;
				this.didHoldBack = true;
				this.didntHoldBackCount--;
			}
			if (this.currentHp <= 0f && this.phase == DragonController.Phase.BossPhase && !this.isBossDead)
			{
				this.currentHp = 0f;
				this.isBossDead = true;
			}
		}
	}

	// Token: 0x04000001 RID: 1
	[SerializeField]
	private Transform firePos;

	// Token: 0x04000002 RID: 2
	[SerializeField]
	private GameObject fireBreath;

	// Token: 0x04000003 RID: 3
	[SerializeField]
	private Transform lowerFirePos;

	// Token: 0x04000004 RID: 4
	[SerializeField]
	private GameObject lowerFireBreath;

	// Token: 0x04000005 RID: 5
	[SerializeField]
	private Transform highFirePos;

	// Token: 0x04000006 RID: 6
	[SerializeField]
	private Transform lowFirePos;

	// Token: 0x04000007 RID: 7
	[SerializeField]
	private GameObject fireBall;

	// Token: 0x04000008 RID: 8
	[SerializeField]
	private Transform fireBallFirePos;

	// Token: 0x04000009 RID: 9
	[SerializeField]
	private Transform nonePhaseFireBallFirePos;

	// Token: 0x0400000A RID: 10
	[SerializeField]
	private Transform attackPhaseGroundOneShotFireBallFirePos;

	// Token: 0x0400000B RID: 11
	[SerializeField]
	private Transform dashUpsideLeft;

	// Token: 0x0400000C RID: 12
	[SerializeField]
	private Transform dashUpsideRight;

	// Token: 0x0400000D RID: 13
	[SerializeField]
	private Transform dashBottomLeft;

	// Token: 0x0400000E RID: 14
	[SerializeField]
	private Transform dashBottomRight;

	// Token: 0x0400000F RID: 15
	[SerializeField]
	private Transform originalPos;

	// Token: 0x04000010 RID: 16
	[SerializeField]
	private Transform first30SecPos;

	// Token: 0x04000011 RID: 17
	[SerializeField]
	private Transform nonePhasePos;

	// Token: 0x04000012 RID: 18
	[SerializeField]
	private Transform goingToNonePhasePos;

	// Token: 0x04000013 RID: 19
	[SerializeField]
	private Transform goingToOriginalPos;

	// Token: 0x04000014 RID: 20
	[SerializeField]
	private BoxCollider2D biteAttack;

	// Token: 0x04000015 RID: 21
	[SerializeField]
	private BoxCollider2D armAttack;

	// Token: 0x04000016 RID: 22
	[SerializeField]
	private GameObject landEffect;

	// Token: 0x04000017 RID: 23
	[SerializeField]
	private Transform landEffectPos;

	// Token: 0x04000018 RID: 24
	[SerializeField]
	private Animator anim;

	// Token: 0x04000019 RID: 25
	[SerializeField]
	private SpriteRenderer spriteRenderer;

	// Token: 0x0400001A RID: 26
	[SerializeField]
	private Slider hpSlider;

	// Token: 0x0400001B RID: 27
	[SerializeField]
	private Slider timerSlider;

	// Token: 0x0400001C RID: 28
	[SerializeField]
	private Slider progressSlider;

	// Token: 0x0400001D RID: 29
	private Transform target;

	// Token: 0x0400001E RID: 30
	private Transform dashStartPos;

	// Token: 0x0400001F RID: 31
	private Transform dashEndPos;

	// Token: 0x04000020 RID: 32
	private Transform fireBallFirePosPrefab;

	// Token: 0x04000021 RID: 33
	private GameObject fireBreathObject;

	// Token: 0x04000022 RID: 34
	private GameObject lowerFireBreathObject;

	// Token: 0x04000023 RID: 35
	private GameObject rowFireBallPrefab;

	// Token: 0x04000024 RID: 36
	private GameObject landEffectPrefab;

	// Token: 0x04000025 RID: 37
	[SerializeField]
	private AudioClip bossBGM;

	// Token: 0x04000026 RID: 38
	private Vector3 offset;

	// Token: 0x04000027 RID: 39
	private Vector3 targetPos = new Vector3(0f, 0f, 0f);

	// Token: 0x04000028 RID: 40
	private float dashTimer;

	// Token: 0x04000029 RID: 41
	private float phaseChangeTimer = 30f;

	// Token: 0x0400002A RID: 42
	private float dashCoolTime = 20f;

	// Token: 0x0400002B RID: 43
	private float highBreathCoolTime = 30f;

	// Token: 0x0400002C RID: 44
	private float lowerBreathCoolTime = 30f;

	// Token: 0x0400002D RID: 45
	private float rowFireAttackCoolTime = 60f;

	// Token: 0x0400002E RID: 46
	private float armAttackCoolTime = 55f;

	// Token: 0x0400002F RID: 47
	private float tailAttackCoolTime = 10f;

	// Token: 0x04000030 RID: 48
	private float attackCooltimePA;

	// Token: 0x04000031 RID: 49
	private float biteAttackCoolTime = 10f;

	// Token: 0x04000032 RID: 50
	private bool isAttacking;

	// Token: 0x04000033 RID: 51
	private bool isDashing;

	// Token: 0x04000034 RID: 52
	private bool readyToDash;

	// Token: 0x04000035 RID: 53
	private bool dashDamaged;

	// Token: 0x04000036 RID: 54
	private bool isDashFinished;

	// Token: 0x04000037 RID: 55
	private bool isBreakingIn;

	// Token: 0x04000038 RID: 56
	private bool isOriginalPos;

	// Token: 0x04000039 RID: 57
	private int didntHoldBackCount = 3;

	// Token: 0x0400003A RID: 58
	private int highAttackRanRange;

	// Token: 0x0400003B RID: 59
	private int lowAttackRanRange;

	// Token: 0x0400003C RID: 60
	private int phaseChanged;

	// Token: 0x0400003D RID: 61
	private int rowBreathRotZOffset;

	// Token: 0x0400003E RID: 62
	private float firstPhaseHp = 200f;

	// Token: 0x0400003F RID: 63
	private float secondPhaseHp = 1000f;

	// Token: 0x04000040 RID: 64
	private float thirdPhaseHp = 1500f;

	// Token: 0x04000041 RID: 65
	private float bossHp = 3000f;

	// Token: 0x04000042 RID: 66
	private float currentHp = 1f;

	// Token: 0x04000043 RID: 67
	private float maxHp = 1f;

	// Token: 0x04000044 RID: 68
	private bool isPhaseChangedWhenNonePhase;

	// Token: 0x04000045 RID: 69
	private bool isPhaseChanged;

	// Token: 0x04000046 RID: 70
	private bool isOutOfCamera;

	// Token: 0x04000047 RID: 71
	private bool didHoldBack;

	// Token: 0x04000048 RID: 72
	private bool isBossDead;

	// Token: 0x04000049 RID: 73
	private bool isBiting;

	// Token: 0x0400004A RID: 74
	private bool isArmAttacking;

	// Token: 0x0400004B RID: 75
	private bool isBited;

	// Token: 0x0400004C RID: 76
	private bool isArmAttacked;

	// Token: 0x0400004D RID: 77
	private bool isHighFiring;

	// Token: 0x0400004E RID: 78
	private bool isLowFiring;

	// Token: 0x0400004F RID: 79
	private bool isNonePhasePos;

	// Token: 0x04000050 RID: 80
	public bool isHardMode;

	// Token: 0x04000051 RID: 81
	private DragonController.Phase phase;

	// Token: 0x04000052 RID: 82
	private bool isBossDeading;

	// Token: 0x02000047 RID: 71
	private enum Phase
	{
		// Token: 0x0400020E RID: 526
		First30SecPhase,
		// Token: 0x0400020F RID: 527
		NonePhase,
		// Token: 0x04000210 RID: 528
		FirstPhase,
		// Token: 0x04000211 RID: 529
		SecondPhase,
		// Token: 0x04000212 RID: 530
		ThirdPhase,
		// Token: 0x04000213 RID: 531
		BossPhase
	}
}
