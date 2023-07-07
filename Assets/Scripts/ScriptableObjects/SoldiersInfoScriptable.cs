using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/SoldierSettings")]
public class SoldiersInfoScriptable : ProductInfoScriptable
{
	[FoldoutGroup("AttackSettings")]
	[SerializeField] protected int attackDamage;

	[FoldoutGroup("AttackSettings")]
	[SerializeField] protected float fireRate;
	[FoldoutGroup("AttackSettings")]
	[SerializeField] protected float fireRange;

	public int AttackDamage { get => attackDamage; }
	public float FireRate { get => fireRate; }
	public float FireRange { get => fireRange; }

}
