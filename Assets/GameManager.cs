using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class GameManager : MonoBehaviour
{
    public XRNode inputSource; // Set to LeftHand or RightHand in Inspector
    private InputDevice device;

    void Start()
    {
        // Get the input device for the specified controller
        device = InputDevices.GetDeviceAtXRNode(inputSource);
    }

    void Update()
    {
        // Check for primary button press (A button on Oculus or equivalent)
        if (device.TryGetFeatureValue(CommonUsages.primaryButton, out bool buttonPressed) && buttonPressed)
        {
            StartGame(); // Call the StartGame function if the button is pressed
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene"); // Replace with your scene name
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
