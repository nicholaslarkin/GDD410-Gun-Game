using UnityEngine;

public class TextureScaling : MonoBehaviour
{
    private Renderer rend;
    private MaterialPropertyBlock propertyBlock;

    private Vector3 originalScale;
    private Vector2 originalTiling;

    private string textureProperty;
    private string tilingProperty;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        propertyBlock = new MaterialPropertyBlock();

        originalScale = transform.lossyScale;

        // Support both Built-in and URP shaders
        if (rend.sharedMaterial.HasProperty("_BaseMap"))
        {
            textureProperty = "_BaseMap";
        }
        else
        {
            textureProperty = "_MainTex";
        }

        tilingProperty = textureProperty + "_ST";

        originalTiling = rend.sharedMaterial.GetTextureScale(textureProperty);
    }

    private void LateUpdate()
    {
        Vector3 currentScale = transform.lossyScale;

        float scaleX = currentScale.x / originalScale.x;
        float scaleZ = currentScale.z / originalScale.z;

        Vector2 newTiling = new Vector2(
            originalTiling.x * scaleX,
            originalTiling.y * scaleZ
        );

        rend.GetPropertyBlock(propertyBlock);

        propertyBlock.SetVector(tilingProperty, new Vector4(
            newTiling.x,
            newTiling.y,
            originalTiling.x == 0 ? 0 : rend.sharedMaterial.GetTextureOffset(textureProperty).x,
            originalTiling.y == 0 ? 0 : rend.sharedMaterial.GetTextureOffset(textureProperty).y
        ));

        rend.SetPropertyBlock(propertyBlock);
    }
}
