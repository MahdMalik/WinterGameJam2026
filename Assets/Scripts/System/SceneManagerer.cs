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
    [SerializeField] GameObject MusicManagement = null;

    //This checks if another scene manager exists here and deletes it if so.
    private void Awake() {
        Initializer.RT.Release();
        Initializer.RT.height = 1920;
        Initializer.RT.width = 1080;
        Initializer.RT.Create();
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
        StartCoroutine(GoToNextScene());
    }


    private IEnumerator GoToNextScene() {
        Initializer.PixelatedPanel.SetActive(true);
        Initializer.PixelCamera.gameObject.SetActive(true);
        for (int i = 1; i < 41; i++) {
            AdjustRenderTextureSize(i, i);
            yield return new WaitForSeconds(0.03f);
        }
        for (int i = 7; i < 27; i++) {
            AdjustRenderTextureSize((i * i), (i * i));
            yield return new WaitForSeconds(0.015f);
        }
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void AdjustRenderTextureSize(int width, int height) {
        Initializer.RT.Release();
        Initializer.RT.width = (int)(1920/width);
        Initializer.RT.height = (int)(1080/height);
        Initializer.RT.Create();
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
        MusicManagement = GameObject.Find("MusicManager");
        MusicManagement.GetComponent<AudioManager>().setVolume(currentVolume);
    }
}
