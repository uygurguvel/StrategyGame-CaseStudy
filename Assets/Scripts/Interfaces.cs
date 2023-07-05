using DG.Tweening;

public interface ISwitcablePanel
{
	SwitchButton switchButton { get; set; }

	bool IsClose { get; set; }

	void OpenPanel();
	void ClosePanel();

}