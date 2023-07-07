using GameExt;
using Pathfinding;
using UnityEngine;

public class BuildingBase : GridObjectBase, IDamageAble
{
	[SerializeField] private DynamicGridObstacle dynamicObstacle;
	protected override void Awake()
	{
		base.Awake();

		dynamicObstacle = GetComponent<DynamicGridObstacle>();
	}

	protected override void Insert()
	{
		base.Insert();
		dynamicObstacle.DoUpdateGraphs();
	}

	protected override void OnMouseDown()
	{
		ActionManager.OpenInformationPanel?.Invoke(productInfo, this as IProducter);
	}

	public override void OnDeath()
	{
		base.OnDeath();
		ActionManager.InfoObjectRemoved?.Invoke(this as IProducter);
	}
}
