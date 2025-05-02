using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.DTOs.Relatorios;
using Backend.DTOs.Tarefas;

namespace Backend.Services.Interfaces
{
    public interface ITarefaService
    {
        Task<TarefaDto>              StartAsync(StartTarefaDto dto);
        Task<TarefaDto>              EndAsync(int id, EndTarefaDto dto);
        Task                         MoveAsync(int tarefaId, int projetoDestinoId);

        Task<IEnumerable<TarefaDto>> ListarEmCursoAsync(int utilizadorId);             // RF16
        Task<IEnumerable<TarefaDto>> ListarConcluidasAsync(int utilizadorId, DateTime inicio, DateTime fim); // RF17

        Task DeleteAsync(int id);                                                      // RF14-15

        Task<RelatorioMensalDto>     RelatorioMensalAsync(int utilizadorId, int ano, int mes);               // RF23-27
        Task<IEnumerable<RelatorioProjetoClienteDto>> RelatorioPorProjetoClienteAsync(int utilizadorId, int ano, int mes); // RF28

        Task<IEnumerable<TarefaDto>> GetByProjetoIdAsync(int projetoId);
        Task<IEnumerable<TarefaDto>> GetByUtilizadorIdAsync(int utilizadorId);
    }
}