using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameOverPanel : MonoBehaviour
{
    [Header("RefButton")]
    [SerializeField] private Button _buttonPlayAgain;
    void Start()
    {
        _buttonPlayAgain.onClick.AddListener(ResetLevel);
    }

    private void ResetLevel()
    {

        SceneManager.LoadScene(SceneManager.loadedSceneCount-1);
    }

  
}
