using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour {
    [HideInInspector]
    public static SceneLoader instance;
    public GameObject fadePanel; // This panel is used for the fade-in effect
    public GameObject loadingScreen; //Loading screen prefab to be instantiated
    private GameObject loadingScreenHolder; //Need this to hold reference to instantiated object
    public float loadingTimer; //The amount of time after which loading screen should be instantiated
    public int lastSceneID; //Caching the id of last scene on every scene change just incase it gets instantiated after the next scene is loaded
    // Use this for initialization
    #region Methods
    void Awake () {
        
        //Need this object and script in all scenes for the loading screen
        DontDestroyOnLoad(gameObject);
		if(instance == null)
        {
            instance = this;
        }
    }
    void EnableLoadingScreen()
    {
        loadingScreenHolder = Instantiate(loadingScreen);
    }
    #endregion
    #region Wrapper Methods
    /// <summary>
    /// Loads scene by build index
    /// </summary>
    /// <param name="buildIndex">Scene's Build Index</param>
    public void LoadScene(int buildIndex)
    {
        Instantiate(fadePanel);
        Invoke("EnableLoadingScreen", loadingTimer);
        StartCoroutine(LoadYourAsyncScene(buildIndex));
    }
    /// <summary>
    /// Loads scene by name
    /// </summary>
    /// <param name="sceneName">Scene name</param>
    public void LoadScene(string sceneName)
    {
        Instantiate(fadePanel);
        Invoke("EnableLoadingScreen", loadingTimer);
        StartCoroutine(LoadYourAsyncScene(sceneName));
    }
    #endregion


    #region Coroutines for Async Loading
    /// <summary>
    /// Loads scene asynchronously using scene ID
    /// </summary>
    /// <param name="buildIndex">Scene's Build Index</param>
    /// <returns></returns>
    public IEnumerator LoadYourAsyncScene(int buildIndex)
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(buildIndex);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
    /// <summary>
    /// Loads scene asynchronously using scene name
    /// </summary>
    /// <param name="sceneName">Scene name</param>
    /// <returns></returns>
    public IEnumerator LoadYourAsyncScene(string sceneName)
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
    #endregion
    #region Events
    void OnEnable()
    {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }
    void OnDisable()
    {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }
    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        //If next scene is loaded, destroy loading screen object
        if(lastSceneID != scene.buildIndex)
        {
            Destroy(loadingScreenHolder);
        }
        lastSceneID = scene.buildIndex;
    }
    #endregion
}
