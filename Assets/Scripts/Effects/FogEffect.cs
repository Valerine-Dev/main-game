using UnityEngine;

namespace Effects
{
    [ExecuteInEditMode, ImageEffectAllowedInSceneView]
    public class FogEffect : MonoBehaviour
    {
        public Material _mat;
        public Color _fogColor;
        public float _depthStart;
        public float _depthDistance;
        private void Start()
        {
            GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;
        }
        private void Update()
        {
            _mat.SetColor("_FogColor", _fogColor);
            _mat.SetFloat("_DepthDistance", _depthStart);
            _mat.SetFloat("_DepthDistance", _depthDistance);
        }
        private void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            Graphics.Blit(source, destination, _mat);
        }
    }
}
