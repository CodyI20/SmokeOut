using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class NegativeEffects : MonoBehaviour
{
    public static NegativeEffects _negativeEffect { get; private set; }
    [HideInInspector]
    public bool isIncreasing = true;
    //GENERAL
    [Min(5f)]
    [SerializeField] private float timeTillIntensityIncreases = 10f;
    [SerializeField] private float timeTillIntensityDecreases = 5f;

    //VIGNETTE
    private Vignette vignette;
    [Range(0.05f, 0.15f)]
    [SerializeField] private float vignetteIntensityChange = 0.1f;

    //SOUNDS
    [SerializeField] private AudioSource _audioPlayed;
    [Range(0.05f, 0.1f)]
    [SerializeField] private float soundIntensityChange = 0.1f;

    private void Awake()
    {
        if (_negativeEffect == null)
            _negativeEffect = this;
        _audioPlayed = GetComponent<AudioSource>();
        VolumeProfile volumeProfile = GetComponent<Volume>()?.profile;
        if (!volumeProfile.TryGet(out vignette)) throw new System.NullReferenceException(nameof(vignette));
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeIntensityOvertime());
    }

    IEnumerator ChangeIntensityOvertime()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeTillIntensityIncreases);
            if (isIncreasing)
            {
                if (vignette != null)
                    vignette.intensity.Override((float)vignette.intensity + vignetteIntensityChange);
                if (_audioPlayed != null)
                    _audioPlayed.volume += soundIntensityChange;
            }
            else
            {
                if (vignette != null && vignette.intensity!=vignette.intensity.min)
                    vignette.intensity.Override((float)vignette.intensity - vignetteIntensityChange);
                if (_audioPlayed != null && _audioPlayed.volume > 0)
                    _audioPlayed.volume -= soundIntensityChange;

                if(vignette.intensity == vignette.intensity.min && _audioPlayed.volume == 0)
                {
                    isIncreasing = true;
                }
            }
        }
    }

    public void ClearEffects()
    {
        StopCoroutine(ChangeIntensityOvertime());
        vignette.intensity.Override(0);
        _audioPlayed.volume = 0;
        StartCoroutine(ChangeIntensityOvertime());
    }

    private void OnDestroy()
    {
        _negativeEffect = null;
    }
}
