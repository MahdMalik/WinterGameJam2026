using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManagerer : MonoBehaviour
{
    public static SceneManagerer instance;
    [SerializeField] private GameObject AudioManager;

    //This checks if another scene manager exists here and deletes it if so.
    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
        
    }
    

    void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0) {
            AudioManager.GetComponent<AudioManager>().PlayMusic("Menu");
        } else if (SceneManager.GetActiveScene().buildIndex == 1) {

        }
    }

    public void Next() {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
