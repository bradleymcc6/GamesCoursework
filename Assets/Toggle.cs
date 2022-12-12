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
  
        gameMode();
    }

    void gameMode()
    {
        UnityEngine.UI.Toggle toggle = toggleGroup.ActiveToggles().FirstOrDefault();
    
        if (toggle.gameObject.name == "EasyToggle")
        {
            PlayerPrefs.SetInt("Easy", 1);
           

        }
        else
        {
            PlayerPrefs.SetInt("Easy", 0);
          
        }
    }
}

