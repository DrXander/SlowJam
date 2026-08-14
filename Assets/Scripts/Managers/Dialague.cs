using TMPro;
using UnityEngine;
using Ink.Runtime;

public class Dialague : MonoBehaviour
{
    [Header("DialagueUI")]
    [SerializeField] private GameObject dialagueUI;
    [SerializeField] private TextMeshProUGUI dialagueText;

    private Story currentStory;
    private bool isDialaguePlaying;

    private static Dialague _instance;


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        _instance = this;
    }

    public static Dialague GetInstance()
    {
        return _instance;
    }

    private void Start()
    {
        dialagueUI.SetActive(false);
        isDialaguePlaying = false;
    }

    public void EnterDialogueMode(TextAsset InkJSON)
    {
        currentStory = new Story(InkJSON.text);
        isDialaguePlaying = true;
        dialagueUI.SetActive(true);
        ContinueStory();
    }

   private void ExitDialogueMode()
    {
        isDialaguePlaying = false;
        dialagueUI.SetActive(false);
        dialagueText.text = "";
    }
    private void Update()
    {
        if (!isDialaguePlaying)
            return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentStory.canContinue)
            {
                dialagueText.text = currentStory.Continue();
            }
            else
            {
                ExitDialogueMode();
            }
        }
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            dialagueText.text = currentStory.Continue();
        }
        else
        {
            ExitDialogueMode();
        }
    }
}
