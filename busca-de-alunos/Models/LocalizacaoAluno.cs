using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace busca_de_alunos.Models
{
    public class LocalizacaoAluno
    {
        [Key]
        [StringLength(50, ErrorMessage ="Matrícula deve ter no máximo 50 caracteres" )]
        [Column("Matricula")]
        [Required(ErrorMessage = "Matrícula é um campo obrigatório.")]
        public string Matricula { get; set; }

        [StringLength(200, ErrorMessage = "Nome deve ter no máximo 200 caracteres")]
        [Column("Nome")]
        [Required(ErrorMessage = "Nome é um campo obrigatório.")]
        public string Nome { get; set; }

        [Column("Endereço")]
        [StringLength(500, ErrorMessage = "Endereço deve ter no máximo 500 caracteres")]
        [Required(ErrorMessage = "Endereço é um campo obrigatório.")]
        public string Endereco { get; set; }
    }

    public class LocalizacaoAlunoContext : DbContext
    {
        public DbSet<LocalizacaoAluno> LocalizacaoAluno { get; set; }
    }
}