using ModestTree;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Weapons;

public class NpcController : CharacterController
{

    [SerializeField]
    private double stopDistance = 0;

    private float cooldownTime = 5f;

    private float nextFireTime = 0f;

    [SerializeField]
    private List<ScriptableObject> items = new();

    [SerializeField]
    private Weapon additionalWeapon;

    [SerializeField]
    private DialogNode dialog;

    private void PerformGameAction()
    {
        switch (character.GetWeaponMode())
        {
            case Mode.Burst:
                action = PerformBurstAttack;
                break;
            case Mode.Single:
                action = PerformAttack;
                break;
        }
    }

    private void PerformAttack()
    {
        if (!characterAnimator.IsInTransition())
        {
            var distance = CalculateDistance();

            if (!character.CanAttack(distance))
            {
                if (distance <= stopDistance)
                {
                    Stop();
                    AttackOrPassTurn(distance);
                }
                else
                {
                    CombatMove(distance);
                }
            }
            else
            {
                Stop();
                AttackTarget(interactionTarget);
            }
        }
    }

    private bool OutOfAmmo()
    {
        return character.IsWeaponStoreEmpty() && HasNoAmmunitions();
    }

    private void CombatMove(int distance)
    {
        character.ReduceActionPointsForMovement(distance);
        characterNavigator.ComeUpAsync(transform, interactionTarget.position);
        characterAnimator.StartWalkAnimation();
        character.ChangeStateToCombatMoving();
    }

    private void AttackOrPassTurn(int distance)
    {
        if (character.CanAttack(distance))
        {
            AttackTarget(interactionTarget);
        }
        else
        {
            DisableAction();
            combatManager.PassTurnToNext();
        }
    }

    private void BurstAttackOrPassTurn(int distance)
    {
        if (character.CanAttackWithBurstFire(distance))
        {
            BurstFireAttack(interactionTarget);
        }
        else
        {
            if (characterAnimator.IsBurstFireEnd())
            {
                DisableAction();
                combatManager.PassTurnToNext();
            }
        }
    }

    private void PerformBurstAttack()
    {
        TryReloadWeapon();
        var distance = CalculateDistance();

        if (OutOfAmmo() || !character.IsWeaponStoreNotEmptyForBurstFire())
        {
            ChangeWeapon();
            return;
        }

        if (!character.CanAttackWithBurstFire(distance))
        {
            if (distance <= stopDistance)
            {
                Stop();
                BurstAttackOrPassTurn(distance);
            }
            else
            {
                CombatMove(distance);
            }
        }
        else
        {
            Stop();
            BurstFireAttack(interactionTarget);
        }
    }

    private void Stop()
    {
        DisableAction();
        character.ChangeStateToCombatTurn();
        characterAnimator.DisableAnimations();
        characterNavigator.StopInPlace();
    }


    public void TakeCombatTurn()
    {
        character.ChangeStateToCombatTurn();
        this.interactionTarget = combatManager.GetChosenOne().transform;

        var distance = CalculateDistance();
        stopDistance = CalculateStopDistance(distance);

        PerformGameAction();
    }

    private int CalculateStopDistance(int distance)
    {
        int movementActionPoints = character.CalculateMovementActionPoints(distance);
        return distance - movementActionPoints;
    }

    protected override void Attack()
    {
        TryReloadWeapon();
        var distance = CalculateDistance();

        if (character.CanAttack(distance))
        {
            if (characterAnimator.IsFireEnd())
            {
                if (Time.time >= nextFireTime)
                {
                    base.Attack();
                    combatManager.CalculateHitPoints();
                    nextFireTime = Time.time + cooldownTime;
                }
            }
        }
        else
        {
            if (Time.time >= nextFireTime)
            {
                DisableAction();
                combatManager.PassTurnToNext();

                nextFireTime = Time.time + cooldownTime;
            }
        }
    }

    private void TryReloadWeapon()
    {
        var ammunitions = GetItemsByType(typeof(Ammunition));
        if (ammunitions != null && !ammunitions.IsEmpty())
        {
            character.TryReloadWeapon((Ammunition)ammunitions.First());
        }
    }

    protected override void BurstAttack()
    {
        TryReloadWeapon();
        var distance = CalculateDistance();
        if (character.CanAttackWithBurstFire(distance))
        {
            if (characterAnimator.IsBurstFireEnd())
            {
                if (Time.time >= nextFireTime)
                {
                    base.BurstAttack();
                    combatManager.CalculateHitPoints();
                    nextFireTime = Time.time + cooldownTime;
                }
            }
        }
        else
        {
            if (Time.time >= nextFireTime)
            {
                DisableAction();
                combatManager.PassTurnToNext();

                nextFireTime = Time.time + cooldownTime;
            }
        }
    }

    private bool HasNoAmmunitions()
    {
        List<Ammunition> ammunitions = GetItemsByType(typeof(Ammunition))
            .Where(a => a != null)
            .Cast<Ammunition>()
            .ToList();
        if (ammunitions == null)
        {
            return true;
        }
        if (ammunitions.Count == 0)
        {
            return true;
        }
        var notEmptyAmmunitions = ammunitions.Where(a => a.IsNotEmpty())
            .ToList();
        return notEmptyAmmunitions.Count == 0;
    }

    private List<ScriptableObject> GetItemsByType(Type type)
    {
        if (items != null)
        {
            if (items.Count > 0)
            {
                return items.Where(item => item.GetType().Equals(type))
                    .ToList();
            }
        }
        return new();
    }

    void OnMouseOver()
    {
        if (combatManager.IsCombatActive())
        {
            cursorManager?.ChangeCursorToAim();
        }
    }

    void OnMouseExit()
    {
        if (combatManager.IsCombatActive())
        {
            cursorManager?.ChangeCursorToDefault();
        }
    }

    private void ChangeWeapon()
    {
        if (additionalWeapon == null)
        {
            DisableCurrentWeapon();
            characterAnimator.SetAnimation(CharacterAnimator.DEFAULT, true);
            character.ChangeWeapon(null);
        }
        else
        {
            DisableCurrentWeapon();

            var newWeaponObject = GetComponentsInChildren<WeaponObject>(true)
                    .Where(gameObject => gameObject.gameObject.CompareTag(additionalWeapon.Name))
                    .FirstOrDefault();

            if (newWeaponObject != null)
            {
                newWeaponObject.gameObject.SetActive(true);
            }

            var idleAnimationName = additionalWeapon.IdleAnimationName;
            characterAnimator.SetAnimation(idleAnimationName, true);
            character.ChangeWeapon(additionalWeapon);
        }
        ExecPerformAttack();
    }

    private void ExecPerformAttack()
    {
        var distance = CalculateDistance();
        stopDistance = CalculateStopDistance(distance);
        action = PerformAttack;
    }

    private void DisableCurrentWeapon()
    {
        var currentWeaponObject = GetComponentsInChildren<WeaponObject>(true)
                .Where(gameObject => gameObject.gameObject.CompareTag(character.Weapon.Name))
                .FirstOrDefault();
        if (currentWeaponObject != null)
        {
            currentWeaponObject.gameObject.SetActive(false);
        }
    }

    public DialogNode Dialog { get => dialog; }

    public void StartCombat()
    {
        character.ChangeStateToCombatTurn();
        this.interactionTarget = combatManager.GetChosenOne().transform;

        var distance = CalculateDistance();
        stopDistance = CalculateStopDistance(distance);

        combatManager?.StartCombat(this);
    }

    private void OnCombarStarted(bool started)
    {
        if (started)
        {
            PerformGameAction();
        }
    }

    protected override void AdditionalInit()
    {
        combatManager.CombatStarted += OnCombarStarted;
    }
}

