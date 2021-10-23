using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;

public class GameManager_Temporary : MonoBehaviour
{
    public static GameManager_Temporary instance;

    public Animator anim;
    public GameObject transitionUI;

    private void Awake()
    {
        if(instance != this)
        {
            instance = this;
        }
        anim = transitionUI.GetComponent<Animator>();
    }

    public void restartLevel()
    {
        // Is called to restart the same scene
        Scene sence = SceneManager.GetActiveScene();
        SceneManager.LoadScene(sence.name);
    }

    public void EndOfGame()
    {
        // Called to show a end screen
        anim.SetTrigger("In");
        StartCoroutine(ChangeScene());
    }

    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene(1);
    }
}
