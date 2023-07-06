using GameExt;
using System.Collections.Generic;
using UnityEngine;

public class ScrollContent : MonoBehaviour
{
	[SerializeField] private float itemOffset;
	[SerializeField] private Vector2 itemSize;

	private RectTransform rectTransform;

	private List<ProductInfoHolder> productHolders;
	private ProductInfoScriptable[] activeProducts;

	private Vector2 minMaxYBorder;

	private float itemYBound;

	private int visibleItemCount;

	public void Awake()
	{
		rectTransform = GetComponent<RectTransform>();
	}

	public void Init(ProductInfoScriptable[] activeProducts)
	{
		itemYBound = itemOffset + itemSize.y;
		visibleItemCount = GetVisibleItemAmount();

		this.activeProducts = activeProducts;

		CreateProductHolders();
		SetItemPositions();
		SetMinMaxBorder();
	}

	/// <summary>
	/// We are creating holders, based on the maximum number of items that can be displayed on the screen.
	/// </summary>
	private void CreateProductHolders()
	{
		productHolders = new List<ProductInfoHolder>();

		for (int i = 0; i < visibleItemCount; i++)
		{
			ProductInfoHolder infoHolder = ActionManager.GetItemFromPool(PoolType.UI_PRODUCT_HOLDER, Vector3.zero, transform).GetComponent<ProductInfoHolder>();
			productHolders.Add(infoHolder);

			int productIndex = i % activeProducts.Length;
			infoHolder.Init(activeProducts[productIndex], productIndex);
		}
	}

	private void SetItemPositions()
	{
		Vector2 startAnchor = Vector2.up * (rectTransform.rect.height - itemSize.y) * 0.5f;

		for (int i = 0; i < productHolders.Count; i++)
		{
			productHolders[i].SetSize(itemSize);
			productHolders[i].SetAnchoredPos(startAnchor - Vector2.up * (i * itemYBound));
		}
	}

	private void SetMinMaxBorder()
	{
		float maxBorder = productHolders[0].transform.position.y;
		float minBorder = productHolders[productHolders.Count - 2].transform.position.y;
		
		minMaxYBorder = new Vector2(minBorder, maxBorder);
	}

	/// <summary>
	/// This function calculates the maximum number of items that can be displayed simultaneously on the screen.
	/// </summary>
	public int GetVisibleItemAmount()
	{
		return Mathf.CeilToInt(rectTransform.rect.height / itemYBound) + 1;
	}

	public ProductInfoHolder GetEdgeItem(EdgeType childType)
	{
		if (childType == EdgeType.Top)
			return productHolders[0];
		else
			return productHolders[productHolders.Count - 1];
	}

	/// <summary>
	/// Checks if an edge activeItem has the reached the out of bounds threshold for the scroll view.
	/// </summary>
	public bool IsLimitReached(ProductInfoHolder targetItem, EdgeType edgeType)
	{
		if (edgeType == EdgeType.Top)
			return minMaxYBorder.y >= targetItem.transform.position.y;
		else
			return minMaxYBorder.x <= targetItem.transform.position.y;
	}

	/// <summary>
	/// This function sets the positions of items according to the given directives. 
	/// Regardless of whether the number of active items is more or less than what is visible on the screen, it creates a loop...
	/// </summary>
	public void InsertItem(EdgeType from, EdgeType target)
	{
		ProductInfoHolder activeItem = GetEdgeItem(from);
		ProductInfoHolder targetItem = GetEdgeItem(target);

		Vector2 offset = (int)target * itemYBound * Vector2.up;
		activeItem.SetAnchoredPos(targetItem.GetAnchoredPosition() + offset);

		int targetProductIndex = (targetItem.ProductIndex - (int)target).GetModulo(activeProducts.Length);
		activeItem.Init(activeProducts[targetProductIndex], targetProductIndex);

		productHolders.Remove(activeItem);

		if (target == EdgeType.Top)
			productHolders.Insert(0, activeItem);
		else
			productHolders.Insert(productHolders.Count, activeItem);
	}
}