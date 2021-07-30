using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] bool canTransition;
    [SerializeField] int sceneIndex;
    Animator animator;
    [SerializeField] string animationIn;
    [SerializeField] string animationOut;
    void Start()
    {
        animator = transform.GetComponent<Animator>();
    }
    
    public void loadScene()
    {
        if (canTransition)
        StartCoroutine(LoadSceneAFterTransition(animationIn));
    }

    public void exitScene()
    {
        StartCoroutine(QuitAFterTransition(animationOut));
    }
    
    private IEnumerator LoadSceneAFterTransition(string animTrigger)
    {
        if(animator)
        animator.SetTrigger(animTrigger);
        yield return new WaitForSeconds(0.8f);
        SceneManager.LoadScene(sceneIndex);
    }
    
    private IEnumerator QuitAFterTransition(string animTrigger)
    {
        if(animator)
        animator.SetTrigger(animTrigger);
        yield return new WaitForSeconds(0.8f);
        Application.Quit(0);
    }
}
