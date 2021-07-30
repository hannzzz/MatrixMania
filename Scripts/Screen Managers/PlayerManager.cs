using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    string playerName = "Player"; //Default
    [SerializeField] InputField nameField;
    public void confirmAction()
    {
        if(nameField.text.Trim().Length > 0)
        {
            playerName = nameField.text;
        }
        else
        {
            playerName = "Player";
        }

        //Save player's name
        PlayerPrefs.SetString("playerName", playerName);
        //Allow's showing hint on start
        PlayerPrefs.SetInt("showHint", 1);
    }
}
