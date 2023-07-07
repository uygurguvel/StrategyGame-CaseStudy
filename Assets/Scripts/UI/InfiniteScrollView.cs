using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using GameExt;

public class InfiniteScrollView : MonoBehaviour, IBeginDragHandler, IDragHandler
{
	private ScrollContent scrollContent;
	private ScrollRect scrollRect;

	private UIProduct edgeItem;
	private UIProduct lastEdgeItem;

	private EdgeType edgeType;

	private Vector2 startDragPos;
	private Vector2 currentDragPos;

	private bool isPositiveScroll;

	private void Awake()
	{
		scrollContent = GetComponentInChildren<ScrollContent>();
		scrollRect = GetComponent<ScrollRect>();

		scrollRect.onValueChanged.AddListener(OnScroll);
	}
	public void OnBeginDrag(PointerEventData eventData)
	{
		startDragPos = eventData.position;
	}

	public void OnDrag(PointerEventData eventData)
	{
		currentDragPos = eventData.position;
	}

	public void OnScroll(Vector2 eventData)
	{
		isPositiveScroll = startDragPos.y < currentDragPos.y;

		HandleVerticalScroll();
	}

	private void HandleVerticalScroll()
	{
		edgeType = isPositiveScroll ? EdgeType.Down : EdgeType.Top;
		edgeItem = scrollContent.GetEdgeItem(edgeType);

		if (scrollContent.IsLimitReached(edgeItem, edgeType))
		{
			EdgeType targetEdge = !isPositiveScroll ? EdgeType.Down : EdgeType.Top;
			scrollContent.InsertItem(targetEdge, edgeType);
		}
	}

}
