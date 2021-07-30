using UnityEngine;
using UnityEngine.UI;

public class RL_Menu : MonoBehaviour
{//add this to button calling the menu
    [SerializeField] Button next_btn;
    [SerializeField] Button prev_btn;
    [SerializeField] GameObject[] pages;
    [SerializeField] GameObject menu;
    int currentPage = 0;

    // Start is called before the first frame update
    void Start()
    {
        displayPage(0);
    }

    public void toggleMenu()
    {
        if (menu.gameObject.activeInHierarchy)
        {
            menu.gameObject.SetActive(false);
        }
        else
        {
            menu.gameObject.SetActive(true);
        }
    }

    public void closeMenu()
    {
        if (menu.gameObject.activeInHierarchy)
        {
            menu.gameObject.SetActive(false);
        }
    }

    public void nextPage()
    {
        currentPage++;
        displayPage(currentPage);

        if(currentPage > pages.Length - 1)
        {
            currentPage = pages.Length - 1;
        }
    }

    public void prevPage()
    {
        currentPage--;
        displayPage(currentPage);

        if (currentPage < 0)
        {
            currentPage = 0;
        }
    }

    public void displayPage(int index)
    {
        for(int i = 0; i< pages.Length; i++)
        {
            if(index == i)
            {
                pages[i].SetActive(true);
            }
            else
            {
                pages[i].SetActive(false);
            }
        }

        if(index == 0)
        {
            prev_btn.gameObject.SetActive(false);
        }
        else
        {
            prev_btn.gameObject.SetActive(true);
        }
        if (index == pages.Length - 1)
        {
            next_btn.gameObject.SetActive(false);
        }
        else
        {
            next_btn.gameObject.SetActive(true);
        }
    }
}
