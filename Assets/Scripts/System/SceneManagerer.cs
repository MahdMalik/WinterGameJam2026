using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManagerer : MonoBehaviour
{
    public static SceneManagerer instance;
    

    public float volume;
    public bool volumeChanging = true;
    public float currentVolume = 0.0f;
    [SerializeField] GameObject MusicManagement;

    //This checks if another scene manager exists here and deletes it if so.
    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }


        if (SceneManager.GetActiveScene().buildIndex == 0) {
            volume = 0.3f;
        }
        MusicManagement = GameObject.Find("MusicManager");
        StartCoroutine(FadeInMusic());
        
    }
    
    IEnumerator FadeInMusic() {
        volumeChanging = true;
        currentVolume = 0.0f;
        for (int i = 0; i < 50; i++) {
            currentVolume += volume/50.0f;
            yield return new WaitForSeconds(0.03f);
        }
        currentVolume = volume;
        volumeChanging = false;
    }


    public void Next() {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void volumeSet(System.Single sliderValue) {
        if (!volumeChanging) {
            volume = sliderValue;
        }
    }

    void Update()
    {
        if (!volumeChanging) {
            currentVolume = volume;
        }
        MusicManagement.GetComponent<AudioManager>().setVolume(currentVolume);
    }
}
