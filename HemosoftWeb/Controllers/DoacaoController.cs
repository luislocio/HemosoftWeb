﻿using Domain.Enum;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Repository.DAL;
using System;

namespace HemosoftWeb.Controllers
{
    public class DoacaoController : Controller
    {
        // TODO: REMOVER TRIADOR
        private Triador triador;

        private readonly TriadorDAO _triadorDAO;

        private readonly DoacaoDAO _doacaoDAO;
        private readonly DoadorDAO _doadorDAO;

        public DoacaoController(DoacaoDAO doacaoDAO, DoadorDAO doadorDAO, TriadorDAO triadorDAO)
        {
            _doacaoDAO = doacaoDAO;
            _doadorDAO = doadorDAO;

            //TODO: REMOVER TRIADOR
            _triadorDAO = triadorDAO;
            this.triador = _triadorDAO.BuscarTriadorPorId(1);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Buscar()
        {
            return View();
        }

        public IActionResult Cadastrar(int? id)
        {
            ViewBag.idDoador = id;
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(Doacao doacao)
        {
            ModelState.Remove("Doador.Cpf");
            ModelState.Remove("Doador.NomeCompleto");
            ModelState.Remove("Doador.Genero");
            ModelState.Remove("Doador.EstadoCivil");
            ModelState.Remove("Doador.TipoSanguineo");
            ModelState.Remove("Doador.FatorRh");

            if (ModelState.IsValid)
            {
                DateTime dataHoje = DateTime.Now;
                Doador doador = _doadorDAO.BuscarDoadorPorId(doacao.Doador.IdDoador);
                doador.UltimaDoacao = dataHoje;

                // Informações do formulário.
                ImpedimentosDefinitivos impedimentosDefinitivos = CriarImpedimentosDefinitivos(doacao);
                ImpedimentosTemporarios impedimentosTemporarios = CriarImpedimentosTemporarios(doacao);
                TriagemClinica triagemClinica = CriarTriagemClinica(doacao);

                // Informações que serão preenchidas após recebimento do exame laboratorial.
                TriagemLaboratorial triagemLaboratorial = new TriagemLaboratorial { };

                doacao = CriarDoacao(impedimentosTemporarios, triagemClinica, impedimentosDefinitivos, triagemLaboratorial, doador, triador, dataHoje);

                int idDoacao = _doacaoDAO.CadastrarDoacao(doacao);

                // TODO: [FEEDBACK] - Mostrar mensagem de sucesso.
                return RedirectToAction("perfil", new RouteValueDictionary { { "id", idDoacao } });
            }

            ViewBag.idDoador = doacao.Doador.IdDoador;
            return View(doacao);
        }

        public IActionResult Listar()
        {
            Doacao doacao = new Doacao() { StatusDoacao = StatusDoacao.Disponivel };

            return View(_doacaoDAO.BuscarDoacaoPorStatus(doacao));
        }

        public IActionResult Perfil(int? id)
        {
            Doacao resultadoDaBusca = _doacaoDAO.BuscarDoacaoPorId(id);
            return View(resultadoDaBusca);
        }

        public IActionResult ConfirmarColeta(int? id)
        {
            Doacao doacao = _doacaoDAO.BuscarDoacaoPorId(id);
            doacao.StatusDoacao = StatusDoacao.AguardandoResultados;
            _doacaoDAO.AlterarDoacao(doacao);

            long codigoGerado = long.Parse(doacao.Doador.Cpf) + DateTime.Now.Ticks;

            return RedirectToAction("desconto", new RouteValueDictionary { { "codigoPromocional", codigoGerado } });
        }

        public IActionResult Desconto(long? codigoPromocional)
        {
            ViewBag.codigoPromocional = codigoPromocional.ToString();

            return View();
        }

        #region Validação de status e atributos

        private StatusDoacao GetStatusDoacao(TriagemClinica triagemClinica, ImpedimentosDefinitivos impedimentosDefinitivos)
        {
            if (triagemClinica.StatusTriagem == StatusTriagem.Aprovado &&
                impedimentosDefinitivos.AntecedenteAvc == false)
            {
                return StatusDoacao.AguardandoAtendimento;
            }

            return StatusDoacao.NaoDisponivel;
        }

        private StatusTriagem GetStatusTriagemClinica(ImpedimentosTemporarios impedimentosTemporarios)
        {
            if (impedimentosTemporarios.BebidaAlcoolica == false &&
                impedimentosTemporarios.Gravidez == Gravidez.Nenhuma &&
                impedimentosTemporarios.Gripe == false &&
                impedimentosTemporarios.Tatuagem == false)
            {
                return StatusTriagem.Aprovado;
            }

            return StatusTriagem.Reprovado;
        }

        #endregion Validação de status e atributos

        #region Criação de objetos para cadastro

        private Doacao CriarDoacao(ImpedimentosTemporarios impedimentosTemporarios, TriagemClinica triagemClinica, ImpedimentosDefinitivos impedimentosDefinitivos, TriagemLaboratorial triagemLaboratorial, Doador doador, Triador triador, DateTime dataHoje)
        {
            return new Doacao
            {
                DataDoacao = dataHoje,
                Doador = doador,
                Triador = triador,
                TriagemClinica = triagemClinica,
                TriagemLaboratorial = triagemLaboratorial,
                StatusDoacao = GetStatusDoacao(triagemClinica, impedimentosDefinitivos),
                ImpedimentosTemporarios = impedimentosTemporarios,
                ImpedimentosDefinitivos = impedimentosDefinitivos
            };
        }

        private ImpedimentosDefinitivos CriarImpedimentosDefinitivos(Doacao doacao)
        {
            return new ImpedimentosDefinitivos { AntecedenteAvc = doacao.ImpedimentosDefinitivos.AntecedenteAvc };
        }

        private ImpedimentosTemporarios CriarImpedimentosTemporarios(Doacao doacao)
        {
            return new ImpedimentosTemporarios
            {
                BebidaAlcoolica = doacao.ImpedimentosTemporarios.BebidaAlcoolica,
                BebidaAlcoolicaUltimaVez = doacao.ImpedimentosTemporarios.BebidaAlcoolicaUltimaVez,
                Gravidez = doacao.ImpedimentosTemporarios.Gravidez,
                GravidezUltimaVez = doacao.ImpedimentosTemporarios.GravidezUltimaVez,
                Gripe = doacao.ImpedimentosTemporarios.Gripe,
                GripeUltimaVez = doacao.ImpedimentosTemporarios.GripeUltimaVez,
                Tatuagem = doacao.ImpedimentosTemporarios.Tatuagem,
                TatuagemUltimaVez = doacao.ImpedimentosTemporarios.TatuagemUltimaVez
            };
        }

        private TriagemClinica CriarTriagemClinica(Doacao doacao)
        {
            return new TriagemClinica
            {
                Peso = doacao.TriagemClinica.Peso,
                Pulso = doacao.TriagemClinica.Pulso,
                Temperatura = doacao.TriagemClinica.Temperatura,
                StatusTriagem = GetStatusTriagemClinica(doacao.ImpedimentosTemporarios)
            };
        }

        #endregion Criação de objetos para cadastro
    }
}