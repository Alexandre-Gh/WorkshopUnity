using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // private Coroutine _loadSceneCoroutine;

    [SerializeField] private GameObject _loadingScreenObject;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(SceneAsset scene)
    {
        StartCoroutine(LoadSceneCoroutine(scene));
        // SceneManager.LoadScene(scene.name);
    }

    IEnumerator LoadSceneCoroutine(SceneAsset scene)
    {
        _loadingScreenObject.SetActive(true);
        AsyncOperation progress = SceneManager.LoadSceneAsync(scene.name, LoadSceneMode.Single);

        while (!progress.isDone)
        {
            yield return null;
        }

        _loadingScreenObject.SetActive(false);
        yield return null;
    }



}
