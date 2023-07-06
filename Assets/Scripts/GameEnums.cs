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
		BUILDING_BARRACK = 1,
		BUILDING_POWERPLANT = 2,
		BUILDING_TOWER = 3,

		//SOLDIERS 10-19;
		SOLDIER_1 = 10,
		SOLDIER_2 = 11,
		SOLDIER_3 = 12,

		//UI_ELEMENTS 20,29;
		UI_PRODUCT_HOLDER = 20,

	}

	[Serializable]
	public class Grid
	{
		public int x;
		public int y;

		public ProductBase currentObj;

		public Grid(int x, int y, ProductBase currentObj)
		{
			this.x = x;
			this.y = y;
			this.currentObj = currentObj;
		}
	}
}
