public class SoldierBase : GridObjectBase
{
	protected override void OnMouseDown()
	{
		ActionManager.UnitSelected?.Invoke(this);
	}
}
