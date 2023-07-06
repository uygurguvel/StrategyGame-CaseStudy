using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Buildings/DefaultBuildingScriptable")]
public class BuildingInfoScriptable : ProductInfoScriptable
{
	private string description;


	protected string Description { get => description; }
}
