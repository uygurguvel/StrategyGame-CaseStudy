using System.Collections;
using UnityEngine;

using Grid = GameExt.Grid;

public class InputController : MonoBehaviour
{
	private GridController gridController;
	private Coroutine dragCoroutine;

	private ProductBase activeProduct;
	private Grid activeGrid;

	private Camera mainCam;

	private void Awake()
	{
		ActionManager.OnDragBegin += OnDragBegin;

		gridController = GridController.Instance;

		mainCam = Camera.main;
	}
	private void Update()
	{
		if (Input.GetMouseButtonUp(0))
			MouseUp();
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

	public void OnDragBegin(ProductInfoScriptable productInfo)
	{
		Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		ProductBase product = ActionManager.GetItemFromPool(productInfo.PoolType, pos, null).GetComponent<ProductBase>();

		if (product != null)
		{
			activeProduct = product;
			dragCoroutine = StartCoroutine(IEDrag(product));
		}
	}

	private IEnumerator IEDrag(ProductBase product)
	{
		WaitForFixedUpdate wait = new WaitForFixedUpdate();

		Grid tempGrid = null;

		Vector3 targetPos;

		while (true)
		{
			targetPos = mainCam.ScreenToWorldPoint(Input.mousePosition);

			activeGrid = gridController.GetGridByWorldPos(targetPos - product.PivotOffset);

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
}
