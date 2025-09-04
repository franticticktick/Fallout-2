using UnityEngine;
public interface Item
{
    Texture2D GetImage();

    int GetQuantity();

    bool IsQuantitative();
}
