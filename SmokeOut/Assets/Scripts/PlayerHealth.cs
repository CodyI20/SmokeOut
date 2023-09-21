using UnityEngine;
using System;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth _playerHealth { get; private set; }

    public static event Action OnPlayerDeath;

    [SerializeField] private float _health = 3;

    public float health
    {
        get { return _health; }
    }

    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        if (_playerHealth == null)
            _playerHealth = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (text != null)
        {
            UpdatePlayerHealthText();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdatePlayerHealthText()
    {
        text.text = $"Lives: {_health}";
    }

    public void TakeDamage()
    {
        _health--;
        UpdatePlayerHealthText();

        if (health <= 0)
        {
            OnPlayerDeath?.Invoke();
        }
    }

    private void OnDestroy()
    {
        _playerHealth = null;
    }
}
