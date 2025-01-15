using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Win : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(ExitScene());
    }

    IEnumerator ExitScene()
    {
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene(0);
    }
}
