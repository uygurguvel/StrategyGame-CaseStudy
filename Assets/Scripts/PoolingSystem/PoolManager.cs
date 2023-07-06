using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using GameExt;

[System.Serializable]
public class Pool
{
	public PoolType type;
	[AssetsOnly] public GameObject prefab;
	public int size;
}

public class PoolManager : MonoBehaviour
{
	[ShowInInspector, ReadOnly] public Dictionary<string, Queue<GameObject>> poolDictionary;
	[SerializeField, ReadOnly] private List<Transform> poolParents = new List<Transform>();

	[SerializeField] private List<Pool> pools;

	private void Awake()
	{
		Init();

		ActionManager.GetItemFromPool += SpawnFromPool;
		ActionManager.ReturnItemToPool += ReturnPool;
	}

	private void Init()
	{
		// Create Pool Parents
		GameObject poolParent;

		for (int i = 0; i < pools.Count; i++)
		{
			poolParent = new GameObject();
			poolParent.transform.SetParent(transform);
			poolParent.name = pools[i].type.ToString();
			poolParents.Add(poolParent.transform);
		}

		poolDictionary = new Dictionary<string, Queue<GameObject>>();

		// Create Pool Size
		for (int i = 0; i < pools.Count; i++)
		{
			Queue<GameObject> objectPool = new Queue<GameObject>();

			for (int j = 0; j < pools[i].size; j++)
			{
				GameObject obj = Instantiate(pools[i].prefab, parent: poolParents[i]);
				obj.SetActive(false);
				objectPool.Enqueue(obj);

				IPooledObject pooled = obj.GetComponent<IPooledObject>();
				if (pooled != null)
					pooled.OnObjectInstantiate();

			}

			poolDictionary.Add(pools[i].type.ToString(), objectPool);
		}
	}

	private GameObject SpawnFromPool(PoolType type, Vector3 position = default, Transform parent = null)
	{
		string tag = type.ToString();

		GameObject objectSpawn;

		if (!poolDictionary.ContainsKey(tag))
		{
			Debug.LogError("Pool with tag " + tag + "doesnt exist");
			return null;
		}

		if (poolDictionary[tag].Count > 0)
		{
			objectSpawn = poolDictionary[tag].Dequeue();
		}
		else
		{
			int poolIndex = poolDictionary.IndexOf(tag);
			pools[poolIndex].size++;
			objectSpawn = Instantiate(pools[poolIndex].prefab);

			IPooledObject createdObject = objectSpawn.GetComponent<IPooledObject>();

			if (createdObject != null)
				createdObject.OnObjectInstantiate();

		}

		objectSpawn.SetActive(true);
		objectSpawn.transform.position = position;

		objectSpawn.transform.SetParent(parent);

		IPooledObject pooled = objectSpawn.GetComponent<IPooledObject>();

		if (pooled != null)
			pooled.OnObjectGetFromPool();

		objectSpawn.name = type + " :" + objectSpawn.GetInstanceID();
		return objectSpawn;
	}

	private void ReturnPool(GameObject poolable, PoolType type)
	{
		string tag = type.ToString();
		IPooledObject pooled = poolable.GetComponent<IPooledObject>();

		if (pooled != null)
			pooled.OnObjectReturnToPool();

		poolable.SetActive(false);
		poolable.transform.SetParent(poolParents.Find(a => a.name == type.ToString()));


		if (!poolDictionary[tag].Contains(poolable))
			poolDictionary[tag].Enqueue(poolable);
	}
}
