using UnityEngine;

public class Pause : MonoBehaviour
{
    public static bool IsPaused = false;
    static public void PauseGame()
    {
        Time.timeScale = 0f;
        IsPaused=true;
    }
    static public void UnpauseGame()
    {
        Time.timeScale = 1.0f;
        IsPaused = false;
    }
}
