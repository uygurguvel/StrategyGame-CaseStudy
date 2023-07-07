using Pathfinding;
using UnityEngine;

public class BuildingBase : GridObjectBase
{
	[SerializeField] private DynamicGridObstacle dynamicObstacle;

	private void Awake()
	{
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
}
