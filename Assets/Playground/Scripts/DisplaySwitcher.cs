using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplaySwitcher : MonoBehaviour
{
    int dispCounter = 0;
    // Start is called before the first frame update
    void Start()
    {
        Screen.fullScreen = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            dispCounter = (dispCounter + 1) % Display.displays.Length;
            Debug.Log("Switch to " + dispCounter);
            if (!Display.displays[dispCounter].active)
            {
                Display.displays[dispCounter].Activate();
            }
            Camera.main.targetDisplay = dispCounter;
            Screen.fullScreen = true;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

    }
}
