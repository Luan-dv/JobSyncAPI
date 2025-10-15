using FluentValidation;
using FluentValidation.Validators;
using JobSync.Exception;
using System.Text.RegularExpressions;

namespace JobSync.Aplication.UseCases.Users;

/// <summary>
/// Validador de CPF customizado para o FluentValidation.
/// Garante que um CPF é válido, verificando o formato, dígitos repetidos e calculando os dígitos verificadores.
/// </summary>
/// <typeparam name="T">O tipo da classe que contém a propriedade CPF.</typeparam>
public partial class CpfValidator<T> : PropertyValidator<T, string>
{
    private const string ERROR_MESSAGE_KEY = "ErrorMessage";

    /// <summary>
    /// Obtém o nome do validador.
    /// </summary>
    public override string Name => "CpfValidator";

    /// <summary>
    /// Retorna o template da mensagem de erro padrão.
    /// </summary>
    /// <param name="errorCode">O código de erro opcional.</param>
    /// <returns>O template da mensagem de erro.</returns>
    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return $"{{{ERROR_MESSAGE_KEY}}}";
    }

    /// <summary>
    /// Executa a lógica de validação do CPF.
    /// </summary>
    /// <param name="context">O contexto de validação atual.</param>
    /// <param name="cpf">A string do CPF a ser validada.</param>
    /// <returns>Retorna <c>true</c> se o CPF for válido; caso contrário, <c>false</c>.</returns>
    public override bool IsValid(ValidationContext<T> context, string cpf)
    {
        var cleanedCpf = CleanCpf(cpf);

        if (string.IsNullOrWhiteSpace(cleanedCpf) || cleanedCpf.Length != 11)
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, ResourceErrorMessages.EMAIL_REQUIRED);
            return false;
        }

        if (IsRepeatedDigits(cleanedCpf))
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, ResourceErrorMessages.EMAIL_REQUIRED);
            return false;
        }

        if (!IsCpfValid(cleanedCpf))
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, ResourceErrorMessages.EMAIL_REQUIRED);
            return false;
        }

        return true;
    }

    /// <summary>
    /// Remove todos os caracteres não numéricos de uma string de CPF.
    /// </summary>
    /// <param name="cpf">A string do CPF original.</param>
    /// <returns>A string do CPF contendo apenas dígitos numéricos.</returns>
    private static string CleanCpf(string cpf)
    {
        return Regex.Replace(cpf, "[^0-9]", "");
    }

    /// <summary>
    /// Verifica se todos os dígitos de um CPF são repetidos (ex: 111.111.111-11).
    /// </summary>
    /// <param name="cpf">A string do CPF limpa (apenas números).</param>
    /// <returns>Retorna <c>true</c> se os dígitos forem repetidos; caso contrário, <c>false</c>.</returns>
    private static bool IsRepeatedDigits(string cpf)
    {
        if (string.IsNullOrEmpty(cpf) || cpf.Length < 11)
            return false;

        return new string(cpf[0], 11) == cpf;
    }

    /// <summary>
    /// Executa a validação matemática do CPF, calculando os dígitos verificadores.
    /// </summary>
    /// <param name="cpf">A string do CPF limpa (apenas números).</param>
    /// <returns>Retorna <c>true</c> se os dígitos verificadores calculados corresponderem aos dígitos finais do CPF; caso contrário, <c>false</c>.</returns>
    private static bool IsCpfValid(string cpf)
    {
        int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        string tempCpf;
        string digito;
        int soma;
        int resto;

        tempCpf = cpf.Substring(0, 9);
        soma = 0;

        for (int i = 0; i < 9; i++)
        {
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
        }

        resto = soma % 11;
        if (resto < 2)
        {
            resto = 0;
        }
        else
        {
            resto = 11 - resto;
        }

        digito = resto.ToString();
        tempCpf = tempCpf + digito;
        soma = 0;

        for (int i = 0; i < 10; i++)
        {
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
        }

        resto = soma % 11;
        if (resto < 2)
        {
            resto = 0;
        }
        else
        {
            resto = 11 - resto;
        }

        digito = digito + resto.ToString();

        return cpf.EndsWith(digito);
    }
}