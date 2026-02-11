using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static int vida = 3;
    [SerializeField] private int vidaInicial = 3;
    [SerializeField] private Image[] coracaes;
    
    public int GetVida()
    {
        return vida;
    }
    public void SetVida(int setVida)
    {
        vida = setVida;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //AjustaVida();
    }
    public void GameOver()
    {
        vida = vidaInicial;
        SceneManager.LoadScene("Fase1");
    }
    public void MudaCena(string destino)
    {
        SceneManager.LoadScene(destino);
    }
    public void AjustaVida()
    {
        for (var i = 0; i  < coracaes.Length; i++)
        {
            if(i < vida)
            {
                coracaes[i].enabled = true;
            }
            else
            {
                coracaes[i].enabled = false;
            }
        }
    }
}
