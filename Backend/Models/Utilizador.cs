using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class Utilizador
{
    public int IdUtilizador { get; set; }

    public string Nome { get; set; } = null!;

    public int? NumHoras { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool Admin { get; set; }

    public bool SuperUser { get; set; }

    public virtual ICollection<Membro> Membros { get; set; } = new List<Membro>();

    public virtual ICollection<Projeto> Projetos { get; set; } = new List<Projeto>();

    public virtual ICollection<Tarefa> IdTarefas { get; set; } = new List<Tarefa>();
}
