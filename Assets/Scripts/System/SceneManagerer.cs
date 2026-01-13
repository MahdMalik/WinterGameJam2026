using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
public class SceneManagerer : MonoBehaviour
{
    public static SceneManagerer instance;
    

    public float volume;
    public bool volumeChanging = true;
    public float currentVolume = 0.0f;
    [SerializeField] GameObject MusicManagement = null;
    [SerializeField] GameObject PlayerObject = null;
    [SerializeField] GameObject Initial;

    public float SetSFXVolume;
    public Sound[] SFXSounds;
    [SerializeField] private AudioSource SFXSource;

    //This checks if another scene manager exists here and deletes it if so.
    private void Awake() {
        Initializer.RT.Release();
        Initializer.RT.width = 1920;
        Initializer.RT.height = 1080;
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
        PlayerObject = GameObject.Find("Player");
        Initializer.PixelatedPanel.SetActive(false);
        Initializer.PixelCamera.gameObject.SetActive(false);
        Initializer.PixelCamera.Render();
        StartCoroutine(FadeInMusic());
    }
    
    IEnumerator FadeInMusic() {
        volumeChanging = true;
        currentVolume = 0.0f;
        for (int i = 0; i < 50; i++) {
            currentVolume += volume/50.0f;
            Initializer.SFXVolume += SetSFXVolume/50.0f;
            yield return new WaitForSeconds(0.045f);
        }
        currentVolume = volume;
        volumeChanging = false;
    }
    IEnumerator FadeOutMusic() {
        volumeChanging = true;
        for (int i = 0; i < 50; i++) {
            currentVolume -= volume/50.0f;
            Initializer.SFXVolume -= SetSFXVolume/50.0f;
            Debug.Log(currentVolume);
            yield return new WaitForSeconds(0.045f);
        }
        currentVolume = 0.0f;
    }


    public void Next() {
        StartCoroutine(GoToNextScene());
    }


    private IEnumerator GoToNextScene() {
        Initializer.PixelatedPanel.SetActive(true);
        Initializer.PixelCamera.Render();
        Initializer.PixelCamera.gameObject.SetActive(true);
        StartCoroutine(FadeOutMusic());
        for (int i = 1; i < 41; i++) {
            AdjustRenderTextureSize(i, i);
            yield return new WaitForSeconds(0.045f);
        }
        for (int i = 7; i < 27; i++) {
            AdjustRenderTextureSize((i * i), (i * i));
            yield return new WaitForSeconds(0.022f);
        }
        if (SceneManager.GetActiveScene().buildIndex == 2) {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex - 1);
        } else {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        }
        StartCoroutine(FadeInMusic());
        PlayerObject = GameObject.Find("Player");
        for (int i = 27; i > 7; i--) {
            AdjustRenderTextureSize((i * i), (i * i));
            yield return new WaitForSeconds(0.022f);
        }
        for (int i = 41; i > 0; i--) {
            AdjustRenderTextureSize(i, i);
            yield return new WaitForSeconds(0.045f);
        }
        Initial.GetComponent<Initializing>().Initialization();
        Initializer.PixelatedPanel.SetActive(false);
        Initializer.PixelCamera.gameObject.SetActive(false);
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
            PlaySFX("Click");
        }
    }

    public void SFXvolumeSet(System.Single sliderValue) {
        if (!volumeChanging) {
            SetSFXVolume = sliderValue;
            Initializer.SFXVolume = SetSFXVolume;
            PlaySFX("Click");
        }
    }

    void Update()
    {
        SFXSource.volume = Initializer.SFXVolume;
        if (!volumeChanging) {
            currentVolume = volume;
        }
        MusicManagement = GameObject.Find("MusicManager");
        MusicManagement.GetComponent<AudioManager>().setVolume(currentVolume);
        if (Input.GetKeyDown(KeyCode.P)) {
            Next();
        }
        if (PlayerObject != null) {
            Initializer.PixelCamera.transform.position = new Vector3 (PlayerObject.transform.position.x, PlayerObject.transform.position.y, PlayerObject.transform.position.z - 20.0f);
        }
    }

    public void PlaySFX(string name) {
        Sound s = Array.Find(SFXSounds, x => x.name == name);
        if(s == null) {
            Debug.Log("No Sounds");
        } else {
            SFXSource.clip = s.clip;
            SFXSource.Play();
        }
    }
}
