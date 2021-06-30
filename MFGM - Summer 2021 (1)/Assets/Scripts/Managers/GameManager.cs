using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    } 
}
