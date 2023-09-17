using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class NegativeEffects : MonoBehaviour
{
    //GENERAL
    [Min(10f)]
    [SerializeField] private float timeTillIntensityIncreases = 10f;

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
        _audioPlayed = GetComponent<AudioSource>();
        VolumeProfile volumeProfile = GetComponent<Volume>()?.profile;
        if (!volumeProfile.TryGet(out vignette)) throw new System.NullReferenceException(nameof(vignette));
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(IncreaseIntensityOverTime());
    }

    private void Update()
    {
        ClearEffects();
    }

    IEnumerator IncreaseIntensityOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeTillIntensityIncreases);
            if (vignette != null)
                vignette.intensity.Override((float)vignette.intensity + vignetteIntensityChange);
            if (_audioPlayed != null)
                _audioPlayed.volume += soundIntensityChange;
        }
    }

    void ClearEffects()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            vignette.intensity.Override(0);
            _audioPlayed.volume = 0;
        }
    }
}
