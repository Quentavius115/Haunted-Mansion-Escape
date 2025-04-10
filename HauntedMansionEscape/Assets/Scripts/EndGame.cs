using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    float timeLimit = 15f * 60;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TimerCoroutine());
        StartCoroutine(RandomSound());
    }


    private System.Collections.IEnumerator TimerCoroutine()
    {

        // Add button check here to skip the return loop?
        yield return new WaitForSeconds(timeLimit);

        // Do something after time runs out
        Debug.Log("Timer finished!");
        GameOver();
    }

    private IEnumerator RandomSound()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(30f, 60f));
            SoundManager.Instance.Play(SoundType.AMBIENCE);
        }
    }

    void GameOver()
    {
        SceneManager.LoadScene("Actual Scene");
    }
}


