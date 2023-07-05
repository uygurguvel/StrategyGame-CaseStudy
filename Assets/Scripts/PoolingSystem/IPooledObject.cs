using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IPooledObject
{
	void OnObjectInstantiate(); // Instantiate
	void OnObjectGetFromPool(); // Spawn from pool
	void OnObjectReturnToPool(); // Return pool
}
