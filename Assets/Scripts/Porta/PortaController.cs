using UnityEngine;

public class PortaController : MonoBehaviour
{
    private Animator meuAnimacao;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        meuAnimacao = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AbrindoPorta()
    {
        meuAnimacao.SetTrigger("Abrindo");
    }
}
