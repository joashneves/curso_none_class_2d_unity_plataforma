using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Informações basicas")]
    [SerializeField] private float velocidade = 2f;
    [SerializeField] private float velocidadePulo = 7f;
    [SerializeField] private int totalDePulos = 1;
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
        Movendo();
        Pulando();
        AlterandoSprites();
    }
    void FixedUpdate()
    {
        meuAnimacao.SetBool("Nochao", IsGrounded());
        if (IsGrounded())
        {
            quantidadesDePulos = totalDePulos;    
        }
    }
    private void Movendo()
    {
        float horizontalVelocidade = Input.GetAxis("Horizontal");
        float minhaVelocidadex = horizontalVelocidade * velocidade;
        meuRigibody.linearVelocity = new Vector2(minhaVelocidadex, meuRigibody.linearVelocityY);
        //Debug.Log($"DEBUG : Velocidade esta {minhaVelocidadex}");
        if(minhaVelocidadex != 0)
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
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Chao"))
        {
           // quantidadesDePulos = totalDePulos;
            //meuAnimacao.SetBool("Nochao", true);
        }
    }
    void OnCollisionExit2D(Collision2D other)
    {
        
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
