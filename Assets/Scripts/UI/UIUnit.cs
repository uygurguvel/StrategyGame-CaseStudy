using System;
using UnityEngine.EventSystems;

public class UIUnit : UIInfoHolderBase, IPointerClickHandler, IPooledObject
{
	private IProducter producter;

	public void OnPointerClick(PointerEventData eventData)
	{
		producter.UnitCalled(productInfo);
	}

	public void SetProducter(IProducter producter)
	{
		this.producter = producter;
	}

	public void OnObjectReturnToPool()
	{
		producter = null;
	}

	public void OnObjectGetFromPool() { }

	public void OnObjectInstantiate() { }


}
