using System.Linq;
using ClassExtensions;
using UnityEngine;

namespace Gameplay {
	// this components represents the element of a tile group, it has knowledge about the other 
	// tiles in the group, 
	public class TileGroupElement : MonoBehaviour {
		// is automatically filled if it's not assigned in the inspector
		[SerializeField]
		private NumberTile[] boundTiles;

		private NumberTile numberTile;
		private TileGroupElement[] boundElementsWithSelf;
		private Vector2[] offsets;
		private bool initialized;
		private bool isDragActive;

		private void Awake() {
			numberTile = GetComponent<NumberTile>();
		}

		private void Start() {
			Setup();
		}

		private void LateUpdate() {
			if (!initialized || !isDragActive) {
				return;
			}

			DragBoundTiles();
		}

		// make the bound tiles move with the one that's being dragged
		private void DragBoundTiles() {
			Vector3 currentPosition = transform.position;

			for (int i = 0; i < boundTiles.Length; i++) {
				Vector3 boundTilePosition = new Vector3(
					currentPosition.x + offsets[i].x,
					currentPosition.y + offsets[i].y,
					transform.position.z
				);
				boundTiles[i].transform.position = boundTilePosition;
			}
		}

		private void Setup() {
			SetupBoundTiles();
			SetupEvents();
			SetupOffsets();

			initialized = true;
		}

		// obtain the "NumberTile" component from the sibling objects if the "boundTiles" variable is null or empty
		private void SetupBoundTiles() {
			if (boundTiles == null || boundTiles.Length == 0) {
				boundTiles = gameObject.GetComponentInSiblings<NumberTile>().ToArray();
			}
		}

		private void SetupEvents() {
			numberTile.OnDragStart += OnDragStart;
			numberTile.OnDragEnd += OnDragEnd;
		}

		// setup the offsets used on dragging
		private void SetupOffsets() {
			Vector3 currentPosition = transform.position;

			offsets = new Vector2[boundTiles.Length];
			for (int i = 0; i < boundTiles.Length; i++) {
				offsets[i] = boundTiles[i].transform.position - currentPosition;
			}
		}

		// warn bound objects that the drag started
		private void OnDragStart() {
			isDragActive = true;
			SetTilesDragActive(true);
		}

		// warn bound objects that the drag ended
		private void OnDragEnd() {
			isDragActive = false;
			SetTilesDragActive(false);
		}

		// set to true of false the "IsDragActive" variable on "boundTiles"
		private void SetTilesDragActive(bool value) {
			foreach (NumberTile boundTile in boundTiles) {
				boundTile.IsDragActive = value;
			}
		}
	}
}