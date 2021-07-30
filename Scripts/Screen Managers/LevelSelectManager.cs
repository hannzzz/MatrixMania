using UnityEngine;
using UnityEngine.UI;

public class LevelSelectManager : MonoBehaviour
{
    [Header("Addition & Subtraction")]
    [SerializeField] Button G1_easyButton;
    [SerializeField] Image G1_easyStars;
    [SerializeField] Button G1_mediumButton;
    [SerializeField] Image G1_mediumStars;
    [SerializeField] Button G1_hardButton;
    [SerializeField] Image G1_hardStars;
    [Header("Multiplication")]
    [SerializeField] Button G2_easyButton;
    [SerializeField] Image G2_easyStars;
    [SerializeField] Button G2_mediumButton;
    [SerializeField] Image G2_mediumStars;
    [SerializeField] Button G2_hardButton;
    [SerializeField] Image G2_hardStars;
    [Header("Transpose & Inverse")]
    [SerializeField] Button G3_easyButton;
    [SerializeField] Image G3_easyStars;
    [SerializeField] Button G3_mediumButton;
    [SerializeField] Image G3_mediumStars;
    [SerializeField] Button G3_hardButton;
    [SerializeField] Image G3_hardStars;

    // Start is called before the first frame update
    void Start()
    {
        //Group 1------------------------------------//
        
        //Easy
        if (PlayerPrefs.HasKey("1E-Stars"))
        { 
            //Add stars of 1st group - easy
            setStarsUI(G1_easyStars, PlayerPrefs.GetInt("1E-Stars")); //Set level stars

            //Unlock medium if 2 stars and not unlocked before
            if (PlayerPrefs.GetInt("1E-Stars") >= 2 && !PlayerPrefs.HasKey("1M-Unlocked"))
            { 
                PlayerPrefs.SetInt("1M-Unlocked", 1);
            } 
        }

        //Medium
        if (PlayerPrefs.HasKey("1M-Unlocked"))
        {
            //Unlock medium
            G1_mediumButton.GetComponent<Image>().sprite = ResourcesManager.instance.mediumUnlocked;
            //Make button interactable
            G1_mediumButton.interactable = true;

            if (PlayerPrefs.HasKey("1M-Stars"))
            {
                //Set level stars
                setStarsUI(G1_mediumStars, PlayerPrefs.GetInt("1M-Stars"));

                //Unlock hard if 2 stars and not unlocked before
                if (PlayerPrefs.GetInt("1M-Stars") >= 2 && !PlayerPrefs.HasKey("1H-Unlocked"))
                { 
                    PlayerPrefs.SetInt("1H-Unlocked", 1);
                }
            }
        }
        else
        { //lock medium
            G1_mediumButton.GetComponent<Image>().sprite = ResourcesManager.instance.mediumLocked;
            G1_mediumButton.interactable = false;
        }

        //Hard
        if (PlayerPrefs.HasKey("1H-Unlocked"))
        {//Unlock hard
            G1_hardButton.GetComponent<Image>().sprite = ResourcesManager.instance.hardUnlocked;
            G1_hardButton.interactable = true;
            if (PlayerPrefs.HasKey("1H-Stars"))
            {//Set level stars
                setStarsUI(G1_hardStars, PlayerPrefs.GetInt("1H-Stars")); 
            }
        }
        else
        { //lock hard
            G1_hardButton.GetComponent<Image>().sprite = ResourcesManager.instance.hardLocked;
            G1_hardButton.interactable = false;
        }

        //Group 2------------------------------------//
        
        //Easy
        if (PlayerPrefs.HasKey("2E-Stars"))
        { 
            setStarsUI(G2_easyStars, PlayerPrefs.GetInt("2E-Stars")); //Set level stars
            if (PlayerPrefs.GetInt("2E-Stars") >= 2 && !PlayerPrefs.HasKey("2M-Unlocked"))
            { //Unlock medium if 2 stars and not unlocked before
                PlayerPrefs.SetInt("2M-Unlocked", 1);
            }
        }

        //Medium
        if (PlayerPrefs.HasKey("2M-Unlocked"))
        {//Unlock medium
            G2_mediumButton.GetComponent<Image>().sprite = ResourcesManager.instance.mediumUnlocked;
            G2_mediumButton.interactable = true;
            if (PlayerPrefs.HasKey("2M-Stars"))
            {//Set level stars
                setStarsUI(G2_mediumStars, PlayerPrefs.GetInt("2M-Stars"));
                if (PlayerPrefs.GetInt("2M-Stars") >= 2 && !PlayerPrefs.HasKey("2H-Unlocked"))
                { //Unlock hard if 2 stars and not unlocked before
                    PlayerPrefs.SetInt("2H-Unlocked", 1);
                }
            }
        }
        else
        { //lock medium
            G2_mediumButton.GetComponent<Image>().sprite = ResourcesManager.instance.mediumLocked;
            G2_mediumButton.interactable = false;
        }

        //Hard
        if (PlayerPrefs.HasKey("2H-Unlocked"))
        {//Unlock hard
            G2_hardButton.GetComponent<Image>().sprite = ResourcesManager.instance.hardUnlocked;
            G2_hardButton.interactable = true;
            if (PlayerPrefs.HasKey("2H-Stars"))
            {//Set level stars
                setStarsUI(G2_hardStars, PlayerPrefs.GetInt("2H-Stars"));
            }
        }
        else
        { //lock hard
            G2_hardButton.GetComponent<Image>().sprite = ResourcesManager.instance.hardLocked;
            G2_hardButton.interactable = false;
        }

        //Group 3------------------------------------//
        
        //Easy
        if (PlayerPrefs.HasKey("3E-Stars"))
        {
            setStarsUI(G3_easyStars, PlayerPrefs.GetInt("3E-Stars")); //Set level stars
            if (PlayerPrefs.GetInt("3E-Stars") >= 2 && !PlayerPrefs.HasKey("3M-Unlocked"))
            { //Unlock medium if 2 stars and not unlocked before
                PlayerPrefs.SetInt("3M-Unlocked", 1);
            }
        }
        
        //Medium
        if (PlayerPrefs.HasKey("3M-Unlocked"))
        {//Unlock medium
            G3_mediumButton.GetComponent<Image>().sprite = ResourcesManager.instance.mediumUnlocked;
            G3_mediumButton.interactable = true;
            if (PlayerPrefs.HasKey("3M-Stars"))
            {//Set level stars
                setStarsUI(G3_mediumStars, PlayerPrefs.GetInt("3M-Stars"));
                if (PlayerPrefs.GetInt("3M-Stars") >= 2 && !PlayerPrefs.HasKey("3H-Unlocked"))
                { //Unlock hard if 2 stars and not unlocked before
                    PlayerPrefs.SetInt("3H-Unlocked", 1);
                }
            }
        }
        else
        { //lock medium
            G3_mediumButton.GetComponent<Image>().sprite = ResourcesManager.instance.mediumLocked;
            G3_mediumButton.interactable = false;
        }
        
        //Hard
        if (PlayerPrefs.HasKey("3H-Unlocked"))
        {//Unlock hard
            G3_hardButton.GetComponent<Image>().sprite = ResourcesManager.instance.hardUnlocked;
            G3_hardButton.interactable = true;
            if (PlayerPrefs.HasKey("3H-Stars"))
            {//Set level stars
                setStarsUI(G3_hardStars, PlayerPrefs.GetInt("3H-Stars"));
            }
        }
        else
        { //lock hard
            G3_hardButton.GetComponent<Image>().sprite = ResourcesManager.instance.hardLocked;
            G3_hardButton.interactable = false;
        }
    }

    //Place stars graphics based on stars number
    void setStarsUI(Image btn, int stars)
    {
        switch (stars)
        {
            case 1:
                btn.sprite = ResourcesManager.instance.oneStars;
                break;
            case 2:
                btn.sprite = ResourcesManager.instance.twoStars;
                break;
            case 3:
                btn.sprite = ResourcesManager.instance.threeStars;
                break;
        }
    }

}
