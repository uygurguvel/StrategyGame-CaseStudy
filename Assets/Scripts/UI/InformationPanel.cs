using TMPro;
using UnityEngine;
using UnityEngine.UI;
using GameExt;
using System;

public class InformationPanel : CanvasPanelBase
{
	[SerializeField] private Image mainImage;
	[SerializeField] private TextMeshProUGUI nameText;
	[SerializeField] private Transform productLayout;

	private AspectRatioFitter aspectRatioFitter;
	private ProductInfoScriptable currentInfo;

	private UIUnit[] infoHolders = new UIUnit[0];

	protected override void Awake()
	{
		base.Awake();

		aspectRatioFitter = GetComponentInChildren<AspectRatioFitter>();
		mainImage.enabled = false;

		ActionManager.OpenInformationPanel += OnObjectSelected;

		ClosePanel();
	}

	private void OnObjectSelected(ProductInfoScriptable info, IProducter producter)
	{
		currentInfo = info;

		ClearSubProducts();

		if (producter != null)
		{
			SetProducts(producter);
			VisualizeInfo();

			if (IsClose)
				OpenPanel();
		}
		else
		{
			ClearInformationPanel();

			if (!IsClose)
				ClosePanel();
		}
	}

	private void ClearInformationPanel()
	{
		nameText.text = string.Empty;
		mainImage.enabled = false;
	}

	private void VisualizeInfo()
	{
		nameText.text = currentInfo.ProductName;

		mainImage.enabled = true;
		mainImage.SetMenuSprite(currentInfo.MenuSprite, aspectRatioFitter);

	}

	private void SetProducts(IProducter producter)
	{
		var products = (currentInfo as ProducterBuildingInfo).Products;

		infoHolders = new UIUnit[products.Length];

		for (int i = 0; i < products.Length; i++)
		{
			UIUnit infoHolder = ActionManager.GetItemFromPool(PoolType.UI_UNIT_HOLDER, Vector3.zero, productLayout).GetComponent<UIUnit>();
			infoHolder.Init(products[i]);
			infoHolder.SetProducter(producter);
			infoHolders[i] = infoHolder;
		}
	}

	private void ClearSubProducts()
	{
		for (int i = 0; i < infoHolders.Length; i++)
		{
			ActionManager.ReturnItemToPool(infoHolders[i].gameObject, PoolType.UI_PRODUCT_HOLDER);
		}

		Array.Resize(ref infoHolders, 0);
	}
}
