using UnityEngine;

public class PortaController : MonoBehaviour
{
    [SerializeField] private string destino = null;
    private Animator meuAnimacao;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        meuAnimacao = GetComponent<Animator>();
    }
    public void AbrindoPorta()
    {
        meuAnimacao.SetTrigger("Abrindo");
    }
    public void IndoParaDestino()
    {
        FindAnyObjectByType<GameManager>().MudaCena(destino);
    }
    public bool TenhoDestino()
    {
        return destino != "";
    }
}
