using System;
using UnityEngine;

namespace GameExt
{
	public enum EdgeType
	{
		Top = 1,
		Down = -1,
	}

	public enum PoolType
	{
		//BUILDINGS 0-9;
		BUILDING_HOUSE = 0,
		BUILDING_BARRACKS = 1,
		BUILDING_POWERPLANT = 2,
		BUILDING_TOWER = 3,

		//SOLDIERS 10-19;
		SOLDIER_1 = 10,
		SOLDIER_2 = 11,
		SOLDIER_3 = 12,

		//UI_ELEMENTS 20-29;
		UI_PRODUCT_HOLDER = 20,
		UI_UNIT_HOLDER = 21,

		//OTHER_OBJECTS = 30-49;
		TARGET_POINT = 30,
		BULLET = 31,
	}

	[Serializable]
	public class Grid
	{
		public int x;
		public int y;

		public GridObjectBase currentObj;

		public Grid(int x, int y, GridObjectBase currentObj)
		{
			this.x = x;
			this.y = y;
			this.currentObj = currentObj;
		}
	}

	public class HealthSystem
	{
		IDamageAble damageableObject;

		private int baseHealh;
		private int currentHealth;

		public HealthSystem(int baseHealh, IDamageAble damageableObject)
		{
			this.baseHealh = baseHealh;
			currentHealth = baseHealh;

			this.damageableObject = damageableObject;
		}

		public void GetDamage(int damage)
		{
			currentHealth -= damage;

			if (currentHealth <= 0)
				Death();
		}

		public void Death()
		{
			damageableObject.OnDeath();
		}

		public void ReSpawn()
		{
			currentHealth = baseHealh;
		}

	}
}
