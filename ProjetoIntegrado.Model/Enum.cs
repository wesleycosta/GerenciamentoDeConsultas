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
        Convenio = 1
    };

    public enum StatusPagamento
    {
        Recebido = 0,
        Pendente = 1
    };

    public enum TipoDeCancelamento
    {
        Cancelado = 0,
        NaoCompareceu = 1
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