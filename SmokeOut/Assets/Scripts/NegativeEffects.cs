using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class NegativeEffects : MonoBehaviour
{
    public static NegativeEffects _negativeEffect { get; private set; }
    [HideInInspector]
    public bool isIncreasing = true;
    [HideInInspector]
    public bool choicesAppear = false;

    private float timeItStartedIncreasing = 0f;
    [Range(1f, 100f)]
    [SerializeField] private float timeAfterWhichChoicesCanAppear = 40f;

    //VIGNETTE
    private Vignette vignette;
    [Range(0.000005f, 0.0001f)]
    [SerializeField] private float vignetteIntensityChange = 0.1f;

    //SOUNDS
    [SerializeField] private AudioSource _audioPlayed;
    [Range(0.000005f, 0.0001f)]
    [SerializeField] private float soundIntensityChange = 0.1f;

    private void Awake()
    {
        if (_negativeEffect == null)
            _negativeEffect = this;
        _audioPlayed = GetComponent<AudioSource>();
        VolumeProfile volumeProfile = GetComponent<Volume>()?.profile;
        if (!volumeProfile.TryGet(out vignette)) throw new System.NullReferenceException(nameof(vignette));
    }

    private void Update()
    {
        ChangeIntensity();
        ChoicesAppear();
    }

    void ChangeIntensity()
    {
        if (isIncreasing)
        {
            if (vignette != null)
                vignette.intensity.Override((float)vignette.intensity + vignetteIntensityChange);
            if (_audioPlayed != null)
                _audioPlayed.volume += soundIntensityChange;
        }
        else
        {
            if (vignette != null && vignette.intensity != vignette.intensity.min)
                vignette.intensity.Override((float)vignette.intensity - vignetteIntensityChange*1.5f);
            if (_audioPlayed != null && _audioPlayed.volume > 0)
                _audioPlayed.volume -= soundIntensityChange*1.5f;

            if (vignette.intensity == vignette.intensity.min && _audioPlayed.volume == 0)
            {
                ClearEffects();
            }
        }
    }

    void ChoicesAppear()
    {
        if (!choicesAppear && isIncreasing && Time.timeSinceLevelLoad > timeItStartedIncreasing + timeAfterWhichChoicesCanAppear)//Make it viable for choices to work if it's decreasing (!isIncreasing)
        {
            choicesAppear = Random.Range(0, 10000) == 0;
        }
    }

    public void ClearEffects()
    {
        vignette.intensity.Override(0);
        _audioPlayed.volume = 0;
        timeItStartedIncreasing = Time.timeSinceLevelLoad;
        isIncreasing = true;
        choicesAppear = false;
    }

    private void OnDestroy()
    {
        _negativeEffect = null;
    }
}
