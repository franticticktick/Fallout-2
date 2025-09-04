using UnityEngine;

namespace Traits
{
    public abstract class Trait: ScriptableObject
    {
        private bool chosen = false;
        public abstract void Apply(Character character);

        public abstract void Cancel(Character character);

        public void Chose()
        {
            chosen = true;
        }

        public bool IsChosen()
        {
            return chosen;
        }
    }
}
