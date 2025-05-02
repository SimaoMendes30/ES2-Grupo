using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class Tarefa
{
    public int IdTarefa { get; set; }

    public string Descricao { get; set; } = null!;

    public DateOnly DataInicio { get; set; }

    public DateOnly? DataFim { get; set; }

    public string Estado { get; set; } = null!;

    public int Responsavel { get; set; }

    public DateTime? DataHoraInicio { get; set; }

    public decimal? PrecoHora { get; set; }

    public virtual ICollection<Projeto> IdProjetos { get; set; } = new List<Projeto>();

    public virtual ICollection<Utilizador> IdUtilizadors { get; set; } = new List<Utilizador>();
}
