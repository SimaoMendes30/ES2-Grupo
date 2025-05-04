namespace Backend.Domain.DTOs.Project
{
    public class ProjectDetailsDto
    {
        public int IdProjeto { get; set; }

        public string Nome { get; set; } = null!;
        
        public string? NomeCliente { get; set; }
        
        public string? Descricao { get; set; }
        
        public decimal? PrecoHora { get; set; }

        public int Responsavel { get; set; }

        public DateTime DataCriacao { get; set; }

        public bool IsDeleted { get; set; }
    }
}