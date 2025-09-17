using UnityEngine;

namespace Skills
{

    public abstract class Skill : ScriptableObject
    {
        [SerializeField]
        private bool main;

        [SerializeField]
        protected int value = 0;

        [SerializeField]
        protected int characteristicsValue = 0;

        [SerializeField]
        private string skillName;

        public void InitBaseValue(Character character)
        {
            characteristicsValue = CalculateValue(character);
        }

        protected abstract int CalculateValue(Character character);

        public int GetValue()
        {
            return value + characteristicsValue;
        }

        public bool IsEmptyValue() => value == 0;

        public void IncreaseValue(int value)
        {
            this.value += value;
        }

        public void IncrementValue()
        {
            if (main)
            {
                value += 2;
            }
            else
            {
                value++;
            }
        }

        public void DicrementValue()
        {
            if (main)
            {
                value -= 2;
                value = value < 0 ? 0 : value;
            }
            else
            {
                value--;
                value = value < 0 ? 0 : value;
            }
        }

        public bool IsMain()
        {
            return main;
        }

        public void MarkAsMain()
        {
            main = true;
            value += 20;
        }

        public void MarkAsNotMain()
        {
            main = false;
            value -= 20;
        }

        public string SkillName { get => skillName; }
    }

}
