using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CombaManager : MonoBehaviour
{
    private const string ATTACKABLE = "Attackable";

    [SerializeField]
    private CombatPanelAnimator combatPanelAnimator;

    [SerializeField]
    private CircularList<CharacterController> participants = new();

    [SerializeField]
    private CharacterController currentParticipant;

    [SerializeField]
    private ChosenOneController chosenOneController;

    private bool combatActive = false;

    [SerializeField]
    private int combatArea = 25;

    [SerializeField]
    private AudioClip startCombatSound;

    [SerializeField]
    private AudioClip getCombatTurnSound;

    [SerializeField]
    private AudioClip passCombatTurnSound;

    [SerializeField]
    private AudioListener audioListener;

    [SerializeField]
    private CursorManager cursorManager;

    private AudioSource audioSource;

    private Action action;

    [SerializeField]
    private List<Image> actionPoints = new();

    [SerializeField]
    private List<Image> enemyActionPoints = new();

    void Update()
    {
        var allNpcDead = participants.Where(p => p is not ChosenOneController)
            .All(p => p.IsDead());

        if (combatActive && allNpcDead)
        {
            combatPanelAnimator.EnableCmbt();
        }
        UpdateActionPoints();
        action?.Invoke();
    }

    private void Start()
    {
        if (audioListener != null)
        {
            audioSource = audioListener.GetComponent<AudioSource>();
        }
        DisableActionPoints();
    }

    public void StartCombat(CharacterController initiator)
    {
        if (!combatActive)
        {
            this.participants.Add(initiator);

            Collider[] hitColliders = Physics.OverlapSphere(transform.position, combatArea);
            List<CharacterController> combatParticipants = hitColliders.OfType<Collider>()
                .ToList()
                .Where(c => c.gameObject.CompareTag(ATTACKABLE)
                    && !EqualsInstanceID(c.gameObject, initiator.gameObject))
                .Select(c => c.gameObject.GetComponent<CharacterController>())
                .ToList();

            combatParticipants.ForEach(p => this.participants.Add(p));

            combatActive = true;

            currentParticipant = this.participants.Find(p => p.IsCombatTurn());
            participants.ForEach(p => p.GetCharacter().InitCombatActionPoints());
            participants.ForEach(p => p.GetCharacter().ChangeStateToWaitingForTurn());

            PlayAudioClipOneShot(startCombatSound);

            combatPanelAnimator?.DisableCmbt();
            combatPanelAnimator?.StartOpenPanelAnimation();

            Debug.Log("Combat has been started");

            cursorManager.StartWtach();

            action = StartCombat;
        }
    }

    private void StartCombat()
    {
        if (!audioSource.isPlaying)
        {
            cursorManager.StopWatch();
            DisableAction();
            PlayAudioClipOneShot(getCombatTurnSound);
            currentParticipant.Character.GetTurnInCombat();
        }
    }

    private void UpdateActionPoints()
    {
        if (combatActive)
        {
            if (currentParticipant is ChosenOneController)
            {
                DisableEnemyActionPoints();

                var chosenOne = currentParticipant as ChosenOneController;
                var character = chosenOne.Character;
                if (character.GetCombatActionPoints() > 10)
                {
                    foreach (var actionPoint in actionPoints)
                    {
                        actionPoint.gameObject.SetActive(true);
                    }
                }
                else
                {
                    for (int i = 0; i < character.GetCombatActionPoints(); i++)
                    {
                        actionPoints[i].gameObject.SetActive(true);
                    }
                    for (int i = character.GetCombatActionPoints(); i < 10; i++)
                    {
                        actionPoints[i].gameObject.SetActive(false);
                    }
                }
            }
            else
            {
                DisableChosenOneActionPoints();
                foreach (var enemyActionPoint in enemyActionPoints)
                {
                    enemyActionPoint.gameObject.SetActive(true);
                }
            }
        }
    }

    private void DisableActionPoints()
    {
        DisableEnemyActionPoints();
        DisableChosenOneActionPoints();
    }

    private void DisableEnemyActionPoints()
    {
        foreach (var enemyActionPoint in enemyActionPoints)
        {
            enemyActionPoint.gameObject.SetActive(false);
        }
    }

    private void DisableChosenOneActionPoints()
    {
        foreach (var actionPoint in actionPoints)
        {
            actionPoint.gameObject.SetActive(false);
        }
    }

    private bool EqualsInstanceID(GameObject character1, GameObject character2)
    {
        return character1.GetInstanceID().Equals(character2.GetInstanceID());
    }

    public ChosenOneController GetChosenOne() => this.chosenOneController;

    public void PassTurnToNext()
    {
        if (currentParticipant is ChosenOneController)
        {
            combatPanelAnimator.ToggleTurn();
            combatPanelAnimator.SetArmorClass();
        }

        currentParticipant.ChangeCharacterStateToWaitingForTurn();
        currentParticipant = participants.GetNext();
        currentParticipant.GetTurnInCombat();

        if (currentParticipant is NpcController npc)
        {
            cursorManager.StartWtach();

            npc.TakeCombatTurn();
            PlayAudioClipOneShot(passCombatTurnSound);
        }

        if (currentParticipant is ChosenOneController)
        {
            combatPanelAnimator.ToggleTurn();
            cursorManager.StopWatch();
            PlayAudioClipOneShot(getCombatTurnSound);
            combatPanelAnimator.SetArmorClass();
        }
    }

    private void PlayAudioClipOneShot(AudioClip audioClip)
    {
        if (audioClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }

    public bool IsCombatActive() => combatActive;

    private void DisableAction() => action = () => { };

    public void EndCombat()
    {
        combatPanelAnimator.StartClosePanelAnimation();
        combatActive = false;
        chosenOneController.ChangeCharacterStateToTraveling();
        participants.Clear();

        PlayAudioClipOneShot(getCombatTurnSound);
        PlayAudioClipOneShot(startCombatSound);

        DisableActionPoints();
    }

    public void RemoveParticipant(CharacterController participant)
    {
        if (participant != null)
        {
            participants.Remove(participant);
        }
    }

    public void CalculateHitPoints()
    {
        combatPanelAnimator.StartReduceHealthPoints();
    }

    public void SetArmorClass()
    {
        combatPanelAnimator.SetArmorClass();
    }
}
