using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameExt
{
	public static class GameExt
	{
		public static int IndexOf<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key)
		{
			int i = 0;
			foreach (var pair in dictionary)
			{
				if (pair.Key.Equals(key))
				{
					return i;
				}
				i++;
			}
			return -1;
		}

		public static int GetModulo(this int k, int n)
		{
			return ((k %= n) < 0) ? k + n : k;
		}

		public static void AdjustImage(this AspectRatioFitter aspectRatioFitter, Rect rect)
		{
			float aspectRatio = rect.width / rect.height;
			aspectRatioFitter.aspectRatio = aspectRatio;
		}

		public static void SetMenuSprite(this Image image, Sprite menuSprite, AspectRatioFitter aspectRatioFitter = null)
		{
			image.overrideSprite = menuSprite;

			if (aspectRatioFitter != null)
				aspectRatioFitter.AdjustImage(menuSprite.rect);
		}

		public static void LookRotation(this Transform transform, Vector2 targetPos)
		{
			Vector2 direction = ((Vector2)transform.position - targetPos).normalized;
			var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
			var offset = 90f;
			transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
		}
	}
}