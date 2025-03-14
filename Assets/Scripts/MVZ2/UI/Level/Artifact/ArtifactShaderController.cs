using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MVZ2.Level.UI
{
    [ExecuteAlways]
    [RequireComponent(typeof(RectTransform))]
    [DisallowMultipleComponent]
    public class ArtifactShaderController : UIBehaviour, IMaterialModifier
    {
        protected override void OnDisable()
        {
            base.OnDisable();
            if (material)
            {
                DestroyImmediate(material);
                material = null;
            }
        }
        private void Update()
        {
            if (!material)
                return;
            material.SetFloat("_Grayscale", grayscale);
            material.SetFloat("_Brighten", brighten);
        }
        public virtual Material GetModifiedMaterial(Material baseMaterial)
        {
            if (!IsActive() || !graphic)
                return baseMaterial;

            if (material == null)
            {
                material = Instantiate(baseMaterial);
                material.SetFloat("_Grayscale", grayscale);
                material.SetFloat("_Brighten", brighten);
            }
            return material;
        }
        public Graphic graphic;
        [Range(0, 1)]
        public float grayscale;
        [Range(0, 1)]
        public float brighten;
        private Material material;
    }
}
