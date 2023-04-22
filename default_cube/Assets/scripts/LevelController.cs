using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public void LevelReload()
    {
        StartCoroutine(ReloadLevel());
    }

    private IEnumerator ReloadLevel()
    {
        yield return new WaitForSeconds(1.0f); // Wait for 1 second before reloading the level
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}