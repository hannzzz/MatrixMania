using UnityEngine;

public class GameBoard : MonoBehaviour
{
    [SerializeField] MatrixCell[] matrixCells; //Cells making up the questions
    bool isComplete;
    [HideInInspector] public int counter = 0; //Number of correct placements
    private void Start()
    {
        isComplete = false;
    }

    void Update()
    {
        //Debug.Log("Correct Counter: " + counter);
        if(counter == matrixCells.Length)
        { //If all cell placements are correct --> complete board
            //Debug.Log("set complete");
            isComplete = true;
        }
        else
        { //Count the number of correct cell placements
            counter = 0;
            for (int i = 0; i < matrixCells.Length; i++)
            {
                if (matrixCells[i].IsCorrect)
                {
                    counter++;
                }
            }
        }
    }

    public bool getIsComplete()
    {
        return isComplete;
    }
}
