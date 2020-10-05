using UnityEngine;

namespace LD47.Core
{
    public class AudioController : MonoBehaviour
    {
        [SerializeField] [Range(0, 20000)] float maxFilterCutoff = 15000f;
        [SerializeField] [Range(0, 20000)] float minFilterCutoff = 400f;
        [SerializeField] [Range(0, 1)] float maxDelay = .4f;
        [SerializeField] [Range(0, 1)] float minDelay = 0f;

        [SerializeField] [Range(0, 3)] float fadeTime = 1f;

        MusicPlayer obj_musicPlayer;
        AudioLowPassFilter cmp_filter;
        AudioEchoFilter cmp_echo;
        [SerializeField] bool fading = false;

        private void Awake()
        {
        }
        void Start()
        {
            obj_musicPlayer = FindObjectOfType<MusicPlayer>();
            cmp_filter = obj_musicPlayer.GetComponent<AudioLowPassFilter>();
            cmp_echo = obj_musicPlayer.GetComponent<AudioEchoFilter>();
        }

        void Update()
        {
            if (fading)
            {
                if (cmp_filter.cutoffFrequency > minFilterCutoff)
                {
                    cmp_filter.cutoffFrequency -= (maxFilterCutoff - minFilterCutoff) / fadeTime * Time.deltaTime;
                }
                if (cmp_echo.wetMix < maxFilterCutoff)
                {
                    cmp_echo.wetMix += (maxDelay - minDelay) / fadeTime * Time.deltaTime;
                }              
            }
            else
            {
                if (cmp_filter.cutoffFrequency < maxFilterCutoff)
                {
                    cmp_filter.cutoffFrequency += (maxFilterCutoff - minFilterCutoff) / fadeTime * Time.deltaTime;
                }
                            
                if (cmp_echo.wetMix > minDelay)
                {
                    cmp_echo.wetMix -= (maxDelay - minDelay) / fadeTime * Time.deltaTime;
                }
            }
        }

        public void SetFadeFX(bool isFading)
        {
            fading = isFading;
        }
    }
}