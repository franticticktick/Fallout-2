using UnityEngine;

namespace Perks
{

    public abstract class Perk
    {

        [SerializeField]
        private string name;


        [SerializeField]
        private int level;


        [SerializeField]
        private int maxLevel;


        [SerializeField]
        private int requiredLevel;

        public abstract void Apply(Character character);

        public bool CheckRequirements(Character character) =>
             character.Level <= requiredLevel && CheckAdditionalRequirements(character);

        protected abstract bool CheckAdditionalRequirements(Character character);

        public string Name { get => name; set => name = value; }

    }
}
