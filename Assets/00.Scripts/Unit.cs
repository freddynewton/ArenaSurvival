using EmeraldAI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace freddynewton
{
    public class Unit : MonoBehaviour
    {
        public Rigidbody rigidbody;

        public EmeraldAISystem emeraldAISystem;

        private Material flashMaterial;
        private SkinnedMeshRenderer[] skinnedMeshRenderers;
        private Material[] materials;

        public void DoDamage(float amount)
        {
            StartCoroutine(meshFlash(0.2f));
        }

        private IEnumerator meshFlash(float time)
        {
            foreach (var skinMeshRenderer in skinnedMeshRenderers)
            {
                skinMeshRenderer.material = flashMaterial;
            }

            yield return new WaitForSecondsRealtime(time);

            for (int i = 0; i < materials.Length; i++)
            {
                skinnedMeshRenderers[i].material = materials[i];
            }
        }

        private void Awake()
        {
            skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
            rigidbody = GetComponent<Rigidbody>();
            flashMaterial = Resources.Load("Materials/FlashGlow") as Material;

            materials = new Material[skinnedMeshRenderers.Length];

            for (int i = 0; i < skinnedMeshRenderers.Length; i++)
            {
                materials[i] = skinnedMeshRenderers[i].material;
            }
        }
    }
}
