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

    public int IdUtilizador { get; set; }

    public DateOnly? DataCriacao { get; set; }

    public virtual Utilizador IdUtilizadorNavigation { get; set; } = null!;

    public virtual ICollection<Membro> Membros { get; set; } = new List<Membro>();

    public virtual ICollection<Tarefa> IdTarefas { get; set; } = new List<Tarefa>();
}
