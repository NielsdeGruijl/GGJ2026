using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject characterSelectionScreen;

    private void Awake()
    {
        characterSelectionScreen.SetActive(false);
    }

    public void ShowCharacterSelection()
    {
        characterSelectionScreen.SetActive(true);
    }
    
}
