using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ClassExtensions {
	public static class GameObjectExtensions {
		public static IEnumerable<T> GetComponentInSiblings<T>(this GameObject gameObject) where T : Component {
			GameObject parentGameObject = gameObject.transform.parent.gameObject;

			return parentGameObject
				.GetComponentsInChildren<T>()
				.Where(currentSibling => {
					GameObject currentTileGameObject = currentSibling.gameObject;
					return currentTileGameObject != parentGameObject &&
					       currentTileGameObject != gameObject;
				});
		}
	}
}