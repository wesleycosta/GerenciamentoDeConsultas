using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using System.Collections.Generic;

namespace ProjetoIntegrado.View.Consultas
{
    using Model;
    using Funcoes;
    using ViewUtil;
    using Mensagens;
    using MahApps.Metro.Controls.Dialogs;

    public partial class CadConsultasWin
    {
        #region  PROPRIEDADES E CTOR

        public bool cadastrou { get; set; }

        private List<FuncionarioModel> oftals = new List<FuncionarioModel>();
        private List<ConvenioModel> convenios = new List<ConvenioModel>();
        private List<PagamentoModel> pagamentos = new List<PagamentoModel>();
        private List<MaterialCirurgiaModel> materiais = new List<MaterialCirurgiaModel>();
        private List<EquipeCirurgiaModel> equipes = new List<EquipeCirurgiaModel>();

        private readonly bool cadastrar;
        private ConsultaModel consulta;
        private bool carregouCliente;

        public CadConsultasWin()
        {
            InitializeComponent();
            Iniciar();
            cadastrar = true;

            Title = "NOVA CONSULTA";

            tabControl.RemoveFromSource(paginaHistorico);
            tabControl.RemoveFromSource(paginaCirurgia);
            tabControl.RemoveFromSource(paginaDiagnostico);

            lbCodigo.Visibility = lbCodigoText.Visibility = Visibility.Hidden;
        }

        public CadConsultasWin(ConsultaModel consulta)
        {
            InitializeComponent();

            this.consulta = consulta;
            Iniciar();
            cadastrar = false;

            Title = $"CONSULTA Nº {consulta.id:D3}  [{consulta.cliente.nome}]";
            imgProcurar.IsEnabled = bordaProcurar.IsEnabled = false;
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            tbNome.Focus();

            if (!cadastrar)
                CarregarConsulta();
        }

        private void Iniciar()
        {
            cbUf.ItemsSource = Model.Estado.EstadoModel.Siglas;
            tbValor.KeyDown += ValidarEntrada.Real_KeyPress;

            imgProcurar.BitmapToImageSource(Icons.Find_16x16);

            tbData.SelectedDate = DateTime.Now;

            CarregarOftals();
            CarregarConvenios();

            tbCpf.LostFocus += (o, a) =>
            {
                if (cadastrar && !carregouCliente)
                    CarregarPorCPF();
            };


            tbValorMedico.KeyDown += ValidarEntrada.Real_KeyPress;

            tbEsfericoODLonge.KeyDown += ValidarEntrada.RealComValorNegativo_KeyPress;
            tbEsfericoOELonge.KeyDown += ValidarEntrada.RealComValorNegativo_KeyPress;

            tbEsfericoODPerto.KeyDown += ValidarEntrada.RealComValorNegativo_KeyPress;
            tbEsfericoOEPerto.KeyDown += ValidarEntrada.RealComValorNegativo_KeyPress;

            tbCilindroODLonge.KeyDown += ValidarEntrada.RealComValorNegativo_KeyPress;
            tbCilindroOELonge.KeyDown += ValidarEntrada.RealComValorNegativo_KeyPress;

            tbCilindroODPerto.KeyDown += ValidarEntrada.RealComValorNegativo_KeyPress;
            tbCilindroOEPerto.KeyDown += ValidarEntrada.RealComValorNegativo_KeyPress;

            tbAdicaoODLonge.KeyDown += ValidarEntrada.RealComValorNegativo_KeyPress;
            tbAdicaoOELonge.KeyDown += ValidarEntrada.RealComValorNegativo_KeyPress;

            tbAdicaoODPerto.KeyDown += ValidarEntrada.RealComValorNegativo_KeyPress;
            tbAdicaoOEPerto.KeyDown += ValidarEntrada.RealComValorNegativo_KeyPress;

            tbEixoODLonge.KeyDown += ValidarEntrada.RealComValorNegativo_KeyPress;
            tbEixoOELonge.KeyDown += ValidarEntrada.RealComValorNegativo_KeyPress;

            tbEixoODPerto.KeyDown += ValidarEntrada.RealComValorNegativo_KeyPress;
            tbEixoOEPerto.KeyDown += ValidarEntrada.RealComValorNegativo_KeyPress;
        }

        #endregion

        #region CARREGAR DADOS NA TELA

        #region CONSULTA, CLIENTE E ENDEREÇO

        private void CarregarConsulta()
        {
            SetarMedico(consulta.medico);
            SetarConvenio(consulta.convenio);
            tbNumeroProcediemnto.Text = consulta.numeroProcedimento;

            cbTipo.SelectedIndex = consulta.formaDeAtentimento == FormaDeAtendimento.Particular ? 0 : 1;
            cbStatusPagamento.SelectedIndex = consulta.statusPagamento == StatusPagamento.Pendente ? 0 : 1;
            cbRetorno.SelectedIndex = consulta.retorno ? 1 : 0;

            tbData.SelectedDate = consulta.data.Date;
            tbHorario.Text = consulta.horario.ToString(@"hh\:mm");
            tbValor.Text = consulta.valor.ToString("n");

            CarregarCliente(consulta?.cliente);
            CarregarEndereco(consulta?.cliente?.endereco);

            CarregarPagamentos();
            CarregarLabelExtrato();
            CarregarCirurgia();
            CarregarDiagnostico();
        }

        private void CarregarLabelExtrato()
        {
            lbValorConsulta.Content = $"{consulta.valor:n}";
            lbTotalPagamento.Content = $"{consulta.TotalPagamento():n}";
            lbDebito.Content = $"{consulta.CalcularDebito():n}";
        }

        private void CarregarCliente(ClienteModel cliente)
        {
            lbCodigo.Content = cliente.id.ToString("D3");
            tbNome.Text = cliente.nome;
            tbCpf.Text = cliente.cpf;
            cbGenero.SelectedIndex = cliente.genero == Genero.Masculino ? 0 : 1;

            tbDataNascimento.Text = cliente.dataDeNascimento.ToString("d");

            tbDddCel.Text = cliente.dddCel.Trim();
            tbCelular.Text = cliente.celular.Trim();
            tbDddTel.Text = cliente.dddTel;
            tbTelefone.Text = cliente.telefone;
            tbEmail.Text = cliente.email;

            CarregarEndereco(cliente.endereco);

            var historico = cliente.Historio();

            lvwHistorico.ItemsSource = historico;

            tbNumeroDiagnosticos.Content = $"{historico.Count:D2}";

            lbHistoricoTotalConsultas.Content = historico.Count.ToString("D3");
            lbHistoricoTotalCanceladas.Content = historico
                                                .Count(x => x.tipoDeConsulta == TipoDeConsulta.Cancelado || x.tipoDeConsulta == TipoDeConsulta.NaoCompareceu)
                                                .ToString("D3");
        }

        private void CarregarEndereco(EnderecoModel endereco)
        {
            tbCep.Text = endereco.cep;
            tbCidade.Text = endereco.cidade;
            cbUf.SelectedItem = endereco.uf;
            tbBairro.Text = endereco.bairro;
            tbLogradouro.Text = endereco.logradouro;
            tbNumero.Text = endereco.numero;
            tbComplemento.Text = endereco.complemento;
        }

        #endregion

        #region CARREGAR COMBO

        private void SetarMedico(FuncionarioModel medico)
        {
            var index = oftals.IndexOf(oftals.FirstOrDefault(x => x.id == medico.id));

            if (index >= 0)
                cbMedicos.SelectedIndex = index;
            else
            {
                cbMedicos.Items.Add(medico.nome);
                oftals.Add(medico);
                cbMedicos.SelectedIndex = 0;
            }
        }

        private void SetarConvenio(ConvenioModel convenio)
        {
            var index = convenios.IndexOf(convenios.FirstOrDefault(x => x.id == convenio?.id));

            if (index >= 0)
                cbConvenio.SelectedIndex = index;
            else if (convenio != null)
            {
                cbConvenio.Items.Add(convenio.nome);
                convenios.Add(convenio);
                cbConvenio.SelectedIndex = 1;
            }
        }

        private void CarregarPorCPF()
        {
            var cpf = Mascara.Remover(tbCpf.Text);
            var cliente = ClienteModel.CarregarTodos().FirstOrDefault(x => x.cpf == cpf);

            if (cliente != null)
            {
                CarregarCliente(cliente);
                consulta = new ConsultaModel { cliente = cliente };
                carregouCliente = true;
            }
        }

        #endregion

        #endregion

        #region CARREGAR DADOS

        private void CarregarOftals()
        {
            cbMedicos.Items.Clear();
            oftals = FuncionarioModel.CarregarMedicos();

            foreach (var f in oftals)
                cbMedicos.Items.Add(f.nome);
        }

        private void CarregarConvenios()
        {
            cbConvenio.Items.Clear();
            cbConvenio.Items.Add("Selecione...");
            convenios = ConvenioModel.CarregarTodos();

            foreach (var c in convenios)
                cbConvenio.Items.Add(c.nome);
        }

        private void CarregarPagamentos()
        {
            consulta.CarregarPagamentos();
            pagamentos = consulta.listaDePagamentos;

            lvwPagamentos.ItemsSource = pagamentos;
        }

        private void CarregarCirurgia()
        {
            tbLocal.Text = consulta?.cirgurgia?.local ?? "";
            tbValorMedico.Text = $"{consulta?.cirgurgia?.valor ?? 0:n}";

            CarregarEquipe();
            CarregarMaterial();
        }

        private void CarregarMaterial()
        {
            materiais = new MaterialCirurgiaModel().CarregarPorIdConsulta(consulta?.id ?? 0);
            lvwMaterial.ItemsSource = materiais;

            tbTotalMaterial.Content = $"{materiais.Sum(x => x.valorTotal):n}";
        }

        private void CarregarEquipe()
        {
            equipes = new EquipeCirurgiaModel().CarregarPorIdConsulta(consulta?.id ?? 0);
            lvwEquipe.ItemsSource = equipes;

            tbTotalEquipe.Content = $"{equipes.Count:D2}";
        }

        private void CarregarDiagnostico()
        {
            #region LONGE

            // OLHO DIREITO
            tbEsfericoODLonge.Text = $"{consulta?.receita?.olhoDireitoLonge?.esferico ?? 0:n}";
            tbCilindroODLonge.Text = $"{consulta?.receita?.olhoDireitoLonge?.cilindro ?? 0:n}";
            tbAdicaoODLonge.Text = $"{consulta?.receita?.olhoDireitoLonge?.adicao ?? 0:n}";
            tbEixoODLonge.Text = $"{consulta?.receita?.olhoDireitoLonge?.eixo ?? 0:n}";

            // OLHO ESQUERDO
            tbEsfericoOELonge.Text = $"{consulta?.receita?.olhoEsquerdoLonge?.esferico ?? 0:n}";
            tbCilindroOELonge.Text = $"{consulta?.receita?.olhoEsquerdoLonge?.cilindro ?? 0:n}";
            tbAdicaoOELonge.Text = $"{consulta?.receita?.olhoEsquerdoLonge?.adicao ?? 0:n}";
            tbEixoOELonge.Text = $"{consulta?.receita?.olhoEsquerdoLonge?.eixo ?? 0:n}";

            #endregion

            #region PERTO

            // OLHO DIREITO
            tbEsfericoODPerto.Text = $"{consulta?.receita?.olhoDireitoPerto?.esferico ?? 0:n}";
            tbCilindroODPerto.Text = $"{consulta?.receita?.olhoDireitoPerto?.cilindro ?? 0:n}";
            tbAdicaoODPerto.Text = $"{consulta?.receita?.olhoDireitoPerto?.adicao ?? 0:n}";
            tbEixoODPerto.Text = $"{consulta?.receita?.olhoDireitoPerto?.eixo ?? 0:n}";

            // OLHO ESQUERDO
            tbEsfericoOEPerto.Text = $"{consulta?.receita?.olhoEsquerdoPerto?.esferico ?? 0:n}";
            tbCilindroOEPerto.Text = $"{consulta?.receita?.olhoEsquerdoPerto?.cilindro ?? 0:n}";
            tbAdicaoOEPerto.Text = $"{consulta?.receita?.olhoEsquerdoPerto?.adicao ?? 0:n}";
            tbEixoOEPerto.Text = $"{consulta?.receita?.olhoEsquerdoPerto?.eixo ?? 0:n}";

            #endregion
        }

        #endregion

        #region MANTEM DADOS

        #region  TO MODEL

        private ConsultaModel ToModel() =>
            new ConsultaModel
            {
                id = consulta?.id ?? 0,
                cliente = ToModelCliente(),
                ativo = true,
                convenio = cbConvenio.SelectedIndex > 0 ? convenios[cbConvenio.SelectedIndex] : null,
                data = tbData.SelectedDate.Value,
                horario = TimeSpan.Parse(tbHorario.Text),
                medico = oftals[cbMedicos.SelectedIndex],
                numeroProcedimento = tbNumeroProcediemnto.Text,
                retorno = cbRetorno.SelectedIndex == 1,
                valor = decimal.Parse(tbValor.Text),
                formaDeAtentimento = cbTipo.SelectedIndex == 0 ? FormaDeAtendimento.Particular : FormaDeAtendimento.Convenio,
                statusPagamento = cbStatusPagamento.SelectedIndex == 0 ? StatusPagamento.Pendente : StatusPagamento.Recebido,
                tipoDeConsulta = cbRetorno.SelectedIndex == 1 ? TipoDeConsulta.Retorno : TipoDeConsulta.Confirmada,

                cirgurgia = ToModelCirurgia(),
                receita = ToModelReceita()
            };

        private EnderecoModel ToModelEndereco() =>
            new EnderecoModel
            {
                id = consulta?.cliente?.endereco?.id ?? 0,
                cep = Mascara.Remover(tbCep.Text),
                cidade = tbCidade.Text,
                uf = cbUf.Text,
                bairro = tbBairro.Text,
                logradouro = tbLogradouro.Text,
                numero = tbNumero.Text,
                complemento = tbComplemento.Text
            };

        private ClienteModel ToModelCliente() =>
            new ClienteModel
            {
                id = consulta?.cliente?.id ?? 0,
                nome = tbNome.Text,
                cpf = Mascara.Remover(tbCpf.Text),
                genero = cbGenero.SelectedIndex == 0 ? Genero.Masculino : Genero.Feminino,
                dataDeNascimento = DataUtil.Converter(tbDataNascimento.Text),

                dddCel = tbDddCel.Text.Trim(),
                celular = tbCelular.Text,
                dddTel = tbDddTel.Text.Trim(),
                telefone = tbTelefone.Text,
                email = tbEmail.Text,

                endereco = ToModelEndereco()
            };

        #region CIRURGIA

        private CirurgiaModel ToModelCirurgia() =>
            new CirurgiaModel
            {
                id = consulta?.cirgurgia?.id ?? 0,
                idConsulta = consulta?.id ?? 0,
                local = tbLocal.Text,
                valor = decimal.Parse(tbValorMedico.Text != string.Empty ? tbValorMedico.Text : "0"),
                ativo = true,
            };

        #endregion

        #region TO MODEL RECEITA

        private ReceitaModel ToModelReceita() =>
            new ReceitaModel
            {
                id = consulta?.receita?.id ?? 0,
                idConsulta = consulta?.id ?? 0,
                olhoDireitoLonge = ToModelOlhoDireitoLonge(),
                olhoEsquerdoLonge = ToModelOlhoEsquerdoLonge(),
                olhoDireitoPerto = ToModelOlhoDireitoPerto(),
                olhoEsquerdoPerto = ToModelOlhoEsquerdoPerto(),
                ativo = true,
            };

        #region LONGE

        private DiagnosticoModel ToModelOlhoDireitoLonge() =>
            new DiagnosticoModel
            {
                id = consulta?.receita?.olhoDireitoLonge?.id ?? 0,
                adicao = tbAdicaoODLonge.Text != string.Empty ? decimal.Parse(tbAdicaoODLonge.Text) : 0,
                cilindro = tbCilindroODLonge.Text != string.Empty ? decimal.Parse(tbCilindroODLonge.Text) : 0,
                eixo = tbEixoODLonge.Text != string.Empty ? decimal.Parse(tbEixoODLonge.Text) : 0,
                esferico = tbEsfericoODLonge.Text != string.Empty ? decimal.Parse(tbEsfericoODLonge.Text) : 0,
                ativo = true,
            };

        private DiagnosticoModel ToModelOlhoEsquerdoLonge() =>
            new DiagnosticoModel
            {
                id = consulta?.receita?.olhoEsquerdoLonge?.id ?? 0,
                adicao = tbAdicaoOELonge.Text != string.Empty ? decimal.Parse(tbAdicaoOELonge.Text) : 0,
                cilindro = tbCilindroOELonge.Text != string.Empty ? decimal.Parse(tbCilindroOELonge.Text) : 0,
                eixo = tbEixoOELonge.Text != string.Empty ? decimal.Parse(tbEixoOELonge.Text) : 0,
                esferico = tbEsfericoOELonge.Text != string.Empty ? decimal.Parse(tbEsfericoOELonge.Text) : 0,
                ativo = true,
            };

        #endregion

        #region PERTO

        private DiagnosticoModel ToModelOlhoDireitoPerto() =>
            new DiagnosticoModel
            {
                id = consulta?.receita?.olhoDireitoPerto?.id ?? 0,
                adicao = tbAdicaoODPerto.Text != string.Empty ? decimal.Parse(tbAdicaoODPerto.Text) : 0,
                cilindro = tbCilindroODPerto.Text != string.Empty ? decimal.Parse(tbCilindroODPerto.Text) : 0,
                eixo = tbEixoODPerto.Text != string.Empty ? decimal.Parse(tbEixoODPerto.Text) : 0,
                esferico = tbEsfericoODPerto.Text != string.Empty ? decimal.Parse(tbEsfericoODPerto.Text) : 0,
                ativo = true,
            };

        private DiagnosticoModel ToModelOlhoEsquerdoPerto() =>
            new DiagnosticoModel
            {
                id = consulta?.receita?.olhoEsquerdoPerto?.id ?? 0,
                adicao = tbAdicaoOEPerto.Text != string.Empty ? decimal.Parse(tbAdicaoOEPerto.Text) : 0,
                cilindro = tbCilindroOEPerto.Text != string.Empty ? decimal.Parse(tbCilindroOEPerto.Text) : 0,
                eixo = tbEixoOEPerto.Text != string.Empty ? decimal.Parse(tbEixoOEPerto.Text) : 0,
                esferico = tbEsfericoOEPerto.Text != string.Empty ? decimal.Parse(tbEsfericoOEPerto.Text) : 0,
                ativo = true,
            };

        #endregion

        #endregion

        #endregion

        private bool NaoExisteConsulta()
        {
            if (ConsultaModel.ExisteConsulta(tbData.SelectedDate.Value, TimeSpan.Parse(tbHorario.Text), consulta?.id ?? 0))
            {
                Mbox.Afirmacao("Aviso", "Já existe uma consulta agendada na data e horário informado!");
                return false;
            }

            return true;
        }

        private void MantemDados()
        {
            consulta = ToModel();

            if (cadastrar)
            {
                if (carregouCliente)
                    consulta.cliente.Atualizar();
                else
                    consulta.cliente.Cadastrar();

                consulta.Cadastrar();

                consulta.cirgurgia.idConsulta = consulta.id;
                consulta.receita.idConsulta = consulta.id;

                consulta.cirgurgia.Cadastrar();
                consulta.receita.CadastrarComDiagnostico();
            }
            else
            {
                consulta.cliente.Atualizar();
                consulta.Atualizar();
                consulta.cirgurgia.Atualizar();
                consulta.receita.AtualizarComDiagnostico();
            }
        }

        #endregion

        #region EVENTOS

        private void ProcurarPaciente_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var frmVincular = new Clientes.ProcurarClienteWin();
            frmVincular.ShowDialog();

            if (frmVincular.Vinculou)
            {
                var cliente = frmVincular.Cliente;
                CarregarCliente(cliente);
                consulta = new ConsultaModel { cliente = cliente };
                carregouCliente = true;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        private void BtnSalvar_Click(object sender, RoutedEventArgs e)
        {
            if (ValidarCampos.Validar(this))
                if (NaoExisteConsulta())
                {
                    MantemDados();
                    cadastrou = true;
                    Close();
                }
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e) => Close();

        #endregion

        #region EVENTOS PAGAMENTO

        private void BtnAdicionar_Click(object sender, RoutedEventArgs e)
        {
            var frm = new Pagamento.CadPagamentoWin(consulta.id);
            frm.ShowDialog();

            if (frm.cadastrou)
            {
                CarregarPagamentos();
                CarregarLabelExtrato();
            }
        }

        private void BtnRemover_Click(object sender, RoutedEventArgs e)
        {
            if (lvwPagamentos.SelectedIndex >= 0)
            {
                var r = Mbox.DesejaExcluir();

                if (r == MessageDialogResult.Affirmative)
                {
                    var pagamento = lvwPagamentos.SelectedItems[0] as PagamentoModel;
                    pagamento?.Remover();

                    pagamentos.Remove(pagamento);
                    lvwPagamentos.Items.Refresh();
                    CarregarLabelExtrato();
                }
            }
            else
                Mbox.SelecioneUmaLinhaDaTabela();
        }

        #endregion

        #region EVENTOS CIRURGIA

        private void btnAddEquipe_Click(object sender, RoutedEventArgs e)
        {
            var cadEquipe = new Equipe.CadEquipeWin(consulta?.cirgurgia);
            cadEquipe.ShowDialog();

            if (cadEquipe.cadastrou)
                CarregarEquipe();
        }

        private void btnRemEquipe_Click(object sender, RoutedEventArgs e)
        {
            if (lvwEquipe.SelectedIndex >= 0)
            {
                var r = Mbox.DesejaExcluir();

                if (r == MessageDialogResult.Affirmative)
                {
                    var equipe = lvwEquipe.SelectedItems[0] as EquipeCirurgiaModel;
                    equipe?.Remover();

                    equipes.Remove(equipe);
                    lvwEquipe.Items.Refresh();
                    tbTotalEquipe.Content = $"{equipes.Count:D2}";
                }
            }
            else
                Mbox.SelecioneUmaLinhaDaTabela();
        }

        private void btnAddMaterial_Click(object sender, RoutedEventArgs e)
        {
            var cadMaterial = new Material.CadMaterialCirurgiaWin(consulta?.cirgurgia);
            cadMaterial.ShowDialog();

            if (cadMaterial.cadastrou)
                CarregarMaterial();
        }

        private void btnRemMaterial_Click(object sender, RoutedEventArgs e)
        {
            if (lvwMaterial.SelectedIndex >= 0)
            {
                var r = Mbox.DesejaExcluir();

                if (r == MessageDialogResult.Affirmative)
                {
                    var material = lvwMaterial.SelectedItems[0] as MaterialCirurgiaModel;
                    material?.Remover();

                    materiais.Remove(material);
                    lvwMaterial.Items.Refresh();
                    tbTotalMaterial.Content = $"{materiais.Sum(x => x.valorTotal):n}";
                }
            }
            else
                Mbox.SelecioneUmaLinhaDaTabela();
        }

        #endregion

        #region EVENTOS DIAGNOSTICO

        private void btnImprimirReceita_Click(object sender, RoutedEventArgs e)
        {
            SplashScreenControle.Mostrar();

            var x = new TimerUtil(TimeSpan.FromMilliseconds(300),
                                 (o, a) =>
                                 {
                                     new Relatorios.Receita.RelReceitaWin(consulta?.id ?? 0).ShowDialog();
                                     var trm = o as DispatcherTimer;
                                     trm.IsEnabled = false;
                                 });
        }

        #endregion
    }
}
