using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "Stimulant", menuName = "Stimulant")]
    public class Stimulant : ScriptableObject, Item, ActiveItem
    {
        [SerializeField]
        private Texture2D image;

        [SerializeField]
        private int quantity;

        public void Use(Character character)
        {
            var hp = new System.Random().Next(10, 20);
            character.Heal(hp);
        }

        public Texture2D GetImage()
        {
            return image;
        }

        public int GetQuantity()
        {
            return quantity;
        }

        public bool IsQuantitative()
        {
            return true;
        }
    }
}
