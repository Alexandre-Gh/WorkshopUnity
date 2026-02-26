using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuLogic : MonoBehaviour
{
    [SerializeField] private SceneAsset _gameScene;

    public void Play()
    {
        GameManager.Instance.LoadScene(_gameScene);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
