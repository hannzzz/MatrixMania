using System;
using Gameplay;
using UnityEngine;
using UnityEngine.EventSystems;


public class NumberTile : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler {
	[SerializeField]
	private int id; //Should equal id of correct matrix cell

	public event Action OnDragStart;
	public event Action OnDragEnd;
	public event Action OnCorrectPlacement;
	public event Action OnWrongPlacement;

	public MatrixCell CurrentMatrixCell => currentMatrixCell;
	public bool CanMove { get; set; } = true;
	public bool IsDragActive { get; set; }

	//Component variables
	private Animator animator;
	private MatrixCell currentMatrixCell;
	private SpriteRenderer spriteRenderer;
	private TileGroupElement tileGroupElement;
	private Vector2 mousePosition;

	private void Awake() {
		animator = transform.GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		tileGroupElement = GetComponent<TileGroupElement>();
	}

	public void OnBeginDrag(PointerEventData eventData) {
		IsDragActive = true;
	}

	public void OnDrag(PointerEventData eventData) {
		if (!CanMove) {
			return;
		}

		OnDragStart?.Invoke();
		//Drag and drop logic
		mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); //Get mouse position
		transform.position = mousePosition;
	}

	public void OnEndDrag(PointerEventData eventData) {
		IsDragActive = false;
		OnDragEnd?.Invoke();
	}

	private void OnTriggerStay2D(Collider2D other) {
		MatrixCell matrixCell = other.gameObject.GetComponent<MatrixCell>();

		//When dragging tile into a square
		if (IsDragActive ||
		    !matrixCell ||
		    matrixCell == currentMatrixCell ||
		    spriteRenderer.color != Color.white ||
		    !matrixCell.IsEmpty ||
		    matrixCell.IsCorrect) {
			return;
		}

		currentMatrixCell = matrixCell;

		//if same id (Correct placement)
		if (currentMatrixCell.ID.Equals(id)) {
			OnCorrectPlacement?.Invoke();
			
			if (!tileGroupElement) {
				DoCorrectPlacementActions();
			}
		}
		//Wrong placement
		else {
			OnWrongPlacement?.Invoke();
			
			if (!tileGroupElement) {
				DoWrongPlacementActions();
			}
		}
	}

	private void OnTriggerExit2D(Collider2D other) {
		if (!currentMatrixCell || other.gameObject != currentMatrixCell.gameObject || currentMatrixCell.IsCorrect) {
			return;
		}

		//Return original color
		spriteRenderer.color = Color.white;
		//Set matrix cell as empty
		currentMatrixCell.IsEmpty = true;
		//Hide shown hints
		currentMatrixCell.hideHint();

		currentMatrixCell = null;
	}

	public void DoCorrectPlacementActions() {
		//Change color to green
		spriteRenderer.color = Color.green;
		//Stop tile from moving
		CanMove = false;
		//Snap tile to cell position
		transform.position = currentMatrixCell.transform.position;
		//Set tile place as not empty to prevent another tile from being added
		currentMatrixCell.IsEmpty = false;
		//Set as correct to count in GameBoard script and determine whether the question is fully answered or not
		currentMatrixCell.IsCorrect = true;

		ResourcesManager.instance.playCorrectSFX(); //play sfx
	}

	public void DoWrongPlacementActions() {
		//Play Animation
		animator.SetTrigger("Wrong");
		//Change color to red
		spriteRenderer.color = Color.red;
		//Snap tile to cell position
		transform.position = currentMatrixCell.transform.position;
		//Set tile place as not empty
		currentMatrixCell.IsEmpty = false;
		//Show hint
		currentMatrixCell.showHint();

		if (!tileGroupElement) {
			DoWrongPlacementGlobalActions();
		}
		
		ResourcesManager.instance.playWrongSFX(); //Play sfx
	}

	public static void DoWrongPlacementGlobalActions() {
		LevelManager.instance.incrementStrikes(); //Add a strike
		LevelManager.instance.decrementTimer(); //Decrease time
	}	
}