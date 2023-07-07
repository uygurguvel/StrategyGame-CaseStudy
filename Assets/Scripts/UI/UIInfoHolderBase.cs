using GameExt;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UIInfoHolderBase : MonoBehaviour
{
	[SerializeField] protected Image m_Image;

	protected ProductInfoScriptable productInfo;

	protected RectTransform rectTransform;
	protected AspectRatioFitter aspectRatioFitter;

	protected int productIndex;

	public int ProductIndex { get => productIndex; }

	protected virtual void Awake()
	{
		rectTransform = transform as RectTransform;
		aspectRatioFitter = GetComponentInChildren<AspectRatioFitter>();
	}

	public virtual void Init(ProductInfoScriptable productInfo, int productIndex = 0)
	{
		this.productIndex = productIndex;
		this.productInfo = productInfo;

		transform.localScale = Vector3.one;

		m_Image.SetMenuSprite(productInfo.MenuSprite, aspectRatioFitter);
	}
	
	public void SetSize(Vector2 itemSize)
	{
		rectTransform.sizeDelta = itemSize;
	}
}
