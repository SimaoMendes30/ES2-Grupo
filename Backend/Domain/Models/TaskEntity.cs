using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class TaskEntity
{
    public int IdTarefa { get; set; }

    public string Titulo { get; set; } = null!;

    public string? Descricao { get; set; }

    public int Responsavel { get; set; }

    public DateTime DataInicio { get; set; }

    public DateTime? DataFim { get; set; }

    public string Estado { get; set; } = null!;

    public decimal? PrecoHora { get; set; }

    public bool IsDeleted { get; set; }

    public virtual UserEntity ResponsavelNavigation { get; set; } = null!;

    public virtual ICollection<ProjectEntity> IdProjetos { get; set; } = new List<ProjectEntity>();

    public virtual ICollection<UserEntity> IdUtilizadors { get; set; } = new List<UserEntity>();
}
