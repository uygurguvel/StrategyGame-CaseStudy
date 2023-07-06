using TMPro;
using UnityEngine;
using UnityEngine.UI;
using GameExt;

public class InformationPanel : CanvasPanelBase
{
	[SerializeField] private Image mainImage;
	[SerializeField] private TextMeshProUGUI nameText;

	private AspectRatioFitter aspectRatioFitter;

	private ProductInfoScriptable currentInfo;

	protected override void Awake()
	{
		base.Awake();

		aspectRatioFitter = GetComponentInChildren<AspectRatioFitter>();
		mainImage.enabled = false;

		//ActionManager.ProductSelected += OnUnitSelected;
	}

	private void Start()
	{
		ClosePanel();
	}

	private void OnUnitSelected(ProductInfoScriptable info)
	{
		currentInfo = info;

		VisualizeInfo();

		if (IsClose)
			OpenPanel();
	}

	private void VisualizeInfo()
	{
		nameText.text = currentInfo.name;

		mainImage.enabled = true;
		mainImage.SetMenuSprite(currentInfo.MenuSprite, aspectRatioFitter);
	}
}
