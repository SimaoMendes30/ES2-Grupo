using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class ProjectEntity
{
    public int IdProjeto { get; set; }

    public string Nome { get; set; } = null!;

    public string? NomeCliente { get; set; }

    public string? Descricao { get; set; }

    public decimal? PrecoHora { get; set; }

    public int Responsavel { get; set; }

    public DateTimeOffset DataCriacao { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<MemberEntity> Membros { get; set; } = new List<MemberEntity>();

    public virtual UserEntity ResponsavelNavigation { get; set; } = null!;

    public virtual ICollection<TaskEntity> IdTarefas { get; set; } = new List<TaskEntity>();
}
