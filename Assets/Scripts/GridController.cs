using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

using Grid = GameExt.Grid;

public class GridController : Singleton<GridController>
{
	[FoldoutGroup("GridSettings")]
	[SerializeField] private int width, height;
	[FoldoutGroup("GridSettings")]
	[SerializeField] private float cellSize;

	private Grid[,] grids;

	protected override void Awake()
	{
		ActionManager.CellSize = () => cellSize;
		CreateGridMap();
	}

	private void CreateGridMap()
	{
		grids = new Grid[width, height];

		for (int i = 0; i < width; i++)
		{
			for (int k = 0; k < height; k++)
			{
				Grid grid = new Grid(i, k, null);
				grids[i, k] = grid;
			}
		}
	}

	public Grid GetGridByWorldPos(Vector2 worldPos)
	{
		int x = Mathf.FloorToInt(worldPos.x / cellSize);
		int y = Mathf.FloorToInt(worldPos.y / cellSize);

		if (IsGridOffBounds(x, y))
			return null;

		return grids[x, y];
	}

	public Vector3 GetGridWorldPos(Grid grid)
	{
		return new Vector3(grid.x, grid.y, 0) * cellSize;
	}

	public bool IsGridsAvaiable(GridObjectBase product, Grid controlGrid)
	{
		bool isAvailable = true;
		var relevantGrids = GetRelevantGrids(product.ObjectSize, controlGrid);

		for (int i = 0; i < relevantGrids.Length; i++)
		{
			if (relevantGrids[i].currentObj != null)
				return false;
		}

		return isAvailable;
	}

	/// <summary>
	/// Actually, this check is not necessary because there is a grid system that covers all playable screens. 
	/// However, I perform this check just in case an object is dragged outside the screen during testing, which could potentially cause an error.
	/// </summary>
	private bool IsGridOffBounds(int x, int y)
	{
		if (x < 0 || x >= grids.GetLength(0))
			return true;

		if (y < 0 || y >= grids.GetLength(1))
			return true;

		return false;
	}

	public void FillGrids(GridObjectBase activeProduct, Grid activeGrid)
	{
		var relevantGrids = GetRelevantGrids(activeProduct.ObjectSize, activeGrid);

		foreach (Grid grid in relevantGrids)
		{
			grid.currentObj = activeProduct;
		}

		activeProduct.SetCurrentGrids(relevantGrids);
	}

	private Grid[] GetRelevantGrids(Vector2 objectSize, Grid startGrid)
	{
		int x = startGrid.x;
		int y = startGrid.y;

		List<Grid> relevantGrids = new List<Grid>();

		for (int i = 0; i < objectSize.x; i++)
		{
			for (int k = 0; k < objectSize.y; k++)
			{
				if (IsGridOffBounds(x + i, y + k))
					return new Grid[0];

				Grid gr = grids[x + i, y + k];
				relevantGrids.Add(gr);
			}
		}

		return relevantGrids.ToArray();
	}
}
