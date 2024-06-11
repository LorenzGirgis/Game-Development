using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public GameObject paddle; // Reference to the paddle GameObject
    public GameObject ball; // Reference to the ball GameObject
    public float powerUpDuration = 2f; // Duration of the power-up effect
    public float sizeIncreaseFactor = 2f; // Factor by which paddle size increases
    public float speedIncreaseFactor = 2f; // Factor by which ball speed increases

    private bool isPowerUpActive = false; // Flag to track if power-up is active

    // Method to activate the paddle size increase power-up
    public void ActivatePaddleSizeIncrease()
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
        Invoke("DeactivatePowerUp", powerUpDuration);
    }

    // Method to activate the ball size increase power-up
    public void ActivateBallSizeIncrease()
    {
        // If power-up is already active, return to prevent multiple activations
        if (isPowerUpActive)
            return;

        // Increase ball size
        Vector3 currentScale = ball.transform.localScale;
        ball.transform.localScale = new Vector3(currentScale.x * sizeIncreaseFactor, currentScale.y, currentScale.z);

        // Set flag to indicate power-up is active
        isPowerUpActive = true;

        // Start a timer to deactivate the power-up after the duration
        Invoke("DeactivatePowerUp", powerUpDuration);
    }

    // Method to activate the ball speed increase power-up
    public void ActivateBallSpeedIncrease()
    {
        // If power-up is already active, return to prevent multiple activations
        if (isPowerUpActive)
            return;

        // Increase ball speed
        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
        rb.velocity *= speedIncreaseFactor;

        // Set flag to indicate power-up is active
        isPowerUpActive = true;

        // Start a timer to deactivate the power-up after the duration
        Invoke("DeactivatePowerUp", powerUpDuration);
    }

    // Method to deactivate the power-up
    private void DeactivatePowerUp()
    {
        // Reset paddle size
        paddle.transform.localScale /= sizeIncreaseFactor;

        // Reset ball size
        ball.transform.localScale /= sizeIncreaseFactor;

        // Reset ball speed
        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
        rb.velocity /= speedIncreaseFactor;

        // Reset flag to indicate power-up is not active
        isPowerUpActive = false;
    }
}
