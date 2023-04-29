using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public GameObject text1;
    public GameObject text2;
    public void LevelReload()
    {
        StartCoroutine(ReloadLevel());
    }

    private IEnumerator ReloadLevel()
    {
        yield return new WaitForSeconds(1.0f); // Wait for 1 second before reloading the level
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //text1 == enabled;
        //text2 == Disable;
    }
    public void NextLevel()
    {
         int nextSceneIndex = SceneManager.GetActiveScene().buildIndex+1;
         if (SceneManager.sceneCount >=0)
            {
                SceneManager.LoadScene(nextSceneIndex);
            }
    }
   
}