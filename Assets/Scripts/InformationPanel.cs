using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InformationPanel : CanvasPanelBase
{
	[SerializeField] private Image mainImage;
	[SerializeField] private TextMeshProUGUI nameText;

	private SpriteSetter spriteSetter;
	private AspectRatioFitter aspectRatioFitter;

	private ProductInfoScriptable currentInfo;

	protected override void Awake()
	{
		base.Awake();

		spriteSetter = new SpriteSetter(mainImage);

		aspectRatioFitter = GetComponentInChildren<AspectRatioFitter>();
		mainImage.enabled = false;

		ActionManager.ProductSelected += OnProductSelected;
	}

	private void Start()
	{
		ClosePanel();
	}

	private void OnProductSelected(ProductInfoScriptable info)
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
		spriteSetter.SetSprite(currentInfo.MenuSprite, aspectRatioFitter);
	}
}
