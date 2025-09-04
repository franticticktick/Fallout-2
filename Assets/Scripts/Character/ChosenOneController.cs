using Armors;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using Weapons;

public class ChosenOneController : CharacterController
{
    [SerializeField]
    private Inventory inventory;

    private float cooldownTime = 5f;

    private float nextFireTime = 0f;

    [SerializeField]
    private AudioClip reloadWeaponClip;

    protected override void Start()
    {
        base.Start();
        InitArmor(); ;
    }

    private void InitArmor()
    {
        var armor = character.Armor;
        var armorObjects = GetComponentsInChildren<ArmorObject>(true);
        foreach (var armorObject in armorObjects)
        {
            armorObject.gameObject.SetActive(false);
        }
        if (armor != null)
        {
            var armorObject = GetComponentsInChildren<ArmorObject>(true)
                .Where(gameObject => gameObject.gameObject.CompareTag(armor.name))
                .FirstOrDefault();
            armorObject.gameObject.SetActive(true);
        }
        else
        {
            var armorObject = GetComponentsInChildren<ArmorObject>(true)
                          .Where(gameObject => gameObject.gameObject.CompareTag("ChosenOne"))
                          .FirstOrDefault();
            armorObject.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        action?.Invoke();
        OnRightButtonClick();
        EnableRunning();
    }

    private void EnableRunning()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            run = !run;
        }
    }

    private void OnRightButtonClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                switch (hit.transform.tag)
                {
                    case GROUND: MoveToTarget(hit.point); break;
                    case ATTACKABLE: AttackTarget(hit.transform); break;
                }
            }
        }
    }

    protected override void Attack()
    {
        base.Attack();
        DisableAction();
    }

    protected override void AttackTarget(Transform target)
    {
        if (Time.time >= nextFireTime)
        {
            base.AttackTarget(target);
            nextFireTime = Time.time + cooldownTime;
        }
    }

    public void Turn()
    {
        PassCombatTurnToNext();
    }

    public void EndCombat()
    {
        combatManager.EndCombat();
    }

    public int GetCurrentHp() => (int)(character.CurrentHitPoints < 0 ? 0 : character.CurrentHitPoints);

    public int GetArmorClass() => character.ArmorClass;

    public void ChangeArmor(Armor armor)
    {
        character.ChangeArmor(armor);
        InitArmor();
    }

    public void ChangeWeapon(Weapon weapon)
    {
        var currentWeapon = character.Weapon;
        if (currentWeapon != null)
        {
            characterAnimator.SetAnimation(currentWeapon.IdleAnimationName, false);
        }
        else
        {
            characterAnimator.SetAnimation(CharacterAnimator.DEFAULT, false);
        }
        character.ChangeWeapon(weapon);
        InitWeapon();
    }

    public Mode ChageWeaponMode()
    {
        return character.ChangeWeaponMode();
    }

    public Mode GetWeaponMode()
    {
        return character.GetWeaponMode();
    }

    public void ReloadWeaponOrStartCombat()
    {
        var mode = character.GetWeaponMode();
        if (mode == Mode.Reload)
        {
            ReloadWeapon();
        }
        else
        {
            StartCombat();
        }
    }

    private void StartCombat()
    {
        DisableAction();

        characterAnimator.DisableAnimations();
        characterNavigator.StopInPlace();
        character.GetTurnInCombat();

        combatManager.StartCombat(this);
    }

    private void ReloadWeapon()
    {
        List<Ammunition> ammunitions = inventory.GetItemsByType<Ammunition>();
        Debug.Log(ammunitions.Count);
        var ammunition = ammunitions
            .Where(ammunition => character.WeaponSupportAmmunitionType(
                ammunition.AmmunitionType))
            .FirstOrDefault();
        if (ammunition != null)
        {
            Debug.Log(ammunition.name);
        }
        character.ReloadWeapon(ammunition);

        PlayAudioClipOneShot(reloadWeaponClip);
    }

    private void PlayAudioClipOneShot(AudioClip audioClip)
    {
        if (audioClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }
}
