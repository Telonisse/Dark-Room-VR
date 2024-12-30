using UnityEngine;

public class SwitchLightOnOff : MonoBehaviour
{
    [Tooltip("The lamp Light that go In to the lamp")]
    [SerializeField]
    public GameObject lampLightIn;

    [Tooltip("The lamp Light that go Out to the lamp")]
    [SerializeField]
    public GameObject lampLightOut;

    [Tooltip("animation for the switch")]
    [SerializeField]
    public Animator switchAnimation;
    [Tooltip("Name of bool that controll on off animation")]
    [SerializeField]
    public string boolName;

    public void ActivateLamp()   { lampLightIn.SetActive(true); lampLightOut.SetActive(true); switchAnimation.SetBool(boolName, true); }
    public void DeactivateLamp() { lampLightIn.SetActive(false); lampLightOut.SetActive(false); switchAnimation.SetBool(boolName, false); }
}
