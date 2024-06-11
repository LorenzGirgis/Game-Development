using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public GameObject paddle; // Reference to the paddle GameObject
    public float powerUpDuration = 3f; // Duration of the power-up effect
    public float sizeIncreaseFactor = 2f; // Factor by which paddle size increases

    private bool isPowerUpActive = false; // Flag to track if power-up is active

    // Method to activate the power-up
    public void ActivatePowerUp()
    {
        // If power-up is already active, return to prevent multiple activations
        if (isPowerUpActive)
            return;

        // Increase paddle size
        Vector3 currentScale = paddle.transform.localScale;
        paddle.transform.localScale = new Vector3(currentScale.x * sizeIncreaseFactor, currentScale.y, currentScale.z);

        // Set flag to indicate power-up is active
        isPowerUpActive = true;

        // Start a timer to deactivate the power-up after the duration
        Invoke("DeactivatePowerUp", powerUpDuration * Time.timeScale);
    }

    // Method to deactivate the power-up
    private void DeactivatePowerUp()
    {
        // Reset paddle size
        Vector3 currentScale = paddle.transform.localScale;
        paddle.transform.localScale = new Vector3(currentScale.x / sizeIncreaseFactor, currentScale.y, currentScale.z);

        // Reset flag to indicate power-up is not active
        isPowerUpActive = false;
    }
}