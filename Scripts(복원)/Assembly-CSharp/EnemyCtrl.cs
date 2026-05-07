using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000010 RID: 16
public class EnemyCtrl : MonoBehaviour
{
	// Token: 0x06000061 RID: 97 RVA: 0x000047C3 File Offset: 0x000029C3
	private void Start()
	{
		this.player = GameObject.FindGameObjectWithTag("Player").transform;
		this.circle = base.GetComponent<CircleCollider2D>();
		this.enemyHp = this.enemyMaxHp;
	}

	// Token: 0x06000062 RID: 98 RVA: 0x000047F2 File Offset: 0x000029F2
	protected void Update()
	{
		this.DistanceToPlayer();
	}

	// Token: 0x06000063 RID: 99 RVA: 0x000047FA File Offset: 0x000029FA
	private IEnumerator FireBallRoutine()
	{
		this.isFiring = true;
		while (this.fireBallCount < this.maxCount)
		{
			Object.Instantiate<GameObject>(this.fireBall, this.firePos.position, Quaternion.identity);
			this.fireBallCount++;
			yield return new WaitForSeconds(0.5f);
		}
		this.isFiring = false;
		yield break;
	}

	// Token: 0x06000064 RID: 100 RVA: 0x00004809 File Offset: 0x00002A09
	private void DistanceToPlayer()
	{
		if (Vector3.Distance(base.transform.position, this.player.position) <= this.mobRange && !this.isFiring)
		{
			base.StartCoroutine(this.FireBallRoutine());
		}
	}

	// Token: 0x06000065 RID: 101 RVA: 0x00004843 File Offset: 0x00002A43
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(base.transform.position, this.mobRange);
	}

	// Token: 0x06000066 RID: 102 RVA: 0x00004868 File Offset: 0x00002A68
	private void EnemyDie()
	{
		if (this.enemyHp <= 0f)
		{
			this.enemyHp = 0f;
			if (GameManager.instance.isFirstWeapon)
			{
				GameManager.instance.firstWeaponEnemyDeadCount++;
			}
			else if (!GameManager.instance.isFirstWeapon)
			{
				GameManager.instance.secondWeaponEnemyDeadCount++;
			}
			Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06000067 RID: 103 RVA: 0x000048D8 File Offset: 0x00002AD8
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Attack"))
		{
			this.enemyHp -= (float)collision.gameObject.GetComponent<WeaponEffect>().damage;
			this.RandomDrop();
		}
		if (collision.CompareTag("Skill"))
		{
			this.enemyHp -= collision.gameObject.GetComponent<WeaponSkill>().damage;
			this.RandomDrop();
		}
	}

	// Token: 0x06000068 RID: 104 RVA: 0x00004948 File Offset: 0x00002B48
	private void RandomDrop()
	{
		int num = Random.Range(0, 100);
		int num2 = Random.Range(0, this.weaponPrefab.Length);
		if (num >= 20 && num <= 100)
		{
			this.EnemyDie();
			return;
		}
		if (num >= 0 && num <= 20)
		{
			Object.Instantiate<GameObject>(this.weaponPrefab[num2], base.transform.position, Quaternion.identity);
			this.EnemyDie();
		}
	}

	// Token: 0x040000B2 RID: 178
	public Transform firePos;

	// Token: 0x040000B3 RID: 179
	public Transform itemSpawn;

	// Token: 0x040000B4 RID: 180
	public GameObject fireBall;

	// Token: 0x040000B5 RID: 181
	private Transform player;

	// Token: 0x040000B6 RID: 182
	private CircleCollider2D circle;

	// Token: 0x040000B7 RID: 183
	public float mobRange = 5f;

	// Token: 0x040000B8 RID: 184
	private int fireBallCount;

	// Token: 0x040000B9 RID: 185
	private int maxCount = 3;

	// Token: 0x040000BA RID: 186
	private bool isFiring;

	// Token: 0x040000BB RID: 187
	public float enemyMaxHp;

	// Token: 0x040000BC RID: 188
	[HideInInspector]
	public float enemyHp;

	// Token: 0x040000BD RID: 189
	[Header("Weapon")]
	public GameObject[] weaponPrefab;
}
