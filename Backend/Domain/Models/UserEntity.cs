using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

public partial class UserEntity
{
    public int IdUtilizador { get; set; }

    public string Nome { get; set; } = null!;

    public int? NumHoras { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool Admin { get; set; }

    public bool SuperUser { get; set; }
    
    [Column("is_deleted")]
    public bool IsDeleted { get; set; }

    public virtual ICollection<MemberEntity> Membros { get; set; } = new List<MemberEntity>();

    public virtual ICollection<ProjectEntity> Projetos { get; set; } = new List<ProjectEntity>();

    public virtual ICollection<TaskEntity> Tarefas { get; set; } = new List<TaskEntity>();

    public virtual ICollection<TaskEntity> IdTarefas { get; set; } = new List<TaskEntity>();
}
