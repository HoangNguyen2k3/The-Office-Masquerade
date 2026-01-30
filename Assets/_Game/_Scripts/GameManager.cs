using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject m_tilemap_1;
    [SerializeField] private GameObject m_tilemap_2;

    [SerializeField] private GameInput m_gameInput;

    private void Start()
    {
        m_gameInput.OnSwapClicked += GameInput_OnSwapClicked;
    }

    private void GameInput_OnSwapClicked(object sender, System.EventArgs e)
    {
        if (m_tilemap_1.activeSelf)
        {
            m_tilemap_1.SetActive(false);
            m_tilemap_2.SetActive(true);
        }
        else
        {
            m_tilemap_2.SetActive(false);
            m_tilemap_1.SetActive(true);
        }
    }
}
