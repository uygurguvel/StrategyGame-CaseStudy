using UnityEngine;

public class Barracks : BuildingBase, IProducter
{
	[SerializeField] private Transform spawnPoint;
	public Transform SpawnPoint { get => spawnPoint; }

	public void UnitCalled(ProductInfoScriptable productInfo)
	{
		SoldierBase soldier = ActionManager.GetItemFromPool(productInfo.PoolType, spawnPoint.position, null).GetComponent<SoldierBase>();
	}
}
