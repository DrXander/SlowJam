using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    private EventInstance MusicEvent;
    private EventInstance AmbBarEvent;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void Start()
    {
        AmbBarEvent = RuntimeManager.CreateInstance(FmodManager.Instance.AmbBar);
        AmbBarEvent.start();
    }

    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }

    public void SwitchMusic(EventReference newMusic)
    {
        StopMusic();
        MusicEvent = RuntimeManager.CreateInstance(newMusic);
        MusicEvent.start();
    }

    public void StopMusic()
    {
        if (MusicEvent.isValid())
        {
            MusicEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            MusicEvent.release();
        }
    }

    private void OnDestroy()
    {
        StopMusic();
        if (AmbBarEvent.isValid())
        {
            AmbBarEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            AmbBarEvent.release();
        }
    }

}