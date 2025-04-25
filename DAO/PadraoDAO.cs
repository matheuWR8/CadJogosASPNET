using CadJogosASPNET.Models;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Collections.Generic;

namespace CadJogosASPNET.DAO
{
    public abstract class PadraoDAO<T> where T : PadraoViewModel
    {
        public PadraoDAO()
        {
            SetTabela();
        }

        protected string Tabela { get; set; }
        protected string NomeSpListar { get; set; } = "spListar";
        protected abstract SqlParameter[] CriarParametros(T model);
        protected abstract T MontarModel(DataRow registro);
        protected abstract void SetTabela();

        public virtual void Insert(T model)
        {
            HelperDAO.ExecutarProcedure("spInsert_" + Tabela, CriarParametros(model));
        }

        public virtual void Update(T model)
        {
            HelperDAO.ExecutarProcedure("spUpdate_" + Tabela, CriarParametros(model));
        }

        public virtual void Delete(int id)
        {
            var parametros = new SqlParameter[]
            {
                new SqlParameter("id", id),
                new SqlParameter("tabela", Tabela)
            };
            HelperDAO.ExecutarProcedure("spDelete", parametros);
        }

        public virtual T Consultar(int id)
        {
            var parametros = new SqlParameter[]
            {
                new SqlParameter("id", id),
                new SqlParameter("tabela", Tabela)
            };
            var tabela = HelperDAO.ExecutarProcedureSelect("spConsultar", parametros);
            if (tabela.Rows.Count == 0)
                return null;
            else
                return MontarModel(tabela.Rows[0]);
        }

        public virtual int ProximoId()
        {
            var parametro = new SqlParameter[]
            {
                new SqlParameter("tabela", Tabela)
            };
            var tabela = HelperDAO.ExecutarProcedureSelect("spProximoId", parametro);
            return Convert.ToInt32(tabela.Rows[0][0]);
        }

        public virtual List<T> Listar()
        {
            var parametros = new SqlParameter[]
            {
                new SqlParameter("tabela", Tabela),
                new SqlParameter("Ordem", "1") // 1 é o primeiro campo da tabela
            };
            var tabela = HelperDAO.ExecutarProcedureSelect(NomeSpListar, parametros);
            List<T> lista = new List<T>();
            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontarModel(registro));
            return lista;
        }
    }
}
