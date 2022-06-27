using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


public class PlayerShaderGraphController : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer playerSkinnedMeshRenderer;
    //[SerializeField] private Light pointLight;
    [SerializeField] private Gradient purpleGradient;
    [SerializeField] private PostProcessVolume vignetteProfile;
    public Vignette m_Vignette;
    private Material purpleMaterial;
    private static readonly int LerpAmount = Shader.PropertyToID("_LerpAmount");

    private void Awake()
    {
        BindEvents();
    }

    private void OnDestroy()
    {
        UnbindEvents();
        SetVignette(0);
    }

    private void BindEvents()
    {
        EventManager.onStaminaUpdated += OnStaminaUpdated;
        EventManager.onGameOver += OnGameOver;
    }

    private void UnbindEvents()
    {
        EventManager.onStaminaUpdated -= OnStaminaUpdated;
        EventManager.onGameOver -= OnGameOver;
    }

    private void Start()
    {
        purpleMaterial = playerSkinnedMeshRenderer.materials[1];
        
    }

    private void SetVignette(float ratio)
    {
        m_Vignette = (Vignette) vignetteProfile.profile.settings[2];
        // m_Vignette = (Vignette) vignetteProfile.profile.components[2];
        m_Vignette.intensity.value = ratio;
    }

    private void OnStaminaUpdated(float ratio)
    {
        ratio = Mathf.Clamp(ratio, 0f, 1f);
        Purplify(ratio);
    }

    
    private void Purplify(float ratio)
    {
        if (GameManager.Instance.GameState == eGameState.Gameplay)
        {
            //purpleMaterial.SetFloat(LerpAmount, ratio);
            var color = purpleGradient.Evaluate(ratio);
            purpleMaterial.SetColor("_Color", color);
            EventManager.SetShakeSpeed?.Invoke(ratio);
            EventManager.SetHeartbeatVolume?.Invoke(ratio);
            SetVignette(ratio);
        }

    }

    private void OnGameOver()
    {
        SetVignette(0);
    }
    
}
