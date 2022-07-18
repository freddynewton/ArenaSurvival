using freddynewton.player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace freddynewton
{
    public class MeshTrailEffect
    {
        private SkinnedMeshRenderer[] meshRenderers;
        private Material material;

        private string shaderVariableRef = "_Alpha";

        public MeshTrailEffect(SkinnedMeshRenderer[] meshRenderers, Material material)
        {
            this.meshRenderers = meshRenderers;
            this.material = material;
        }

        public IEnumerator ActivateTrail(float duration, float meshDestroyDelay, float meshRefreshRate = 0.1f)
        {
            while (duration > 0)
            {
                duration -= meshRefreshRate;

                for (int i = 0; i < meshRenderers.Length; i++)
                {
                    var gObj = new GameObject();
                    gObj.transform.SetPositionAndRotation(meshRenderers[i].transform.position, meshRenderers[i].transform.rotation);

                    var mr = gObj.AddComponent<MeshRenderer>();
                    var mf = gObj.AddComponent<MeshFilter>();
                    mr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

                    Mesh mesh = new Mesh();
                    meshRenderers[i].BakeMesh(mesh);

                    mf.mesh = mesh;
                    mr.material = material;

                    PlayerManager.Instance.playerMovement.StartCoroutine(AnimateMaterialFloat(mr.material, 0, meshDestroyDelay));

                    GameObject.Destroy(gObj, meshDestroyDelay);
                }

                yield return new WaitForSeconds(meshRefreshRate);
            }
        }

        private IEnumerator AnimateMaterialFloat (Material mat, float goal, float time)
        {
            float valueToAnimate = mat.GetFloat(shaderVariableRef);

            var currentTime = 0f;

            while (currentTime < time)
            {
                currentTime += Time.deltaTime;
                valueToAnimate = Mathf.SmoothStep(valueToAnimate, goal, currentTime / time);
                mat.SetFloat(shaderVariableRef, valueToAnimate);

                yield return null;
            }
        }
    }
}
