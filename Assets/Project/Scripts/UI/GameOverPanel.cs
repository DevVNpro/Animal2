using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameOverPanel : MonoBehaviour
{
  
    public void ResetLevel()
    {

        SceneManager.LoadScene(SceneManager.loadedSceneCount-1);
    }

  
}
