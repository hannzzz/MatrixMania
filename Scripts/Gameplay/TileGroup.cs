using System.Collections.Generic;
using UnityEngine;

namespace Gameplay {
	public class TileGroup : MonoBehaviour {
		// the elements of the group
		[SerializeField]
		private List<TileGroupElement> elements = new List<TileGroupElement>();

		// the "NumberTile" components, extracted from the elements of the group
		private NumberTile[] elementTiles;
		private int currentCorrectPlacementsCount;
		private int totalPlacementsCount;

		private void Awake() {
			Setup();
		}

		private void Setup() {
			SetupElements();
			ExtractTilesFromElements();
			SetupListeners();
		}

		// obtain the group elements if they're not provided through the inspector
		private void SetupElements() {
			if (elements.Count != 0) {
				return;
			}

			foreach (Transform childTransform in transform) {
				TileGroupElement childElement = childTransform.GetComponent<TileGroupElement>();
				
				if (!childElement) {
					childElement = childTransform.gameObject.AddComponent<TileGroupElement>();
				}

				elements.Add(childElement);
			}
		}

		private void SetupListeners() {
			foreach (NumberTile currentTile in elementTiles) {
				currentTile.OnDragStart += OnTileDragStart;
				currentTile.OnCorrectPlacement += OnTileCorrectPlacement;
				currentTile.OnWrongPlacement += OnWrongTilePlacement;
			}
		}

		private void OnTileDragStart() {
			ResetData();
		}

		// obtain the "NumberTile" component from the group elements
		private void ExtractTilesFromElements() {
			elementTiles = new NumberTile[elements.Count];
			for (int i = 0; i < elements.Count; i++) {
				elementTiles[i] = elements[i].GetComponent<NumberTile>();
			}
		}

		private void OnTileCorrectPlacement() {
			// increases the current correct placements count
			currentCorrectPlacementsCount++;

			// check if all the tiles were placed in the correct place
			// 		if so, make them all play the correct placement actions
			if (currentCorrectPlacementsCount == elementTiles.Length) {
				foreach (NumberTile currentTile in elementTiles) {
					currentTile.DoCorrectPlacementActions();
				}
			}

			OnAnyPlacement();
		}

		private void OnWrongTilePlacement() {
			OnAnyPlacement();
		}

		private void OnAnyPlacement() {
			// increases the current total placements count
			totalPlacementsCount++;

			// check if all the tiles were placed in the wrong place
			// 		if so, make them all play the wrong placement actions and increase the strikes
			if (currentCorrectPlacementsCount != totalPlacementsCount &&
			    totalPlacementsCount == elementTiles.Length) {
				foreach (NumberTile currentTile in elementTiles) {
					currentTile.DoWrongPlacementActions();
				}

				NumberTile.DoWrongPlacementGlobalActions();
			}
		}

		// reset the internal state of the class
		private void ResetData() {
			currentCorrectPlacementsCount = 0;
			totalPlacementsCount = 0;
		}
	}
}