using GameExt;
using UnityEngine;

public interface ISwitcablePanel
{
	SwitchButton SwitchButton { get; set; }

	bool IsClose { get; set; }

	void OpenPanel();
	void ClosePanel();

}

public interface IGridObject
{
	bool IsFit { get; set; }
	Vector3 Pivot { get; }
	Vector3 PivotOffset { get; set; }
	Vector2Int ObjectSize { get; set; }
}

public interface IProducter
{
	Transform SpawnPoint { get; }

	void UnitCalled(ProductInfoScriptable productInfo);
}

public interface IDamageAble
{
	Transform Transform { get; }
	HealthSystem HealthSystem { get; set; }

	void OnDeath();
}
