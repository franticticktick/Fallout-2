using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Weapons;

public class ActionButton : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private ChosenOneController chosenOne;

    [SerializeField]
    private TMP_Text modeField;

    [SerializeField]
    private TMP_Text actionPointsField;

    [SerializeField]
    private Texture2D buttonDown;

    [SerializeField]
    private Texture2D buttonUp;

    [SerializeField]
    private AudioClip buttonUpClip;

    [SerializeField]
    private AudioClip buttonDownClip;

    [SerializeField]
    private AudioSource audioSource;

    void Start()
    {
        SetWeaponMode();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            ChangeWeaponMode();
        }
    }

    private void SetWeaponMode()
    {
        if (chosenOne != null)
        {
            var mode = chosenOne.GetWeaponMode();
            InitFields(mode);
        }
    }

    private void ChangeWeaponMode()
    {
        if (chosenOne != null)
        {
            var mode = chosenOne.ChageWeaponMode();
            InitFields(mode);
        }
    }

    private void InitFields(Mode mode)
    {
        var character = chosenOne.Character;
        var weapon = chosenOne.Character.Weapon;
        if (weapon == null)
        {
            SetTxt(actionPointsField, character.GetMelleAttackAtionPoints());
        }
        else
        {
            switch (mode)
            {
                case Mode.Single:
                    SetTxt(modeField, "Одиночный");
                    break;
                case Mode.Burst:
                    SetTxt(modeField, "Очередь");
                    break;
                case Mode.Reload:
                    SetTxt(modeField, "Перезарядка");
                    break;
            }
            if (mode != Mode.Reload)
            {
                SetTxt(actionPointsField, character.GetWeaponActionPoints());
            }
            else
            {
                SetTxt(actionPointsField, character.GetReloadWeaponAtionPoints());
            }
        }
    }

    private void SetTxt(TMP_Text field, string text)
    {
        if (modeField != null)
        {
            field.text = text;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (TryGetComponent<Image>(out var image))
            {
                Sprite newSprite = Sprite.Create(buttonDown,
                    new Rect(0, 0, buttonDown.width, buttonDown.height),
                    new Vector2(0.5f, 0.5f), 100f);
                image.sprite = newSprite;
                PlayAudioClipOneShot(buttonDownClip);
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (TryGetComponent<Image>(out var image))
            {
                Sprite newSprite = Sprite.Create(buttonUp,
                   new Rect(0, 0, buttonUp.width, buttonUp.height),
                   new Vector2(0.5f, 0.5f), 100f);
                image.sprite = newSprite;
                PlayAudioClipOneShot(buttonUpClip);
            }
        }
    }

    private void PlayAudioClipOneShot(AudioClip audioClip)
    {
        if (audioClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }
}

