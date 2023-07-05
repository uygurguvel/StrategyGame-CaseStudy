using UnityEngine;
using UnityEngine.UI;

public class ProductInfoScriptable : ScriptableObject
{
	[SerializeField] protected string productName;
	[SerializeField] protected Sprite menuSprite;
	[SerializeField] protected PoolType poolType;

	public string ProductName { get => productName; }
	public PoolType PoolType { get => poolType; }
	public Sprite MenuSprite { get => menuSprite; }
}
