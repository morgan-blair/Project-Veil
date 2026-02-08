using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [SerializeField] private MusicLibrary musicLibrary;
    [SerializeField] private AudioSource musicSource;

    private Coroutine currentFade;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlayMusic(string trackName, float fadeDuration = 0.5f)
    {
        AudioClip nextTrack = musicLibrary.GetClipFromName(trackName);
        if (nextTrack == null)
        {
            Debug.LogWarning($"MusicManager: Track '{trackName}' not found!");
            return;
        }

        if (currentFade != null)
            StopCoroutine(currentFade);

        currentFade = StartCoroutine(AnimateMusicCrossfade(nextTrack, fadeDuration));
    }

    public void Stop(float fadeDuration = 0.5f)
    {
        if (musicSource.isPlaying)
        {
            if (currentFade != null)
                StopCoroutine(currentFade);

            StartCoroutine(FadeOutAndStop(fadeDuration));
        }
    }

    private IEnumerator FadeOutAndStop(float fadeDuration)
    {
        float startVolume = musicSource.volume;
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(startVolume, 0f, t / fadeDuration);
            yield return null;
        }

        musicSource.Stop();
        musicSource.clip = null;
        musicSource.volume = 1f; // Reset for next play
    }

    private IEnumerator AnimateMusicCrossfade(AudioClip nextTrack, float fadeDuration)
    {
        if (musicSource.clip == nextTrack)
            yield break; // Prevent replaying same track

        float t = 0f;

        // Fade out current music
        if (musicSource.isPlaying)
        {
            while (t < fadeDuration)
            {
                t += Time.deltaTime;
                musicSource.volume = Mathf.Lerp(1f, 0f, t / fadeDuration);
                yield return null;
            }
        }

        // Switch and fade in
        musicSource.clip = nextTrack;
        musicSource.Play();

        t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(0f, 1f, t / fadeDuration);
            yield return null;
        }

        musicSource.volume = 1f;
    }
}
