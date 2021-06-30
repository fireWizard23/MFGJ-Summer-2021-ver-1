using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager myInstance;
    public static GameManager Instance
    {
        get
        {
            if (myInstance == null)
                myInstance = new GameManager();
            return myInstance;
        }
    }

    public bool hideMouse = false;
    public bool confineMouse = false;


    void Start()
    {
        myInstance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(hideMouse)
        {
            Cursor.visible = false;
        }
        else
        {
            Cursor.visible = true;
        }
        if (confineMouse)
        {
            Cursor.lockState = CursorLockMode.Confined;
        } else if(Cursor.lockState != CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Gameplay");
        }
    } 
}
