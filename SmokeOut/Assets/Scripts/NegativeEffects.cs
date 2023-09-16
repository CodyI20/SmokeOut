using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class NegativeEffects : MonoBehaviour
{
    private Vignette vignette;
    [Min(10f)]
    [SerializeField] private float timeTillIntensityIncreases = 5f;
    [Range(0.05f, 0.2f)]
    [SerializeField] private float amountOfIntesityChange = 0.1f;

    private void Awake()
    {
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
            vignette.intensity.Override((float)vignette.intensity + amountOfIntesityChange);
        }
    }

    void ClearEffects()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            vignette.intensity.Override(0);
        }
    }
}
