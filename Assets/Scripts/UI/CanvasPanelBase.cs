using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class CanvasPanelBase : MonoBehaviour, ISwitcablePanel
{
	[FoldoutGroup("SwitchSettings")]
	[SerializeField] protected Vector2 closedPanelPosition;
	[FoldoutGroup("SwitchSettings")]
	[SerializeField] protected Ease switchEase;
	[FoldoutGroup("SwitchSettings")]
	[SerializeField] protected float switchDoTime;

	protected RectTransform rectTransform;
	protected Tween openCloseTween;

	protected Vector2 initialPos;

	public SwitchButton SwitchButton { get; set; }
	public bool IsClose { get; set; }


	protected virtual void Awake()
	{
		rectTransform = GetComponent<RectTransform>();
		initialPos = rectTransform.anchoredPosition;

		SwitchButton = GetComponentInChildren<SwitchButton>();
		SwitchButton.Init(this);
	}

	public void OpenPanel()
	{
		openCloseTween.Kill();
		openCloseTween = rectTransform.DOAnchorPos(initialPos, switchDoTime).SetEase(switchEase);
		
		IsClose = false;
	}

	public void ClosePanel()
	{
		openCloseTween.Kill();
		openCloseTween = rectTransform.DOAnchorPos(closedPanelPosition, switchDoTime).SetEase(switchEase);

		IsClose = true;
	}
}
