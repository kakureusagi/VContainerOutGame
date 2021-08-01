using UnityEngine;

namespace App.Presentation
{

	public static class GameObjectExtensions
	{
		public static void DestroyChildren(this GameObject gameObject)
		{
			if (gameObject == null)
			{
				return;
			}

			foreach (Transform transform in gameObject.transform)
			{
				Object.Destroy(transform.gameObject);
			}
		}
	}

}