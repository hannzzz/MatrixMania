using UnityEngine;
using UnityEngine.UI;

public class InstructionsMenu : MonoBehaviour
{
    [SerializeField] GameObject[] MenuPages;
    [SerializeField] Text introText;
    int currentPage = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("showHint") == 1)
        {
            currentPage = 0;
            introText.text = "Welcome, " + PlayerPrefs.GetString("playerName") + "\n\nThis is a serious game to teach you matrix calculations! \n(press continue)";
            if (MenuPages != null)
            {
                displayCurrentPage(currentPage);
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
        
    }

    public void continuePressed()
    {
        if(currentPage < MenuPages.Length - 1)
        {
            currentPage++;
            displayCurrentPage(currentPage);
        }
        else
        {
            gameObject.SetActive(false);
            PlayerPrefs.SetInt("showHint", 0);
        }
    }

    void displayCurrentPage(int index)
    {
        for (int i = 0; i < MenuPages.Length; i++)
        {
            if(i == index)
            {
                MenuPages[i].SetActive(true);
            }
            else
            {
                MenuPages[i].SetActive(false);
            }
        }
    }
}
