using UnityEngine;

public class BossCinematicManager : MonoBehaviour
{
    public GameObject golem;            // El Golem real (desactivado al inicio)
    public GameObject golemRocks;       // El padre de las piedras animadas
    public GameObject bossHealthBar;    // La barra de vida del Boss
    public GameObject bossCam;
    
    
    public void ActivateGolem()
    {
        if (golem != null) golem.SetActive(true);
        if (golemRocks != null) golemRocks.SetActive(false);
        if (bossHealthBar != null) bossHealthBar.SetActive(true);
    }
    public void DeactivateBossCam()
    {
        if (bossCam != null) bossCam.SetActive(false);
    }
}