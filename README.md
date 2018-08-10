# asynchronous-scene-loading
A small package that provides asynchronous screen loading with fade-in effect

Pretty simple to use, put the SceneLoader prefab in the first scene of your game.
Afterwards, just call: 
```
SceneLoader.instance.LoadScene(1); //Build index
```
or call via scene name
```
SceneLoader.instance.LoadScene("SceneName");
```
To assign loading screen, just pull the LoadingScreenUI object on your scene and replace its child with your own loading screen/canvas element
