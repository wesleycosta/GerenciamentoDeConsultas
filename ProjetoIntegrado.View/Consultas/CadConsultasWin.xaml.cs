using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

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

            Title = "EDITAR CONSULTA";
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
            tbValor.KeyUp += ValidarEntrada.Real_KeyPress;
            imgProcurar.BitmapToImageSource(Icons.Find_16x16);

            tbData.SelectedDate = DateTime.Now;

            CarregarOftals();
            CarregarConvenios();

            tbCpf.LostFocus += (o, a) =>
            {
                if (cadastrar && !carregouCliente)
                    CarregarPorCPF();
            };
        }

        #endregion

        #region CARREGAR DADOS NA TELA

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
                olhoDireito = ToModelOlhoDireito(),
                olhoEsquerdo = ToModelOlhoEsquerdo(),
                ativo = true,
            };

        private DiagnosticoModel ToModelOlhoDireito() =>
            new DiagnosticoModel
            {
                id = consulta.receita?.olhoDireito?.id ?? 0,
                categoria = new CategoriaModel { id = 1 },
                adicao = 1,
                cilindro = 2,
                eixo = 3,
                esferico = 4,
                ativo = true,
            };

        private DiagnosticoModel ToModelOlhoEsquerdo() =>
            new DiagnosticoModel
            {
                id = consulta.receita?.olhoEsquerdo?.id ?? 0,
                categoria = new CategoriaModel { id = 1 },
                adicao = 1,
                cilindro = 2,
                eixo = 3,
                esferico = 4,
                ativo = true,
            };

        #endregion

        #endregion

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
                CarregarEquipe();
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
                    tbTotalEquipe.Content = $"{materiais.Count:n}";
                }
            }
            else
                Mbox.SelecioneUmaLinhaDaTabela();
        }

        #endregion
    }
}
