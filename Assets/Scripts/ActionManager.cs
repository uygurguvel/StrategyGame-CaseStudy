using Sirenix.Utilities;
using System;
using System.Reflection;
using UnityEngine;
using GameExt;

public static class ActionManager
{
	public static Action<IDamageAble> OnObjectDeath { get; set; }

	public static Action<ProductInfoScriptable, IProducter> OpenInformationPanel { get; set; }
	public static Action<IProducter> InfoObjectRemoved { get; set; }

	public static Action<ProductInfoScriptable> OnDragBegin { get; set; }

	public static Action<SoldierBase> UnitSelected { get; set; }
	public static Action<SoldierBase> OnUnitRemove { get; set; }

	public static Func<float> CellSize { get; set; }

	public static Func<PoolType, Vector3, Transform, GameObject> GetItemFromPool { get; set; }
	public static Action<GameObject, PoolType> ReturnItemToPool { get; set; }

	public static void ClearActionManagerData()
	{
		var info = typeof(ActionManager)
		.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

		info.ForEach(a => a.SetValue(a.Name, null));
	}

}
