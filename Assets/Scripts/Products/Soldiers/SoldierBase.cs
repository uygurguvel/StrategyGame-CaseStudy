using GameExt;
using Pathfinding;
using System.Collections;
using UnityEngine;

public class SoldierBase : GridObjectBase
{
	[SerializeField] private GameObject outline;

	private AIDestinationSetter aiDestinationSetter;
	private IAstarAI aStar;

	private GridObjectBase target;

	private Transform targetPoint;

	private Coroutine movementCoroutine;
	private Coroutine attackCoroutine;

	private void Awake()
	{
		aiDestinationSetter = GetComponent<AIDestinationSetter>();
		aStar = GetComponent<IAstarAI>();
	}

	protected override void OnMouseDown()
	{
		ActionManager.UnitSelected?.Invoke(this);
	}

	public void SetTarget(Vector3 targetPoint, GridObjectBase target)
	{
		StopMovementCoroutine();
		StopAttackCoroutine();

		bool isAttacking = false;

		if (target != null)
		{
			this.target = target;
			this.targetPoint = this.target.transform;

			isAttacking = true;
		}
		else
		{
			this.targetPoint = ActionManager.GetItemFromPool(PoolType.TARGET_POINT, targetPoint, null).transform;
		}


		aiDestinationSetter.target = this.targetPoint;
		movementCoroutine = StartCoroutine(IEMovement(isAttacking));

	}

	private IEnumerator IEMovement(bool isAttack)
	{
		WaitForFixedUpdate wait = new WaitForFixedUpdate();

		float attackRange = 3f;
		float movementThreshold = isAttack ? attackRange : 0.25f;
		float dist;

		aStar.isStopped = false;

		while (true)
		{
			dist = (targetPoint.transform.position - transform.position).magnitude;

			if (dist <= movementThreshold)
			{
				if (isAttack)
					attackCoroutine = StartCoroutine(IEAttack());

				StopMovementCoroutine();
			}

			yield return wait;
		}
	}

	private IEnumerator IEAttack()
	{
		WaitForFixedUpdate wait = new WaitForFixedUpdate();

		while (true)
		{

			yield return wait;
		}
	}

	private void StopMovementCoroutine()
	{
		if (movementCoroutine != null)
		{
			StopCoroutine(movementCoroutine);
			movementCoroutine = null;

		}

		aiDestinationSetter.target = null;
		aStar.isStopped = true;

	}
	private void StopAttackCoroutine()
	{
		if (attackCoroutine != null)
		{
			StopCoroutine(attackCoroutine);
			attackCoroutine = null;
		}
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
