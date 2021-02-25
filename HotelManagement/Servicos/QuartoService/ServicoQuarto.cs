using System;
using System.Collections.Generic;
using System.Text;
using HotelManagement.Servicos.QuartoService.Models;

namespace HotelManagement.Servicos.QuartoService
{
    class ServicoQuarto
    {

        public static List<ConsultarStatus> BuscarPorStatus(string situacao, int tipo)
        {
            List<ConsultarStatus> listaQuartos = new List<ConsultarStatus>(); 
            var statusId = Dados.Data.ListaSituacaoQuartos.Find(s => s.Descricao == situacao);
            Dados.Data.ListaQuartos.FindAll(quarto => quarto.SituacaoId == statusId.SituacaoId && quarto.TipoId == tipo).ForEach(q => {
                var quarto = new ConsultarStatus() {
                    QuartoId = q.QuartoId,
                    SituacaoId = q.SituacaoId
                };
                listaQuartos.Add(quarto);
            });
            return listaQuartos;
        }


        public static SituacaoQuarto ObterSituação(int id)
        {
            var situacao = Dados.Data.ListaSituacaoQuartos.Find(s => s.SituacaoId == id);
            return new SituacaoQuarto() { Descricao = situacao.Descricao };
        }

        public static TipoQuarto ObterTipoQuarto(int tipo)
        {
            var tipoQuarto = Dados.Data.ListaTipoQuarto.Find(t => t.TipoId == tipo);
            return new TipoQuarto() { Descricao = tipoQuarto.Descricao, Valor = tipoQuarto.Valor };
        }


    }
}
