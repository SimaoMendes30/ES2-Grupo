using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class Projeto
{
    public int IdProjeto { get; set; }

    public string Nome { get; set; } = null!;

    public string? NomeCliente { get; set; }

    public string? Descricao { get; set; }

    public decimal? PrecoHora { get; set; }

    public int Responsavel { get; set; }

    public DateOnly? DataCriacao { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<Membro> Membros { get; set; } = new List<Membro>();

    public virtual Utilizador ResponsavelNavigation { get; set; } = null!;

    public virtual ICollection<Tarefa> IdTarefas { get; set; } = new List<Tarefa>();
}
