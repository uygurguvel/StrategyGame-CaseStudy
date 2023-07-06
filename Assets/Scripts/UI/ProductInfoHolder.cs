using UnityEngine;
using UnityEngine.UI;
using GameExt;
using UnityEngine.EventSystems;

public class ProductInfoHolder : MonoBehaviour, IBeginDragHandler, IDragHandler
{
	[SerializeField] private Image m_Image;

	private RectTransform rectTransform;

	private ProductInfoScriptable productInfo;

	private AspectRatioFitter aspectRatioFitter;

	private int productIndex;

	public int ProductIndex { get => productIndex; }

	public Vector2 GetAnchoredPosition() => rectTransform.anchoredPosition;

	private void Awake()
	{
		rectTransform = transform as RectTransform;
		aspectRatioFitter = GetComponentInChildren<AspectRatioFitter>();
	}

	public void Init(ProductInfoScriptable productInfo, int productIndex)
	{
		this.productInfo = productInfo;
		this.productIndex = productIndex;

		m_Image.SetMenuSprite(productInfo.MenuSprite, aspectRatioFitter);
	}

	public void SetAnchoredPos(Vector2 pos)
	{
		rectTransform.anchoredPosition = pos;
	}

	public void SetSize(Vector2 itemSize)
	{
		transform.localScale = Vector3.one;
		rectTransform.sizeDelta = itemSize;
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		ActionManager.OnDragBegin?.Invoke(productInfo);
	}

	/// <summary>
	/// I didn't want to leave an empty interface, but the OnBeginDrag function doesn't work without it.
	/// I could have used IPointer, but I didn't want the scroll content while dragging.
	/// </summary>
	public void OnDrag(PointerEventData eventData)
	{

	}
}
