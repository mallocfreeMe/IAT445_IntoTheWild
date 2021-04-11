using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utility
{
    public class ChangeScene : MonoBehaviour
    {
        [SerializeField] private string sceneName; 
    
        private void OnTriggerEnter(Collider other)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
