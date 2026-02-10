using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Informações basicas")]
    [SerializeField] private int vida = 10;
    [SerializeField] private float velocidade = 2f;
    [SerializeField] private float velocidadePulo = 7f;
    [SerializeField] private int totalDePulos = 1;
    private bool morto = false;
    private float delayDano = 0f;
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
        // Componentes do player
        meuRigibody = GetComponent<Rigidbody2D>();
        meuSprite = GetComponent<Transform>();
        meuAnimacao = GetComponent<Animator>();
        meuBoxCollider = GetComponent<BoxCollider2D>();
        // variaveis do player
        this.quantidadesDePulos = totalDePulos;
    }

    // Update is called once per frame
    void Update()
    {
        if (!morto)
        {
            Movendo();
            Pulando();
            AlterandoSprites();
            Invencibilidade();
        }
    }
    void FixedUpdate()
    {
        meuAnimacao.SetBool("Nochao", IsGrounded());
        if (IsGrounded())
        {
            quantidadesDePulos = totalDePulos;
        }
    }
    private void Invencibilidade()
    {
        if (delayDano > 0f)
        {
            delayDano -= Time.deltaTime;
        }
    }
    private void Movendo()
    {
        float horizontalVelocidade = Input.GetAxis("Horizontal");
        float minhaVelocidadex = horizontalVelocidade * velocidade;
        meuRigibody.linearVelocity = new Vector2(minhaVelocidadex, meuRigibody.linearVelocityY);
        //Debug.Log($"DEBUG : Velocidade esta {minhaVelocidadex}");
        if (minhaVelocidadex != 0)
        {
            meuAnimacao.SetBool("Movendo", true);
            //Debug.Log($"Animação andando {minhaVelocidadex}");
        }
        else
        {
            meuAnimacao.SetBool("Movendo", false);
        }
    }
    private void AlterandoSprites()
    {
        float horizontalVelocidade = Input.GetAxis("Horizontal");
        float minhaVelocidadex = horizontalVelocidade * velocidade;
        meuSprite.localScale = new Vector3(Mathf.Sign(minhaVelocidadex), 1f, 1f);

    }
    private void Pulando()
    {
        meuAnimacao.SetFloat("velV", meuRigibody.linearVelocityY);
        var pulo = Input.GetButtonDown("Jump");
        if (pulo && this.quantidadesDePulos > 0)
        {
            meuRigibody.linearVelocity = new Vector2(meuRigibody.linearVelocityX, velocidadePulo);
            this.quantidadesDePulos--;
            //meuAnimacao.SetBool("Nochao", false);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ColisaoDeInimigo")) ;
        {
            if (transform.position.y >= other.transform.position.y)
            {
                Debug.Log("POr cima");
                meuRigibody.linearVelocity = new Vector2(meuRigibody.linearVelocityX, velocidadePulo);
                other.GetComponentInParent<Animator>().SetTrigger("Dano");
            }
            else
            {
                Debug.Log("Por baixo");
                if (delayDano <= 0f)
                {
                    vida--;
                    delayDano = 2f;
                    meuAnimacao.SetTrigger("Dano");
                    meuAnimacao.SetInteger("Vida", vida);
                }
            }
        }
    }
    public void Morrendo()
    {
        morto = true;
        meuRigibody.linearVelocity = Vector2.zero;
    }
    private bool IsGrounded()
    {
        bool chao = Physics2D.Raycast(meuBoxCollider.bounds.center, Vector2.down, .6f, leyerLevel);
        Color cor;
        if (chao)
        {
            cor = Color.red;
        }
        else
        {
            cor = Color.green;
        }
        Debug.DrawRay(meuBoxCollider.bounds.center, Vector2.down * 0.6f, cor);
        return chao;
    }
}
