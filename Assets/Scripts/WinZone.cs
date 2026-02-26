using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WinZone : MonoBehaviour
{
    [SerializeField] private SceneAsset _mainMenuScene;
    [SerializeField] private GameObject _winCanvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>() != null)
        {
            _winCanvas.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReturnToMenu()
    {
        GameManager.Instance.LoadScene(_mainMenuScene);
    }
}
