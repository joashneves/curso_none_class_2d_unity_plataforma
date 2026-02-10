using UnityEngine;

public class InimigoController : MonoBehaviour
{
    [Header("Informações basicas")]
    [SerializeField] private float velocidade = 2f;
    [SerializeField] private float tempoDeEspera = 2f;
    [SerializeField] private BoxCollider2D colisorFilho;
    private bool morto = false;
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
        meuBoxCollider = GetComponent<BoxCollider2D>();
        meuRigibody.linearVelocity = new Vector2(velocidade, meuRigibody.linearVelocityX);
        transform.localScale = new Vector3(Mathf.Sign(meuRigibody.linearVelocityX) * -1, 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
    }
    void FixedUpdate()
    {
        if (!morto)
        {
            Movendo();   
            
        }
    }
    private void Movendo()
    {
        if (BatendoNaParade())
        {
            meuRigibody.linearVelocity = new Vector2(meuRigibody.linearVelocityX * -1f, meuRigibody.linearVelocityY);
        }
        if(meuRigibody.linearVelocityX != 0)transform.localScale = new Vector3(Mathf.Sign(meuRigibody.linearVelocityX) * -1, 1f, 1f);

        if(tempoDeEspera <= 0)
        {
            var indoPara = Random.Range(-1, 2);
            meuRigibody.linearVelocity = new Vector2(velocidade * indoPara, meuRigibody.linearVelocityY);
           
            tempoDeEspera = Random.Range(2f, 10f);
            if(indoPara == 0)tempoDeEspera = Random.Range(2f, 4f);
            Debug.Log($"INIMIGO INDO PARA : {indoPara}, com {tempoDeEspera}");
        }
        else
        {
            tempoDeEspera -= Time.deltaTime;
        }
        meuAnimacao.SetBool("Movendo", meuRigibody.linearVelocityX != 0);
    }
    private bool BatendoNaParade()
    {
        var dir = new Vector2(Mathf.Sign(meuRigibody.linearVelocityX), 0f);
        bool parede = Physics2D.Raycast(meuBoxCollider.bounds.center, dir, 1f, leyerLevel);
        return parede;
    }
    public void Morrendo()
    {
        morto = true;
        meuRigibody.linearVelocity = Vector2.zero;
        colisorFilho.enabled = false;
        //Destroy(gameObject, 2f);
    }
}
