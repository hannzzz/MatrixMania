using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TheoryManager : MonoBehaviour
{
    [SerializeField] Button nextBtn;
    [SerializeField] Button prevBtn;
    [SerializeField] GameObject[] screens;
    int currentIndex = 0;

    private void Start()
    {
        btnsDisplay();
    }

    void btnsDisplay()
    {
        if (currentIndex == 0)
        {
            prevBtn.gameObject.SetActive(false);
        }
        else
        {
            prevBtn.gameObject.SetActive(true);
        }

        if(currentIndex == screens.Length - 1)
        {
            nextBtn.gameObject.SetActive(false);
        }
        else
        {
            nextBtn.gameObject.SetActive(true);
        }
    }

    public void next()
    {
        if(currentIndex >= 0 && currentIndex < screens.Length - 1)
        {
            StartCoroutine(MoveCameraToNextScreen(true));
            currentIndex++;
        }
    }

    public void prev()
    {
        if (currentIndex > 0 && currentIndex < screens.Length)
        {
            StartCoroutine(MoveCameraToNextScreen(false));
            currentIndex--;
        }
    }

    private IEnumerator MoveCameraToNextScreen(bool down)
    {
        float t = 0.0f;
        Vector3 startingPos = Camera.main.transform.position;
        Vector3 targetPos;
        if (down)
        {
            targetPos = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y - 10, Camera.main.transform.position.z);
        }
        else
        {
            targetPos = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y + 10, Camera.main.transform.position.z);
        }
        while (t < 1.0f)
        {
            t += Time.deltaTime * (Time.timeScale / 0.1f); //Transition duration


            Camera.main.transform.position = Vector3.Lerp(startingPos, targetPos, t);
            btnsDisplay();
            yield return 0;
        }

    }
}
