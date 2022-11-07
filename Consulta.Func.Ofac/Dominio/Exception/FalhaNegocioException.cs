namespace Consulta.Func.Ofac.Dominio.Exception;

public class FalhaNegocioException
{
    public string ErrorName { get; set; } = "Erro de validação.";
    public string ErrorMessage { get; set; }

    public override string ToString()
    {
        return this.ErrorMessage;
    }
}