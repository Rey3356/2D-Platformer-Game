using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathOnFall : MonoBehaviour
{
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] PlayerController playerController;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.GetComponent<PlayerController>() != null)
        {
            playerHealth.health = 0;
            playerController.HurtPlayer(new Vector3(0,0,0));
        }
    }

    IEnumerator DeathReload()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
}
