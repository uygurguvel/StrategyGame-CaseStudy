using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Buildings/ProducterBuildingScriptable")]
public class ProducterBuildingInfo : BuildingInfoScriptable
{
	[SerializeField] private ProductInfoScriptable[] products;


	public ProductInfoScriptable[] Products { get => products; }

}
