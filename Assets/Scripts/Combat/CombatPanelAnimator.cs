using System;
using UnityEngine;
using UnityEngine.UI;

public class CombatPanelAnimator : MonoBehaviour
{
    public Image targetImage;

    public Image cmbt;

    public Image turn;

    [SerializeField]
    private Button turnButton;

    [SerializeField]
    private Button cmbtButton;

    public Sprite[] frames;

    public float frameRate = 0.5f;

    public int currentFrame = 0;

    public float timer = 0f;

    private Action action;

    [SerializeField]
    private Sprite[] nums;

    [SerializeField]
    private Sprite[] numsTransitions;

    private CircularList<Sprite> circularList = new CircularList<Sprite>();

    [SerializeField]
    private ChosenOneController controller;

    [SerializeField]
    private Image firstDigitHp;

    [SerializeField]
    private Image secondDigitHp;

    [SerializeField]
    private Image thirdDigitHp;

    private int hpValue = 0;

    private int reduceHpValue = 0;

    private int currentHpSprite = 0;

    public float hpSpriteRate = 0.2f;

    [SerializeField]
    private Image firstDigitArmorClass;

    [SerializeField]
    private Image secondDigitArmorClass;

    [SerializeField]
    private Image thirdDigitArmorClass;

    public event Action<bool> PanelOpen;

    void Start()
    {
        foreach (var sprite in numsTransitions)
        {
            circularList.Add(sprite);
        }
        hpValue = controller.GetCurrentHp();
        string currentHp = controller.GetCurrentHp().ToString();
        char[] hpDigits = currentHp.ToCharArray();

        string currentArmorClass = controller.GetArmorClass().ToString();
        char[] armorClassDigits = currentArmorClass.ToCharArray();

        InitUiFields(hpDigits, firstDigitHp, secondDigitHp, thirdDigitHp);
        InitUiFields(armorClassDigits, firstDigitArmorClass, secondDigitArmorClass, thirdDigitArmorClass);
        DisableAction();
    }

    private void InitUiFields(char[] digits, Image firstDigit,
        Image secondDigit, Image thirdDigit)
    {
        if (digits.Length == 1)
        {
            var num = ConvertCharToInt(digits[0]);

            firstDigit.sprite = nums[0];
            secondDigit.sprite = nums[0];
            thirdDigit.sprite = nums[num];
        }
        if (digits.Length == 2)
        {
            var num1 = ConvertCharToInt(digits[0]);
            var num2 = ConvertCharToInt(digits[1]);

            firstDigit.sprite = nums[0];
            secondDigit.sprite = nums[num1];
            thirdDigit.sprite = nums[num2];
        }
        if (digits.Length == 3)
        {
            var num1 = ConvertCharToInt(digits[0]);
            var num2 = ConvertCharToInt(digits[1]);
            var num3 = ConvertCharToInt(digits[2]);

            firstDigit.sprite = nums[num1];
            secondDigit.sprite = nums[num2];
            thirdDigit.sprite = nums[num3];
        }
    }

    private int ConvertCharToInt(char digit)
    {
        return digit - '0';
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            StartReduceHealthPoints();
        }
        action?.Invoke();
    }

    public void StartOpenPanelAnimation()
    {
        action = OpenPanel;
    }

    public void StartClosePanelAnimation()
    {
        turn?.gameObject.SetActive(false);
        cmbt?.gameObject.SetActive(false);
        action = ClosePanel;
    }

    private void OpenPanel()
    {
        timer += Time.deltaTime;
        if (timer >= frameRate)
        {
            if (currentFrame != 3)
            {
                currentFrame = (currentFrame + 1) % frames.Length;
                targetImage.sprite = frames[currentFrame];
                timer -= frameRate;
            }
            if (currentFrame == 3)
            {
                turn?.gameObject.SetActive(true);
                cmbt?.gameObject.SetActive(true);
                DisableAction();
                timer = 0f;
                PanelOpen?.Invoke(true);
            }
        }
    }

    private void ClosePanel()
    {
        timer += Time.deltaTime;
        if (timer >= frameRate)
        {
            if (currentFrame != 0)
            {
                currentFrame = (currentFrame - 1) % frames.Length;
                targetImage.sprite = frames[currentFrame];
                timer -= frameRate;
            }
            if (currentFrame == 0)
            {
                DisableAction();
                currentFrame = 0;
                DisableAction();
            }
        }
    }

    public void StartReduceHealthPoints()
    {
        reduceHpValue = hpValue - controller.GetCurrentHp();
        hpValue = controller.GetCurrentHp();
        if (reduceHpValue != 0)
        {
            action = ReduceHealthPoints;
        }
    }

    public void SetArmorClass()
    {
        string currentArmorClass = controller.GetArmorClass().ToString();
        char[] armorClassDigits = currentArmorClass.ToCharArray();

        InitUiFields(armorClassDigits, firstDigitArmorClass, secondDigitArmorClass, thirdDigitArmorClass);
    }

    private void ReduceHealthPoints()
    {
        timer += Time.deltaTime;
        if (timer >= hpSpriteRate)
        {
            currentHpSprite++;
            if (currentHpSprite < reduceHpValue)
            {
                if (currentHpSprite % 2 == 0)
                {
                    firstDigitHp.sprite = circularList.GetNext();
                    secondDigitHp.sprite = circularList.GetNext();
                    thirdDigitHp.sprite = circularList.GetNext();
                }
                else
                {
                    thirdDigitHp.sprite = circularList.GetNext();
                    secondDigitHp.sprite = circularList.GetNext();
                    firstDigitHp.sprite = circularList.GetNext();
                }
                timer -= hpSpriteRate;
            }
            else
            {
                currentHpSprite = 0;
                timer = 0;
                reduceHpValue = 0;

                char[] digits = hpValue.ToString().ToCharArray();
                InitUiFields(digits, firstDigitHp, secondDigitHp, thirdDigitHp);

                DisableAction();
            }
        }
    }

    public void EnableCmbt()
    {
        cmbtButton.interactable = true;
    }

    public void EnableTurn()
    {
        turnButton.interactable = true;
    }

    public void DisableTurn()
    {
        turnButton.interactable = false;
    }

    public void DisableCmbt()
    {
        cmbtButton.interactable = false;
    }

    private void DisableAction() => action = () => { };

    public bool IsHpCounting() => action == ReduceHealthPoints;
}
