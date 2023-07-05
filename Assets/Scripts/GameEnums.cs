
using UnityEngine.UI;
using UnityEngine;
using System;

public enum EdgeType
{
	Top = 1,
	Down = -1,
}

public enum PoolType
{
	//BUILDINGS 0-9;
	House = 0,
	Barrack = 1,
	PowerPlant = 2,
	Tower = 3,

	//SOLDIERS 10-19;
	Soldier1 = 10,
	Soldier2 = 11,
	Soldier3 = 12,

	//UI_ELEMENTS 20,29;
	ProductHolder = 20,

}


[Serializable]
public class SpriteSetter
{
	public Image image;

	public SpriteSetter(Image image)
	{
		this.image = image;
	}

	public void SetSprite(Sprite sprite, AspectRatioFitter aspectRatioFitter = null)
	{
		image.overrideSprite = sprite;

		if (aspectRatioFitter != null)
			aspectRatioFitter.AdjustImage(sprite.rect);
	}
}
