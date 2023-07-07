using GameExt;
using System.Collections;
using UnityEngine;

public class ShootSystem : MonoBehaviour
{
	private SoldierBase soldier;
	private SoldiersInfoScriptable soldierInfo;

	private Coroutine attackCoroutine;

	private IDamageAble target;

	private void Awake()
	{
		ActionManager.OnObjectDeath += OnTargetDeath;
	}

	public void Init(SoldierBase soldier, SoldiersInfoScriptable soldierInfo)
	{
		this.soldier = soldier;
		this.soldierInfo = soldierInfo;
	}

	public void StartShooting(IDamageAble target)
	{
		this.target = target;

		StopAttackCoroutine();
		attackCoroutine = StartCoroutine(IEAttack());
	}

	private void OnTargetDeath(IDamageAble damageableObject)
	{
		if (target == damageableObject)
		{
			StopShooting();
			target = null;
		}
	}

	public void StopShooting()
	{
		StopAttackCoroutine();
	}

	private IEnumerator IEAttack()
	{
		WaitForFixedUpdate wait = new WaitForFixedUpdate();

		float timer = 0;

		while (true)
		{
			timer += Time.fixedDeltaTime;

			if (timer >= soldierInfo.FireRate)
			{
				Shoot();
				timer = 0;
			}

			transform.LookRotation(target.Transform.position);

			yield return wait;
		}
	}

	private void Shoot()
	{
		Bullet bullet = ActionManager.GetItemFromPool(PoolType.BULLET, transform.position, null).GetComponent<Bullet>();
		bullet.Shoot(soldierInfo.AttackDamage, target);
	}

	private void StopAttackCoroutine()
	{
		if (attackCoroutine != null)
		{
			StopCoroutine(attackCoroutine);
			attackCoroutine = null;
		}
	}

}
