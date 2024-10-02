using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex); // An universal line that you can use to get to any scene if you use the right input for the scene number
    }

    public void Quit()
    {
        Application.Quit(); // The code to quit the game
        Debug.Log("Quit!");
    }

}