using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SwitchButton : MonoBehaviour
{
	[SerializeField] private Transform arrowIcon;

	private ISwitcablePanel switchableObject;
	private Button button;

	private void Awake()
	{
		button = GetComponent<Button>();
		button.onClick.AddListener(OnClick);
	}

	public void Init(ISwitcablePanel switchableObject)
	{
		this.switchableObject = switchableObject;
	}

	private void OnClick()
	{
		if (switchableObject.IsClose)
			switchableObject.OpenPanel();
		else
			switchableObject.ClosePanel();

		Vector3 iconScale = arrowIcon.transform.localScale;
		iconScale.x *= -1;
		arrowIcon.transform.localScale = iconScale;

	}

}
