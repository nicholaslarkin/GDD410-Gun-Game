using UnityEngine;

public class TextureTiling : MonoBehaviour
{
    [SerializeField] private Vector2 tiling = new Vector2(2, 2);

    private MeshRenderer objectRenderer;
    private MaterialPropertyBlock propertyBlock;

    private void Awake()
    {
        objectRenderer = GetComponent<MeshRenderer>();
        propertyBlock = new MaterialPropertyBlock();

        UpdateTiling();
    }

    private void UpdateTiling()
    {
        objectRenderer.GetPropertyBlock(propertyBlock);

        propertyBlock.SetVector("_BaseMap_ST",
            new Vector4(tiling.x, tiling.y, 0, 0));

        objectRenderer.SetPropertyBlock(propertyBlock);
    }
}
