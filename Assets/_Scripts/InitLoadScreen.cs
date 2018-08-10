using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitLoadScreen : MonoBehaviour
{
    public float timeToLoad = 3f; //Time to wait for before enabling the loading screen
    //Need to create this screen independent, I am doing this so the screen doesnt get enabled after
    //the next scene has loaded
    void Start()
    {
        Invoke("EnableLoadingScreen", timeToLoad);
    }
    void EnableLoadingScreen()
    {
        //Enabling the first child which happens to be loading screen :)
        transform.GetChild(0).gameObject.SetActive(true);
    }
}
