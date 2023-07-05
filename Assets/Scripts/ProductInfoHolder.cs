using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ProductInfoHolder : MonoBehaviour, IPointerClickHandler
{
	[SerializeField] private Image m_Image;

	private RectTransform rectTransform;

	private ProductInfoScriptable productInfo;

	private AspectRatioFitter aspectRatioFitter;
	private SpriteSetter spriteSetter;

	private int productIndex;

	public int ProductIndex { get => productIndex; }

	public Vector2 GetAnchoredPosition() => rectTransform.anchoredPosition;

	private void Awake()
	{
		rectTransform = transform as RectTransform;
		aspectRatioFitter = GetComponentInChildren<AspectRatioFitter>();

		spriteSetter = new SpriteSetter(m_Image);
	}

	public void Init(ProductInfoScriptable productInfo, int productIndex)
	{
		this.productInfo = productInfo;
		this.productIndex = productIndex;

		spriteSetter.SetSprite(productInfo.MenuSprite, aspectRatioFitter);
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

	public void OnPointerClick(PointerEventData eventData)
	{
		ActionManager.ProductSelected?.Invoke(productInfo);
	}

}
