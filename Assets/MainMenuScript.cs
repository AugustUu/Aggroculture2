
using UnityEngine;
using UnityEngine.SceneManagement;
using Button = UnityEngine.UI.Button;

public class MainMenuScript : MonoBehaviour
{
    public Button play;

    public Scene main_scene;
    void Start()
    {
        play.onClick.AddListener(() =>
        {
            Debug.Log("å´®åƒƒå");
            SceneManager.LoadScene("Main(XP)",LoadSceneMode.Single);
            
        });
    }
    
}
