using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class ResourcesManager : MonoBehaviour
{
    [Header("Images")]
    [SerializeField] public Sprite noStars;
    [SerializeField] public Sprite oneStars;
    [SerializeField] public Sprite twoStars;
    [SerializeField] public Sprite threeStars;
    [SerializeField] public Sprite easyUnlocked;
    [SerializeField] public Sprite mediumUnlocked;
    [SerializeField] public Sprite hardUnlocked;
    [SerializeField] public Sprite easyLocked;
    [SerializeField] public Sprite mediumLocked;
    [SerializeField] public Sprite hardLocked;
    [Header("Audio")]
    [SerializeField] AudioClip wrongAction;
    [SerializeField] AudioClip correctAction;
    [SerializeField] AudioClip levelClear;

    public static ResourcesManager instance = null;
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
        DontDestroyOnLoad(gameObject);
    }

    public void playWrongSFX()
    {
        GetComponent<AudioSource>().clip = wrongAction;
        GetComponent<AudioSource>().Play();
    }

    public void playCorrectSFX()
    {
        GetComponent<AudioSource>().clip = correctAction;
        GetComponent<AudioSource>().Play();
    }

    public void playLevelClearSFX()
    {
        GetComponent<AudioSource>().clip = levelClear;
        GetComponent<AudioSource>().Play();
    }
}
