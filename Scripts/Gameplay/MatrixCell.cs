using UnityEngine;

public class MatrixCell : MonoBehaviour {
	//A single matrix cell on a board
	[SerializeField]
	int id;

	[SerializeField]
	private SpriteRenderer[] hintSquares; //Which cells should be hinted at

	[SerializeField]
	private Color[] hintSquareColors; //What colour each cell should be

	private bool isCorrect;
	private bool isEmpty = true;

	public int ID => id;

	public bool IsCorrect {
		get => isCorrect;
		set => isCorrect = value;
	}

	public bool IsEmpty {
		get => isEmpty;
		set => isEmpty = value;
	}

	public void showHint() {
		//Loop over all hint squares and assign them the specified color
		if (hintSquares.Length > 0) {
			if (hintSquareColors.Length > 0) {
				for (int i = 0; i < hintSquares.Length; i++) {
					//Debug.Log(hintSquareColors[i]);
					hintSquares[i].color = new Color(hintSquareColors[i].r, hintSquareColors[i].g, hintSquareColors[i].b);
				}
			}
			else {
				for (int i = 0; i < hintSquares.Length; i++) {
					hintSquares[i].color = Color.red;
				}
			}
		}
	}

	public void hideHint() {
		//Return hinted tiles to normal colour
		if (hintSquares != null) {
			for (int i = 0; i < hintSquares.Length; i++) {
				hintSquares[i].color = Color.white;
			}
		}
	}
}