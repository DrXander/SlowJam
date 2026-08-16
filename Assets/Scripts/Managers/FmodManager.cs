using UnityEngine;

public class FmodManager : MonoBehaviour
{
    [field: Header("AMb bar")]
    [field: SerializeField] public FMODUnity.EventReference AmbBar { get; private set; }

    [field: Header("day 1 music")]
    [field: SerializeField] public FMODUnity.EventReference Day1Music { get; private set; }

    [field: Header("day 2 music")]
    [field: SerializeField] public FMODUnity.EventReference Day2Music { get; private set; }

    [field: Header("day 3 music")]
    [field: SerializeField] public FMODUnity.EventReference Day3Music { get; private set; }

    [field: Header("day 4 music")]
    [field: SerializeField] public FMODUnity.EventReference Day4Music { get; private set; }

    [field: Header("Bottle Click")]
    [field: SerializeField] public FMODUnity.EventReference BottleClick { get; private set; }

    [field: Header("Bottle Glug")]
    [field: SerializeField] public FMODUnity.EventReference BottleGlug { get; private set; }

    [field: Header("Bottle misc 1")]
    [field: SerializeField] public FMODUnity.EventReference BottleMisc1 { get; private set; }

    [field: Header("Bottle misc 2")]
    [field: SerializeField] public FMODUnity.EventReference BottleMisc2 { get; private set; }

    [field: Header("Bottle open")]
    [field: SerializeField] public FMODUnity.EventReference BottleOpen { get; private set; }

    [field: Header("Bottle pour")]
    [field: SerializeField] public FMODUnity.EventReference BottlePour { get; private set; }

    [field: Header("UIclick")]
    [field: SerializeField] public FMODUnity.EventReference UIClick { get; private set; }

    [field: Header("UI hover")]
    [field: SerializeField] public FMODUnity.EventReference UIHover { get; private set; }

    [field: Header("UI Misc")]
    [field: SerializeField] public FMODUnity.EventReference UIMisc { get; private set; }

    [field: Header("UI Misc 2")]
    [field: SerializeField] public FMODUnity.EventReference UIMisc2 { get; private set; }

    [field: Header("UI Money Count")]
    [field: SerializeField] public FMODUnity.EventReference UIMoneyCount { get; private set; }

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

    public FMODUnity.EventReference GetDayMusic(int dayIndex)
    {
        switch (dayIndex)
        {
            case 0: return Day1Music;
                case 1: return Day2Music;
                case 2: return Day3Music;
                default: return Day4Music;
        }
    }

}
