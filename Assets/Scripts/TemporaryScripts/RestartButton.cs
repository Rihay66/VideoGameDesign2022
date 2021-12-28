using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    public void restart()
    {
        print("Restarting!");
        SceneManager.LoadScene(0);
    }
}
