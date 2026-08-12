using UnityEngine;

public class FmodManager : MonoBehaviour
{
    
    [field: Header("day 1 music")]
    [field: SerializeField] public FMODUnity.EventReference Day1Music { get; private set; }
    public static FmodManager Instance { get; private set; }

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


}
