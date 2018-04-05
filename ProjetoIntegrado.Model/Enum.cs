namespace ProjetoIntegrado.Model
{
    public enum Genero
    {
        Feminino = 0,
        Masculino = 1
    }

    public enum FormaDeAtendimento
    {
        Particular = 0,
        Convenio = 1,
        Todos
    };

    public enum StatusPagamento
    {
        Recebido = 0,
        Pendente = 1,
        Todos = 2
    };

    public enum TipoDeConsulta
    {
        Retorno = 0,
        Cancelado = 1,
        NaoCompareceu = 2,
        Confirmada = 3
    };

    public enum TipoDeCancelamento
    {
        NaoCompareceu = 0,
        Cancelado = 1
    };

    public enum FiltroPessoa
    {
        nome,
        cpf,
        telefone,
        celular,
        email
    };
}