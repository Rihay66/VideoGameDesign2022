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
    private void Update()
    {
        if (instance != this)
        {
            instance = this;
            Debug.Log("Reset Instance!");
            return;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            Debug.Log("Quitting!");
        }
    }
    public void restartLevel()
    {
        // Is called to show a game over    
        SceneManager.LoadScene(2);
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
