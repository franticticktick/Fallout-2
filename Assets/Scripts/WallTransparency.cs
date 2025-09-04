using System.Collections.Generic;
using UnityEngine;

public class WallTransparency : MonoBehaviour
{
    public Transform player;

    public LayerMask wallLayer;

    public float distance = 5f;

    public float radius = 4f;

    public Camera cam;

    private HashSet<Collider> colliders = new();

    void Update()
    {
        Ray ray = new(player.position, Vector3.forward);
        RaycastHit[] hits = Physics.SphereCastAll(ray, radius, distance, wallLayer);

        if (hits.Length > 0)
        {
            foreach (var hit in hits)
            {
                MakeColidersTransparent(hit, ray);
            }
        }
        else
        {
            MakeColidersOpaque();
        }
    }

    private void MakeColidersTransparent(RaycastHit hit, Ray ray)
    {
        Renderer renderer = hit.collider.GetComponent<Renderer>();
        colliders.Add(hit.collider);

        if (renderer != null)
        {
            foreach (Material mat in renderer.materials)
            {
                Color currentColor = mat.color;
                mat.color = new Color(currentColor.r, currentColor.g, currentColor.b, 0.5f);
            }
        }
    }

    private void MakeColidersOpaque()
    {
        foreach (Collider collider in colliders)
        {
            if (collider != null)
            {
                if (collider.TryGetComponent(out Renderer renderer))
                {
                    foreach (Material mat in renderer.materials)
                    {
                        Color currentColor = mat.color;
                        mat.color = new Color(currentColor.r, currentColor.g, currentColor.b, 1);
                    }
                }
            }
        }
        colliders.Clear();
    }
}
