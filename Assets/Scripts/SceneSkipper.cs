using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneSkipper : MonoBehaviour
{

    bool dirty = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPressNext()
    {
        if (!dirty)

        {
            dirty = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);


        }
        }
}
