using DG.Tweening;
using GameExt;
using TMPro;
using UnityEngine;

public class HitGhostText : MonoBehaviour, IPooledObject
{
	[SerializeField] private TextMeshPro text;

	[SerializeField] private float randomAmount;
	[SerializeField] private float height;
	[SerializeField] private float doTime;

	[SerializeField] private Ease ease = Ease.InBack;

	private Vector3 initialScale;

	public void Init(int damage)
	{
		text.text = damage.ToString();
		transform.DOScale(0.1f, doTime).SetEase(Ease.InOutSine).SetRelative().SetLoops(2, LoopType.Yoyo);

		Vector2 rndCircle = new Vector2(Random.Range(-randomAmount, randomAmount), height);

		transform.DOMove(rndCircle, doTime).SetEase(ease).SetRelative()
			.OnComplete(() =>
			{
				ActionManager.ReturnItemToPool(gameObject, PoolType.HIT_GHOST_TEXT);
			});
	}

	public void OnObjectGetFromPool()
	{
		transform.localScale = initialScale;
	}

	public void OnObjectInstantiate()
	{
		initialScale = transform.localScale;
	}

	public void OnObjectReturnToPool()
	{
	}
}
