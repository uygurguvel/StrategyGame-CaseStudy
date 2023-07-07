using GameExt;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour, IPooledObject
{
	[SerializeField] private float bulletSpeed;

	private Coroutine shootCoroutine;

	private IDamageAble target;

	private void Awake()
	{
		ActionManager.OnObjectDeath += OnTargetDeath;
	}

	public void Shoot(int attackDamage, IDamageAble target)
	{
		this.target = target;
		shootCoroutine = StartCoroutine(IEShoot(attackDamage));

	}

	private IEnumerator IEShoot(int attackDamage)
	{
		WaitForFixedUpdate wait = new WaitForFixedUpdate();

		Vector3 targetPos = target.Transform.position;
		transform.LookRotation(targetPos);

		float distThreshold = 0.1f;
		float dist = Mathf.Infinity;

		while (dist > distThreshold)
		{
			dist = (transform.position - targetPos).magnitude;
			transform.position = Vector2.MoveTowards(transform.position, targetPos, Time.fixedDeltaTime * bulletSpeed);

			yield return wait;
		}

		if (target != null)
			target.HealthSystem.GetDamage(attackDamage);

		ActionManager.ReturnItemToPool(gameObject, PoolType.BULLET);
	}

	private void OnTargetDeath(IDamageAble obj)
	{
		if (obj == target)
		{
			target = null;

			StopCoroutine(shootCoroutine);
			shootCoroutine = null;

			ActionManager.ReturnItemToPool(gameObject, PoolType.BULLET);
		}
	}

	public void OnObjectGetFromPool()
	{

	}

	public void OnObjectInstantiate()
	{

	}

	public void OnObjectReturnToPool()
	{

	}
}
