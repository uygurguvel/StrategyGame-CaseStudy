using UnityEngine;
using UnityEngine.EventSystems;

public class UIProduct : UIInfoHolderBase, IBeginDragHandler, IDragHandler
{
	public Vector2 GetAnchoredPosition() => rectTransform.anchoredPosition;

	public void SetAnchoredPos(Vector2 pos)
	{
		rectTransform.anchoredPosition = pos;
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		ActionManager.OnDragBegin?.Invoke(productInfo);
	}

	/// <summary>
	/// I didn't want to leave an empty interface(), but the OnBeginDrag function doesn't work without it.
	/// I could have used IPointer, but I didn't want the scroll content while dragging.
	/// </summary>
	public void OnDrag(PointerEventData eventData)
	{

	}

}
