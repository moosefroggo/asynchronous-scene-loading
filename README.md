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
