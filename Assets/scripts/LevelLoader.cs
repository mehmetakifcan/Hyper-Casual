using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader Current;
    private Scene _lastLoadScene;
    // Start is called before the first frame update


    void Start()
    {
        Current = this;
        GameObject.FindObjectOfType<AdController>().InitializeAds();

        ChangeLevel("Level " + PlayerPrefs.GetInt("currentLevel"));


    }

    public void ChangeLevel(string sceneName)
    {
        StartCoroutine(ChangeScene(sceneName));
        

    }

    IEnumerator ChangeScene(string sceneName)
    {
        if (_lastLoadScene.IsValid())
        {
            SceneManager.UnloadSceneAsync(_lastLoadScene);
            bool sceneUnloaded = false;
            while (!sceneUnloaded)
            {
                sceneUnloaded = !_lastLoadScene.IsValid();
                yield return new WaitForEndOfFrame();
            }
        }

        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        bool sceneLoaded = false;
        while (!sceneLoaded)
        {
            _lastLoadScene = SceneManager.GetSceneByName(sceneName);
            sceneLoaded = _lastLoadScene != null && _lastLoadScene.isLoaded;
            yield return new WaitForEndOfFrame();
        }



    }



}
    
