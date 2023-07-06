using UnityEngine;

public class ProductionMenuPanel : CanvasPanelBase
{
	private ProductInfoScriptable[] activeProducts;
	private ScrollContent scrollContent;

	protected override void Awake()
	{
		base.Awake();
		scrollContent = GetComponentInChildren<ScrollContent>();
	}


	private void Start()
	{
		activeProducts = Resources.LoadAll<BuildingInfoScriptable>("ActiveProducts/Buildings");
		scrollContent.Init(activeProducts);
	}

}
