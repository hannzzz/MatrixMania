using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    //Inspector controls
    [SerializeField] float levelTime = 120; //seconds
    //Star Conditions
    [SerializeField] int minTo3Stars = 4; //Min Correct questions needed for 3 stars
    [SerializeField] int minTo2Stars = 3; //Min Correct questions needed for 2 stars
    [SerializeField] int minTo1Stars = 2; //Min Correct questions needed for 1 star
    //UI
    [SerializeField] Text timeUI; //Time display
    [SerializeField] Animator timerAnim; //Timer animator
    [SerializeField] GameObject menuUI; //Results menu
    [SerializeField] Text menuUIMessage;
    [SerializeField] Text menuUIScore;
    [SerializeField] Image menuUIStars;
    [SerializeField] GameObject tutorialUI; //Tutorial menu
    //Level objects
    [SerializeField] Image[] strikesUI; //Xs
    [SerializeField] GameBoard[] questions; //Drag "GameBoard" object of each question in the level here

    //Level data
    int questionIndex; //Which question we're currently on
    int score;
    int stars;
    int strikes;
    //Vars
    string sceneName;
    float timer;
    bool gameOver;
    bool run;

    //Singleton
    public static LevelManager instance = null;
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(instance.gameObject);
            instance = this;
        }
    }
    //Initial values
    void Start()
    {
        score = 0;
        stars = 0;
        strikes = 0;
        questionIndex = 0;
        timer = levelTime;
        sceneName = SceneManager.GetActiveScene().name;
        menuUI.SetActive(false);
        gameOver = false;
        run = true;
    }

    void Update()
    {
        if (run) //Level is running
        {
            if (gameOver) //Gameover
            { 
                //Play SFX
                ResourcesManager.instance.playLevelClearSFX();
                //Display score / number of questions
                menuUIScore.text = score + "/" + questions.Length;
                //Calculate stars
                stars = getStars(score);
                //Add stars graphics
                if (stars == 1) { menuUIStars.sprite = ResourcesManager.instance.oneStars; }
                else if (stars == 2) { menuUIStars.sprite = ResourcesManager.instance.twoStars; }
                else if (stars == 3) { menuUIStars.sprite = ResourcesManager.instance.threeStars; }
                else { menuUIStars.sprite = ResourcesManager.instance.noStars; }
                //Show score menu
                menuUI.SetActive(true);
                //Saving data
                PlayerPrefs.SetInt(sceneName + "-Stars", stars); //Save stars
                run = false;
            }
            else
            { //Level running
                if (timer > 0)
                { 
                    //Timer display
                    DisplayTime(timer);
                    //Strikes display
                    DisplayStrikes(strikes);

                    timer -= Time.deltaTime;

                    //Next question: if current is complete but not final question
                    if (questions[questionIndex].getIsComplete() && questionIndex < questions.Length - 1)
                    { 
                        StartCoroutine(MoveCameraToNextQuestion()); //Move camera
                        questionIndex++;
                        score++;
                        //strikes = 0; //uncomment to reset strikes after each question
                    }

                    if (questions[questionIndex].getIsComplete() && questionIndex == questions.Length - 1 && !gameOver && timer > 0)
                    { //Completed the final question
                        score++;
                        menuUIMessage.text = "Congratulations!";
                        gameOver = true;
                    }

                    if (strikes >= 3)
                    { //3 strikes
                        menuUIMessage.text = "Game Over!";
                        gameOver = true;
                    }

                }
                else
                { //Time out
                    menuUIMessage.text = "Time's up!";
                    gameOver = true;
                }
            }
        }
        
    }

    //Splits time (s) into (mm:ss) and displays it in UI
    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeUI.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    //Display number of Xs according to strikes on UI
    void DisplayStrikes(int numOfStrikes)
    {
        if (numOfStrikes > 0)
        {
            for (int i = 0; i < numOfStrikes; i++)
            {
                strikesUI[i].color = Color.red;
            }
        }
        else
        {
            for (int i = 0; i < strikesUI.Length; i++)
            {
                strikesUI[i].color = Color.white;
            }
        }
        
    }

    public void incrementStrikes()
    {
        strikes++;  
    }

    //Time penalty
    public void decrementTimer()
    {
        timer -= 5;
        timerAnim.SetTrigger("DecTimer"); //-5 animation
    }

    //From score to stars
    int getStars(int score)
    {
        if(score >= minTo3Stars)
        {
            return 3;
        }else if(score >= minTo2Stars)
        {
            return 2;
        }
        else if(score >= minTo1Stars)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    //Moves camera to next question
    private IEnumerator MoveCameraToNextQuestion()
    {
        float t = 0.0f;
        Vector3 startingPos = Camera.main.transform.position;
        //Move 20 in x
        Vector3 targetPos = new Vector3(Camera.main.transform.position.x + 20, Camera.main.transform.position.y, Camera.main.transform.position.z);
        while (t < 1.0f)
        {
            t += Time.deltaTime * (Time.timeScale / 1.5f); //Transition duration

            Camera.main.transform.position = Vector3.Lerp(startingPos, targetPos, t);
            yield return 0;
        }
        
    }

    //Tutorial functions toggler
    public void toggleTutorialMenu()
    {
        if (tutorialUI.activeInHierarchy)
        {
            tutorialUI.SetActive(false);
        }
        else
        {
            tutorialUI.SetActive(true);
        }
    }

    //Closes tutorial menu
    public void closeTutorialMenu()
    {
        if (tutorialUI.activeInHierarchy)
        {
            tutorialUI.SetActive(false);
        }
    }
}
