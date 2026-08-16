using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;
using static GameManager;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    public class Characters
    {
        public string characterName;
        public Sprite portiart;
        public string[] Nodes;
    }

    [Header("Characters")]
    [SerializeField] private DialogueRunner dialogueRunner;
    [SerializeField] private Image portrait;
    [SerializeField] private Animator portraitAnimator;
    [SerializeField] private Characters[] characterData;

    [Header("Paused menu")]
    [SerializeField] private GameObject PauseMenuUI;

    public static GameManager Instance { get; private set; }
    public GameState GameStates {  get; private set; }

    private void States(GameState newState)
    {
        GameStates = newState;
        switch (GameStates)
        {
            case GameState.GameResume:
                Time.timeScale = 1f;
                if (PauseMenuUI != null) PauseMenuUI.SetActive(false);
                break;
            case GameState.GamePause:
                Time.timeScale = 0f;
                if (PauseMenuUI != null) PauseMenuUI.SetActive(true);
                break;
        }
    }

    public enum GameState
    {
        GameResume,
        GamePause,
    }

    public readonly int[][] CharacterTime =
    {
        new[] {0,1},
        new[] {2,3},
        new[] {1,2},
        new[] {0,3},
        new[] {1,0},
        new[] {2},
    };

    private int days = 0;
    private int charactertalked = 0;
    private int[] visitCount;

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

        visitCount = new int[characterData.Length];
    }

    private void Start()
    {
        States(GameState.GameResume);

        dialogueRunner.onDialogueStart.AddListener(OnTalkStart);
        dialogueRunner.onDialogueComplete.AddListener(OnTalkComplete);

        AudioManager.Instance.SwitchMusic(FmodManager.Instance.GetDayMusic(days));

        CurrentCustomer();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameStates == GameState.GameResume)
            {
                States(GameState.GamePause);
            }
            else if (GameStates == GameState.GamePause)
            {
                States(GameState.GameResume);
            }
        }
    }

    private void OnTalkStart()
    {
        if (portraitAnimator != null) portraitAnimator.SetBool("isTalking", true);
    }


    private void OnTalkComplete()
    {
        if (portraitAnimator != null)
        {
            portraitAnimator.SetBool("isTalking", false);
            portraitAnimator.SetTrigger("Exit");
        }

        charactertalked++;

        if (charactertalked < CharacterTime[days].Length)
        {
            StartCoroutine(NextCustomerAfterDelay());   // was: CurrentCustomer();
        }
        else
        {
            days++;
            charactertalked = 0;

            if (days < CharacterTime.Length)
            {
                AudioManager.Instance.SwitchMusic(FmodManager.Instance.GetDayMusic(days));
                StartCoroutine(NextCustomerAfterDelay()); // was: CurrentCustomer();
            }
            else
            {
                Debug.Log("All days complete.");
            }
        }
    }

    private IEnumerator NextCustomerAfterDelay()
    {
        yield return null;
            new WaitForSeconds(0.2f);                 
                                           
        CurrentCustomer();
    }

    private void CurrentCustomer()
    {
        int index = CharacterTime[days][charactertalked];

        if (characterData == null || index >= characterData.Length)
        {
            Debug.LogError($"GameManager: no character set up for index {index}.");
            return;
        }

        Characters character = characterData[index];

        int visit = visitCount[index];
        if (character.Nodes == null || visit >= character.Nodes.Length)
        {
            Debug.LogError($"GameManager: {character.characterName} has no Yarn node for visit {visit + 1}. " +
                           "Add more node names to their visitNodes array.");
            return;
        }

        if (portrait != null && character.portiart != null)
            portrait.sprite = character.portiart;

        if (portraitAnimator != null)
        {
            portraitAnimator.SetInteger("character", index);
            portraitAnimator.SetTrigger("Enter");              
        }


        string node = character.Nodes[visit];
        visitCount[index]++;
        Debug.Log($"Starting node: {node} for {character.characterName}");
        dialogueRunner.StartDialogue(node);
    }


}
