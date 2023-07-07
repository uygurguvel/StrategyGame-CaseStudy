using GameExt;
using Pathfinding;
using System.Collections;
using UnityEngine;

public class SoldierBase : GridObjectBase
{
	[SerializeField] private GameObject outline;

	private AIDestinationSetter aiDestinationSetter;
	private IAstarAI aStar;

	private ShootSystem shootSystem;

	private GridObjectBase target;

	private Transform targetPoint;

	private Coroutine movementCoroutine;

	protected override void Awake()
	{
		base.Awake();

		shootSystem = GetComponent<ShootSystem>();
		shootSystem.Init(this, productInfo as SoldiersInfoScriptable);

		aiDestinationSetter = GetComponent<AIDestinationSetter>();
		aStar = GetComponent<IAstarAI>();
	}

	protected override void OnMouseDown()
	{
		ActionManager.UnitSelected?.Invoke(this);
	}

	public void SetTarget(Vector3 targetPos, GridObjectBase target)
	{
		shootSystem.StopShooting();
		StopMovementCoroutine();

		bool isAttacking = false;

		if (target != null && target != this)
		{
			this.target = target;
			isAttacking = true;
		}

		targetPoint = ActionManager.GetItemFromPool(PoolType.TARGET_POINT, targetPos, null).transform;

		aiDestinationSetter.target = targetPoint;
		movementCoroutine = StartCoroutine(IEMovement(isAttacking));
	}

	private IEnumerator IEMovement(bool isAttack)
	{
		WaitForFixedUpdate wait = new WaitForFixedUpdate();

		float movementThreshold = 0.25f;
		float distThreshold = isAttack ? (productInfo as SoldiersInfoScriptable).FireRange : movementThreshold;
		float dist;

		aStar.isStopped = false;

		while (true)
		{
			dist = (targetPoint.transform.position - transform.position).magnitude;

			if (dist <= distThreshold)
			{
				StopMovementCoroutine();

				if (isAttack)
					shootSystem.StartShooting(target);
			}

			yield return wait;
		}
	}

	private void StopMovementCoroutine()
	{
		if (movementCoroutine != null)
		{
			StopCoroutine(movementCoroutine);
			movementCoroutine = null;

			ActionManager.ReturnItemToPool(targetPoint.gameObject, PoolType.TARGET_POINT);
		}

		aiDestinationSetter.target = null;
		aStar.isStopped = true;
	}

	public override void OnDeath()
	{
		SetOutline(false);
		ActionManager.OnUnitRemove?.Invoke(this);
		StopMovementCoroutine();


		base.OnDeath();
	}

	public void UnitSelected()
	{
		SetOutline(true);
	}
	public void UnitChanged()
	{
		SetOutline(false);
	}
	private void SetOutline(bool isActive)
	{
		outline.SetActive(isActive);
	}
}
