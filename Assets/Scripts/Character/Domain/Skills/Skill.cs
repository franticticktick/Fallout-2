using UnityEngine;

namespace Skills
{

    public abstract class Skill : ScriptableObject
    {
        [SerializeField]
        private bool main;

        [SerializeField]
        protected int value;

        public abstract void Calculate(Character character);

        public int GetValue()
        {
            return value;
        }

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
    }

}
