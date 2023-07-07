using System;
using System.Collections;
using UnityEngine;

using Grid = GameExt.Grid;

public class InputController : MonoBehaviour
{
	private GridController gridController;
	private Coroutine dragCoroutine;

	private GridObjectBase activeProduct;
	private Grid activeGrid;

	private SoldierBase activeUnit;

	private Camera mainCam;


	private void Awake()
	{
		ActionManager.OnDragBegin += OnDragBegin;

		ActionManager.UnitSelected += OnUnitSelected;
		ActionManager.OnUnitRemove += OnUnitRemove;
		ActionManager.OnObjectDeath += OnObjectDeath;

		gridController = GridController.Instance;

		mainCam = Camera.main;
	}
	private void Update()
	{
		if (Input.GetMouseButtonUp(0))
			MouseUp();
		if (Input.GetMouseButtonDown(1))
			OnRightClick();
	}

	private void MouseUp()
	{
		if (dragCoroutine != null)
		{
			StopCoroutine(dragCoroutine);
			dragCoroutine = null;
		}

		if (activeProduct != null)
			DragCompleted();
	}

	private void OnRightClick()
	{
		Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

		if (Physics.Raycast(ray, out RaycastHit hit))
		{
			if (activeUnit != null)
			{
				var gridObj = hit.collider.GetComponent<GridObjectBase>();
				activeUnit.SetTarget(hit.point, gridObj);
			}

		}
	}

	public void OnDragBegin(ProductInfoScriptable productInfo)
	{
		Vector3 pos = mainCam.ScreenToWorldPoint(Input.mousePosition);

		GridObjectBase product = ActionManager.GetItemFromPool(productInfo.PoolType, pos, null).GetComponent<GridObjectBase>();

		if (product != null)
		{
			activeProduct = product;
			dragCoroutine = StartCoroutine(IEDrag(product));
		}
	}

	private IEnumerator IEDrag(GridObjectBase product)
	{
		WaitForFixedUpdate wait = new WaitForFixedUpdate();

		Grid tempGrid = null;

		Vector3 targetPos;

		while (true)
		{
			targetPos = mainCam.ScreenToWorldPoint(Input.mousePosition);

			activeGrid = gridController.GetGridByWorldPos(targetPos/* + product.PivotOffset*/);

			if (activeGrid != null)
			{
				//This line is to avoid repeatedly scanning within the same grid.
				if (tempGrid == activeGrid)
				{
					yield return wait;
					continue;
				}

				tempGrid = activeGrid;

				if (gridController.IsGridsAvaiable(product, activeGrid))
					product.SetFitState(true);
				else
					product.SetFitState(false);


				product.Snap(gridController.GetGridWorldPos(activeGrid));
			}
			else
			{
				product.SetFitState(false);
			}

			yield return wait;
		}

	}

	private void DragCompleted()
	{
		activeProduct.DragComplete();

		if (activeProduct.IsFit)
		{
			gridController.FillGrids(activeProduct, activeGrid);
		}
	}

	private void OnUnitSelected(SoldierBase unit)
	{
		if (activeUnit != null)
			activeUnit.UnitChanged();

		activeUnit = unit;
		activeUnit.UnitSelected();
	}

	private void OnUnitRemove(SoldierBase obj)
	{
		if (activeUnit == obj)
			activeUnit = null;
	}

	private void OnObjectDeath(IDamageAble obj)
	{
		if (activeUnit as IDamageAble == obj)
			activeUnit = null;
	}

}
