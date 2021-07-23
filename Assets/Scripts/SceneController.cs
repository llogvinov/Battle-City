using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] Animator _animator;
    
    private int _menuSceneIndex = 0;
    private int _currentSceneIndex;

    private float _transitionTime = 3.5f;

    public void TryLoadNextScene()
    {
        _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (_currentSceneIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            StartCoroutine(LoadStage(_currentSceneIndex + 1));
        }
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene(_menuSceneIndex);
        Time.timeScale = 1f;
    }

    public void ReloadScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    private IEnumerator LoadStage(int stageIndex)
    {
        _animator?.SetTrigger("StartTransition");

        yield return new WaitForSeconds(_transitionTime);

        SceneManager.LoadScene(stageIndex);
    }
}
