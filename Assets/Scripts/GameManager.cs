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
    [SerializeField] private Animator _loadingScreenAnimator;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        _loadingScreenAnimator = _loadingScreenObject.GetComponent<Animator>();
    }

    public void LoadScene(string scene)
    {
        StartCoroutine(LoadSceneCoroutine(scene));
        // SceneManager.LoadScene(scene.name);
    }

    IEnumerator LoadSceneCoroutine(string scene)
    {
        // _loadingScreenObject.SetActive(true);
        _loadingScreenAnimator.SetTrigger("Start");

        yield return new WaitForSeconds(2f);

        AsyncOperation progress = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Single);

        while (!progress.isDone)
        {
            yield return null;
        }

        // _loadingScreenObject.SetActive(false);
        // yield return null;
        _loadingScreenAnimator.SetTrigger("End");
    }



}
