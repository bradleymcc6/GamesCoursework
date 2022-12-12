using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;
public class Toggle : MonoBehaviour
{

    [SerializeField]
    ToggleGroup toggleGroup;
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        /*   if (toggle.isOn==true)
           {
               PlayerPrefs.SetInt("Easy", 1);

                   } 

                   else
                   {
               PlayerPrefs.SetInt("Easy", 0);
                   }*/
        gameMode();
    }

    void gameMode()
    {
        UnityEngine.UI.Toggle toggle = toggleGroup.ActiveToggles().FirstOrDefault();
        Debug.Log("It worked! " + toggle.gameObject.name);
        if (toggle.gameObject.name == "EasyToggle")
        {
            PlayerPrefs.SetInt("Easy", 1);
            Debug.Log(PlayerPrefs.GetInt("Easy"));

        }
        else
        {
            PlayerPrefs.SetInt("Easy", 0);
            Debug.Log(PlayerPrefs.GetInt("Easy"));
        }
    }
}

