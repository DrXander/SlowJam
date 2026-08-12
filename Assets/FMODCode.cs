using UnityEngine;

public class FMODCode : MonoBehaviour
{

    [SerializeField] private FMODUnity.EventReference eventReference;
    
    public void PlaySound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(eventReference, transform.position);
    }

    void Start()
    {
        PlaySound();
    }
}