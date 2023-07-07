public class BuildingBase : GridObjectBase
{
	protected override void OnMouseDown()
	{
		ActionManager.OpenInformationPanel?.Invoke(productInfo, this as IProducter);
	}
}
