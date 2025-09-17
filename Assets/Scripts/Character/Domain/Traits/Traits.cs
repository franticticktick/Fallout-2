using UnityEngine;

namespace Traits
{
    public abstract class Trait : ScriptableObject
    {
        [SerializeField]
        private bool chosen = false;
        [SerializeField]
        private string traitName;
        [SerializeField]
        private string description;

        public string TraitName { get => traitName; }
        public string Description { get => description; }

        public abstract void Apply(Character character);

        public abstract void Cancel(Character character);

        public void Chose(Character character)
        {
            chosen = true;
            Apply(character);
        }

        public void UnChose(Character character)
        {
            chosen = false;
            Cancel(character);
        }

        public bool IsChosen()
        {
            return chosen;
        }
    }
}
