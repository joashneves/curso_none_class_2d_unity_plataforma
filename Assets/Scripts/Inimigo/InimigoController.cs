using UnityEngine;

public class InimigoController : MonoBehaviour
{
    [Header("Informações basicas")]
    [SerializeField] private float velocidade = 2f;
    [SerializeField] private float tempoDeEspera = 2f;
    [Header("Informações do raycast")]
    [SerializeField] private LayerMask leyerLevel;
    private int quantidadesDePulos = 1;
    private Rigidbody2D meuRigibody;
    private Transform meuSprite;
    private Animator meuAnimacao;
    private BoxCollider2D meuBoxCollider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        meuRigibody = GetComponent<Rigidbody2D>();
        meuSprite = GetComponent<Transform>();
        meuAnimacao = GetComponent<Animator>();
        
        meuRigibody.linearVelocity = new Vector2(velocidade, meuRigibody.linearVelocityX);
        transform.localScale = new Vector3(Mathf.Sign(meuRigibody.linearVelocityX) * -1, 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
     Movendo();   
    }
    private void Movendo()
    {
        if(tempoDeEspera <= 0)
        {
            var indoPara = Random.Range(-1, 2);
            meuRigibody.linearVelocity = new Vector2(velocidade * indoPara, meuRigibody.linearVelocityX);
            if(meuRigibody.linearVelocityX != 0)transform.localScale = new Vector3(Mathf.Sign(meuRigibody.linearVelocityX) * -1, 1f, 1f);
            
            tempoDeEspera = Random.Range(2f, 10f);
        }
        else
        {
            tempoDeEspera -= Time.deltaTime;
        }
        meuAnimacao.SetBool("Movendo", meuRigibody.linearVelocityX != 0);
    }
}
