using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class MemberEntity
{
    public int IdMembro { get; set; }

    public int IdUtilizador { get; set; }

    public int IdProjeto { get; set; }

    public DateTime DataConvite { get; set; }

    public DateTime? DataEstado { get; set; }

    public string EstadoConvite { get; set; } = null!;

    public string? EstadoAtividade { get; set; }

    public virtual ProjectEntity IdProjectEntityNavigation { get; set; } = null!;

    public virtual UserEntity IdUserEntityNavigation { get; set; } = null!;
}
