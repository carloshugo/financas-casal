﻿using System;
using FinancasCasal.Models.Enums;
using System.Collections.Generic;

namespace FinancasCasal.Models
{
    public class Conta {
        public int Id { get; set; }
        public string Banco { get; set; }
        public int CodigoAgencia { get; set; }
        public int CodigoConta { get; set; }
        public TipoConta TipoConta { get; set; }
        public double Saldo { get; set; }
        public ICollection<Transacao> Transacoes { get; set; } = new List<Transacao>();

        public Conta()
        {
        }

        public Conta(int id, string banco, int codigoAgencia, int codigoConta, TipoConta tipoConta)
        {
            Id = id;
            Banco = banco;
            CodigoAgencia = codigoAgencia;
            CodigoConta = codigoConta;
            TipoConta = tipoConta;
        }

        public void AdicionarTransacao(Transacao transacao)
        {
            Transacoes.Add(transacao);
        }

        public void RemoverTransacao(Transacao transacao)
        {
            Transacoes.Remove(transacao);
        }
    }
}