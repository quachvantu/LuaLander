using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoad
{
    public enum Scene
    {
        Menu,
        Game,
        GameOver,
    }

    public static void LoadScene(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());

    }
}

