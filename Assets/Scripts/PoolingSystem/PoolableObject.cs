using UnityEngine;
using UnityEngine.Events;

public class PoolableObject : MonoBehaviour, IPooledObject
{
	[SerializeField] private UnityEvent onObjectCreated;
	[SerializeField] private UnityEvent onObjectGetFromPool;
	[SerializeField] private UnityEvent onObjectReturnToPool;

	public void OnObjectReturnToPool() => onObjectReturnToPool?.Invoke();
	public void OnObjectInstantiate() => onObjectCreated?.Invoke();
	public void OnObjectGetFromPool() => onObjectGetFromPool?.Invoke();
}
