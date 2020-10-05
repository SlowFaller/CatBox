using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace LD47.Core
{
    public class EffectController : MonoBehaviour
    {
        [SerializeField] [Range(0, 1)] float maxVignetteIntensity =.6f;
        [SerializeField] [Range(0, 1)] float maxGrainIntensity = .7f;
        [SerializeField] [Range(0, 1)] float minVignetteIntensity = .3f;
        [SerializeField] [Range(0, 1)] float minGrainIntensity = .1f;

        [SerializeField] [Range(0,3)] float fadeTime = 1f;

        PostProcessVolume cmp_processVol;
        Vignette vignette;
        Grain grain;
        [SerializeField] bool fading = false;

        private void Awake()
        {
            cmp_processVol = GetComponent<PostProcessVolume>();
        }
        void Start()
        {
           cmp_processVol.profile.TryGetSettings<Vignette>(out vignette);
           cmp_processVol.profile.TryGetSettings<Grain>(out grain);
        }

        // Update is called once per frame
        void Update()
        {
            if (fading)
            {
                if(vignette.intensity.value < maxVignetteIntensity)
                {
                    vignette.intensity.value += (maxVignetteIntensity - minVignetteIntensity) / fadeTime * Time.deltaTime;
                }
                if (grain.intensity.value < maxGrainIntensity)
                {
                    grain.intensity.value = (maxGrainIntensity - minGrainIntensity) / fadeTime * Time.deltaTime;
                }
            }
            else
            {
                if (vignette.intensity.value > minVignetteIntensity)
                {
                    vignette.intensity.value -= (maxVignetteIntensity - minVignetteIntensity) / fadeTime * Time.deltaTime;
                }
                if (grain.intensity.value > minGrainIntensity)
                {
                    grain.intensity.value -= (maxGrainIntensity - minGrainIntensity) / fadeTime * Time.deltaTime;
                }
            }
        }

        public void SetFadeFX(bool isFading)
        {
            fading = isFading;
        }
    }
}