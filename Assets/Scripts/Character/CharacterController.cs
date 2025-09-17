using System;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class CharacterController : MonoBehaviour
{
    //Tags
    protected const string GROUND = "Ground";
    protected const string ATTACKABLE = "Attackable";

    [SerializeField]
    protected Character character;

    protected Action action;

    protected CharacterNavigator characterNavigator;

    private Vector3 movingTarget;

    protected Transform interactionTarget;

    protected CharacterAnimator characterAnimator;

    [SerializeField]
    protected CombaManager combatManager;

    [SerializeField]
    private Transform firePoint;

    [SerializeField]
    private float speed = 10;

    protected AudioSource audioSource;

    [SerializeField]
    protected CursorManager cursorManager;

    private new Collider collider;

    [SerializeField]
    private AudioClip defaultHitSound;

    private bool playAttackSound = true;

    [SerializeField]
    protected Logger logger;

    [SerializeField]
    protected bool run = false;

    [SerializeField]
    private int walkSpeed = 2;

    [SerializeField]
    private int runSpeed = 5;

    [SerializeField]
    private bool raiseMuzzle = true;

    protected virtual void Start()
    {
        collider = GetComponent<Collider>();
        audioSource = GetComponent<AudioSource>();
        characterAnimator = new CharacterAnimator(GetComponent<Animator>());

        characterNavigator = CharacterNavigator(GetComponent<NavMeshAgent>(),
            GetComponent<NavMeshObstacle>());

        DisableAction();

        characterNavigator.OnStopMove += OnMovementStop;
        characterNavigator.OnStartMoving += OnStartInteract;

        InitWeapon();
        if (character != null)
        {
            character.ChangeStateToTraveling();
            character.InitArmorClass();
        }
        if (character != null)
        {
            character.ActionDone += OnLogMessage;
        }
    }

    void Update()
    {
        action?.Invoke();
    }

    protected void InitWeapon()
    {
        if (character != null)
        {
            var weapon = character.Weapon;
            if (weapon == null)
            {
                var weaponObjects = GetComponentsInChildren<WeaponObject>(true);
                foreach (var weaponObject in weaponObjects)
                {
                    weaponObject.gameObject.SetActive(false);
                }
                characterAnimator.SetAnimation(CharacterAnimator.DEFAULT, true);
            }
            else
            {
                var weaponObject = GetComponentsInChildren<WeaponObject>(true)
                    .Where(gameObject => gameObject.gameObject.CompareTag(weapon.Name))
                    .FirstOrDefault();
                if (weaponObject != null)
                {
                    weaponObject.gameObject.SetActive(true);
                }
                characterAnimator.SetAnimation(weapon.IdleAnimationName, true);
            }
        }
    }

    protected virtual void AttackTarget(Transform target)
    {
        if (combatManager.IsCombatActive() && character.IsCombatTurn())
        {
            interactionTarget = target;
            action = TurnTowardsTargetForAttack;
        }
    }

    private void TurnTowardsTargetForAttack()
    {
        var rotated = characterNavigator.TurnTowardsTheTarget(transform, interactionTarget.position);
        if (rotated)
        {
            action = Attack;
        }
    }

    protected int CalculateDistance() =>
         (int)Vector3.Distance(interactionTarget.position, transform.position);

    protected virtual void Attack()
    {
        var enemy = interactionTarget.GetComponent<CharacterController>();
        var distance = CalculateDistance();

        if (character.CanAttack(distance) && enemy.IsHitAnimationEnded())
        {
            character.HitDamage(enemy.Character, distance);
            enemy.ToggleAttackHitAnimation();
            characterAnimator.SetAnimation(character.GetAttackAnimationName(), true);
        }
    }

    public void RunEnemyHitAnimation()
    {
        var enemy = interactionTarget.GetComponent<CharacterController>();
        enemy.RunHitAnimation();
        PlayAudioClip(enemy.GetHitSound());
    }

    public AudioClip GetHitSound() => character.GetArmorHitSound() == null ? defaultHitSound
            : character.GetHitSound();

    public bool IsHitAnimationEnded() => characterAnimator.IsHitEnd();

    public void StopHitAnimation()
    {
        characterAnimator.StopHitAnimation();
        characterAnimator.ToggleHitAnimation();
    }

    protected void BurstFireAttack(Transform target)
    {
        if (combatManager.IsCombatActive() && character.IsCombatTurn())
        {
            interactionTarget = target;
            action = TurnTowardsTargetForBurstAttack;
        }
    }

    private void TurnTowardsTargetForBurstAttack()
    {
        var rotated = characterNavigator.TurnTowardsTheTarget(transform, interactionTarget.position);
        if (rotated)
        {
            action = BurstAttack;
        }
    }

    protected virtual void BurstAttack()
    {
        var distance = CalculateDistance();

        if (character.CanAttackWithBurstFire(distance))
        {
            var enemy = interactionTarget.GetComponent<CharacterController>();
            character.HitDamageWithBurstFire(enemy.Character, distance);

            enemy.ToggleAttackHitAnimation();

            characterAnimator.SetAnimation(character.GetAttackAnimationName(), true);
        }
    }

    public void RaiseMuzzleFlash()
    {
        if (!characterAnimator.IsInTransition())
        {
            var weaponName = character.GetWeaponName();
            if (weaponName != null)
            {
                var muzzleFlash = GetComponentsInChildren<MuzzleEffectFlash>(true)
                        .Where(gameObject => gameObject.gameObject.CompareTag(weaponName))
                        .FirstOrDefault();
                var shootEffect = GetComponentsInChildren<ShootEffect>(true)
                        .Where(gameObject => gameObject.gameObject.CompareTag(weaponName))
                        .FirstOrDefault();

                if (muzzleFlash != null)
                {
                    RaiseMuzzleFlashIfSingleMode(muzzleFlash);
                    RaiseMuzzleFlashIfBurstMode(muzzleFlash);

                    if (playAttackSound)
                    {
                        playAttackSound = false;
                        PlayAudioClip(character.GetHitSound());
                    }

                    if (shootEffect == null)
                    {
                        var enemy = interactionTarget?.GetComponent<CharacterController>();
                        if (enemy != null)
                        {
                            enemy.RunHitAnimation();
                        }
                    }
                }
                if (shootEffect != null)
                {
                    RaiseShootEffect(shootEffect);
                }
            }
        }
    }

    private void RaiseMuzzleFlashIfSingleMode(MuzzleEffectFlash muzzleFlash)
    {
        if (character.GetWeaponMode() == Weapons.Mode.Single)
        {
            var newMuzzleFlash = Instantiate(muzzleFlash, muzzleFlash.transform.position,
                muzzleFlash.transform.rotation);
            newMuzzleFlash.gameObject.SetActive(true);
            Destroy(newMuzzleFlash.gameObject, newMuzzleFlash.Delay);
        }
    }

    private void RaiseMuzzleFlashIfBurstMode(MuzzleEffectFlash muzzleFlash)
    {
        if (character.GetWeaponMode() == Weapons.Mode.Burst && raiseMuzzle)
        {
            muzzleFlash.GetComponent<ParticleSystem>().Play();
            raiseMuzzle = false;
        }
    }

    private void RaiseShootEffect(ShootEffect shootEffect)
    {
        var enemy = interactionTarget.GetComponent<CharacterController>();
        int distance = CalculateDistance();

        if (distance > Character.MELLE_ATTACK_DISTANCE)
        {
            var shoot = Instantiate(shootEffect, shootEffect.transform.position, Quaternion.identity).gameObject;
            shoot.SetActive(true);
            shoot.GetComponent<Rigidbody>().velocity = (enemy.FirePoint.position
                - shoot.transform.position).normalized * speed;
        }
        else
        {
            enemy.RunHitAnimation();
        }
    }

    public void StopMuzzle()
    {
        var weaponName = character.GetWeaponName();
        var muzzleFlash = GetComponentsInChildren<ParticleSystem>(true)
                    .Where(gameObject => gameObject.gameObject.CompareTag(weaponName))
                    .FirstOrDefault();

        if (muzzleFlash != null)
        {
            muzzleFlash.Stop();
        }
        raiseMuzzle = true;
    }

    private void PlayAudioClip(AudioClip audioClip)
    {
        if (audioClip != null)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }
    }

    public void ToggleAttackHitAnimation()
    {
        characterAnimator.ToggleHitAnimation();
    }

    public void RunHitAnimation()
    {
        if (!character.IsDead())
        {
            characterAnimator.StartHitAnimation();
        }
        else
        {
            Die();
        }
    }

    protected void MoveToTarget(Vector3 movingTarget)
    {
        if (character.IsTraveling())
        {
            SetMoveAction(movingTarget);
        }
        if (character.IsCombatTurn())
        {
            var distance = (int)Vector3.Distance(movingTarget, transform.position);
            if (character.ReduceActionPointsForMovementIfPossible(distance))
            {
                SetMoveAction(movingTarget);
                character.ChangeStateToCombatMoving();
            }
        }
    }

    private void SetMoveAction(Vector3 movingTarget)
    {
        characterAnimator.ApplyRootMotion(false);
        this.movingTarget = movingTarget;
        this.interactionTarget = null;
        action = Move;
    }

    public void Move()
    {
        characterNavigator.MoveAsync(transform.position, movingTarget);
    }

    protected void DisableAction()
    {
        playAttackSound = true;
        action = () => { };
    }

    private CharacterNavigator CharacterNavigator(NavMeshAgent agent, NavMeshObstacle obstacle) =>
     new CharacterNavigator(agent, obstacle);

    public void StopFiring()
    {
        characterAnimator.SetAnimation(character.GetAttackAnimationName(), false);
    }

    private void OnStartInteract(bool start)
    {
        if (start)
        {
            if (run)
            {
                characterAnimator.SetAnimation(CharacterAnimator.WALK, false);
                characterAnimator.SetAnimation(CharacterAnimator.RUN, true);

                characterNavigator.SetRunningSpeed();
                characterNavigator.SetStopRunningSpeed();
            }
            else
            {
                characterAnimator.SetAnimation(CharacterAnimator.RUN, false);
                characterAnimator.StartWalkAnimation();

                characterNavigator.SetWalkingSpeed();
                characterNavigator.SetStopWalkingSpeed();
            }
        }
    }

    private void OnMovementStop(bool stop)
    {
        if (stop)
        {
            DisableAction();
            characterAnimator.DisableAnimations();
            characterAnimator.ApplyRootMotion(true);
            if (combatManager.IsCombatActive())
            {
                character.ChangeStateToCombatTurn();
            }
        }
    }

    public Character Character { get => character; }

    public Transform FirePoint { get => firePoint; }
    public bool RaiseMuzzle { get => raiseMuzzle; set => raiseMuzzle = value; }

    public bool IsCombatTurn() => character.IsCombatTurn();

    public void ChangeCharacterStateToWaitingForTurn()
    {
        character.ChangeStateToWaitingForTurn();
    }

    public void ChangeCharacterStateToTraveling()
    {
        character.ChangeStateToTraveling();
    }

    public void GetTurnInCombat()
    {
        character.GetTurnInCombat();
    }

    public bool IsDead() => character.IsDead();

    public Character GetCharacter() => character;

    protected void PassCombatTurnToNext()
    {
        if (character.IsCombatTurn())
        {
            character.PussTurn();
            combatManager.PassTurnToNext();
        }
    }

    public void Die()
    {
        collider.enabled = false;
        DisableAction();
        characterAnimator.SetAnimation(CharacterAnimator.DEATH, true);
        combatManager.RemoveParticipant(this);
    }

    private void OnLogMessage(string message)
    {
        if (logger != null)
        {
            logger.AddLog(message);
        }
    }
}

