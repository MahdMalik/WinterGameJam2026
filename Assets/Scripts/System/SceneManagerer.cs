using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
public class SceneManagerer : MonoBehaviour
{
    public static SceneManagerer instance;
    

    public float volume;
    public bool volumeChanging;
    public float currentVolume = 0.0f;
    [SerializeField] GameObject MusicManagement = null;
    [SerializeField] GameObject PlayerObject = null;
    [SerializeField] GameObject Initial;
    public bool volumeBarsVisible = false;

    //Pause Menu Objects
    [SerializeField] GameObject leftVolumeBar;
    [SerializeField] GameObject rightVolumeBar;
    [SerializeField] GameObject pauseScreen;

    public float SetSFXVolume = 0.3f;
    public Sound[] SFXSounds;
    [SerializeField] private AudioSource SFXSource;
    [SerializeField] private bool walkingSoundCooldown;

    //This checks if another scene manager exists here and deletes it if so.
    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    private void Start() {
        //This sets up the texture used for the screen transition.
        Initializer.RT.Release();
        Initializer.RT.width = 1920;
        Initializer.RT.height = 1080;
        Initializer.RT.Create();
        //Sets volume to start at 30%.
        if (SceneManager.GetActiveScene().buildIndex == 0) {
            volume = 0.3f;
        }
        //Finds the music manager and player. Sets the screen transition to off and fades in music.
        MusicManagement = GameObject.Find("MusicManager");
        leftVolumeBar = GameObject.Find("VolumeChangingSlider");
        rightVolumeBar = GameObject.Find("SFXVolumeChangingSlider");
        pauseScreen = GameObject.Find("pauseScreen");
        Initializer.PixelatedPanel.SetActive(false);
        Initializer.PixelCamera.gameObject.SetActive(false);
        Initializer.PixelCamera.Render();
        Initializer.playerMoving = false;
        Initializer.worldFrozen = false;
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
            yield return new WaitForSeconds(0.045f);
        }
        currentVolume = 0.0f;
    }


    public void Next() {
        StartCoroutine(GoToNextScene());
    }

    public void VolumeBars() {
        if (volumeBarsVisible) {
            StartCoroutine(BringOutBars());
        } else {
            StartCoroutine(BringInBars());
        }
    }

    private IEnumerator BringInBars() {
        for (int i = 0; i < 40; i++) {
        leftVolumeBar.transform.position = new Vector3(leftVolumeBar.transform.position.x + 4.0f, leftVolumeBar.transform.position.y, leftVolumeBar.transform.position.z);
        rightVolumeBar.transform.position = new Vector3(rightVolumeBar.transform.position.x - 4.0f, rightVolumeBar.transform.position.y, rightVolumeBar.transform.position.z);
        yield return new WaitForSeconds(0.03f);
        }
        volumeBarsVisible = true;
    }
    private IEnumerator Pause() {
        yield return new WaitForSeconds(0.03f);
    }
    private IEnumerator BringOutBars() {
        for (int i = 0; i < 40; i++) {
        leftVolumeBar.transform.position = new Vector3(leftVolumeBar.transform.position.x - 4.0f, leftVolumeBar.transform.position.y, leftVolumeBar.transform.position.z);
        rightVolumeBar.transform.position = new Vector3(rightVolumeBar.transform.position.x + 4.0f, rightVolumeBar.transform.position.y, rightVolumeBar.transform.position.z);
        yield return new WaitForSeconds(0.03f);
        }
        volumeBarsVisible = false;
    }



    private IEnumerator GoToNextScene() {
        if (volumeBarsVisible) {
            StartCoroutine(BringOutBars());
            yield return new WaitForSeconds(1.2f);
        }
        Initializer.playerMoving = false;
        Initializer.worldFrozen = true;
        if (SceneManager.GetActiveScene().buildIndex == 1) {
            PlaySFX("Death");
        } else {
            PlaySFX("Click");
        }
        //Sets the screen transition on.
        Initial.GetComponent<Initializing>().Initialization();
        Initializer.PixelatedPanel.SetActive(true);
        Initializer.PixelCamera.Render();
        Initializer.PixelCamera.gameObject.SetActive(true);
        StartCoroutine(FadeOutMusic());
        //Lowers screen resolution.
        for (int i = 1; i < 41; i++) {
            AdjustRenderTextureSize(i, i);
            yield return new WaitForSeconds(0.045f);
        }
        for (int i = 7; i < 27; i++) {
            AdjustRenderTextureSize((i * i), (i * i));
            yield return new WaitForSeconds(0.022f);
        }
        //Sends you to the next scene.
        if (SceneManager.GetActiveScene().buildIndex == 2) {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex - 1);
        } else {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        }
        StartCoroutine(FadeInMusic());
        //Waits until the player is found before continuing.
        for (int k = 1; k == 1;) {
            Initial.GetComponent<Initializing>().Initialization();
            PlayerObject = GameObject.Find("Player");
            yield return new WaitForSeconds(0.05f);
            Debug.Log("Trying to Find.");
            if ((Initializer.PixelCamera != null) && (PlayerObject != null) && SceneManager.GetActiveScene().buildIndex == 1) {
                k = 10;
            }
        }
        //Makes sure the transition camera is on and starts raising resolution again.
        Initializer.PixelatedPanel.SetActive(true);
        Initializer.PixelCamera.gameObject.SetActive(true);
        Initializer.PixelCamera.transform.position = new Vector3 (PlayerObject.transform.position.x, PlayerObject.transform.position.y, PlayerObject.transform.position.z - 20.0f);
        for (int i = 27; i > 7; i--) {
            Initial.GetComponent<Initializing>().Initialization();
            AdjustRenderTextureSize((i * i), (i * i));
            yield return new WaitForSeconds(0.022f);
            Initializer.PixelCamera.transform.position = new Vector3 (PlayerObject.transform.position.x, PlayerObject.transform.position.y, PlayerObject.transform.position.z - 20.0f);
        }
        for (int i = 41; i > 0; i--) {
            AdjustRenderTextureSize(i, i);
            yield return new WaitForSeconds(0.045f);
        }
        //Turns the transition off.
        Initial.GetComponent<Initializing>().Initialization();
        Initializer.PixelatedPanel.SetActive(false);
        Initializer.PixelCamera.gameObject.SetActive(false);
        Initializer.worldFrozen = false;
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
        //Used to set volume for the music and SFX. Has to be stored here so it carries across scenes.
        SFXSource.volume = Initializer.SFXVolume;
        if (!volumeChanging) {
            currentVolume = volume;
        }
        MusicManagement = GameObject.Find("MusicManager");
        MusicManagement.GetComponent<AudioManager>().setVolume(currentVolume);
        //If P is pressed, go to the next scene. Used instead of a button because buttons are stupid.
        if (Input.GetKeyDown(KeyCode.P)) {
            Next();
        }
        if (Input.GetKeyDown(KeyCode.Escape) && (SceneManager.GetActiveScene().buildIndex != 0)) {
            StartCoroutine(Pause());
        }
        //Moves the screen transition camera to the player at all times.
        if (PlayerObject != null) {
            Initializer.PixelCamera.transform.position = new Vector3 (PlayerObject.transform.position.x, PlayerObject.transform.position.y, PlayerObject.transform.position.z - 20.0f);
        }
        //Plays the Walking SFX
        if (Initializer.playerMoving == true && walkingSoundCooldown == false) {
            StartCoroutine(WalkRepeat());
            PlaySFX("Step");
            // Debug.Log("AAAAA");
        }
    }

    //Lets this script play SFX.
    public void PlaySFX(string name) {
        Sound s = Array.Find(SFXSounds, x => x.name == name);
        if(s == null) {
            Debug.Log("No Sounds");
        } else {
            SFXSource.clip = s.clip;
            SFXSource.Play();
        }
    }

    private IEnumerator WalkRepeat() {
        walkingSoundCooldown = true;
        PlaySFX("Step");
        yield return new WaitForSeconds(0.1f);
        walkingSoundCooldown = false;
    }
}
