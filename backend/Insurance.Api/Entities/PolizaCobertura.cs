namespace Insurance.Api.Entities;

public class PolizaCobertura
{
    public int Id { get; set; }
    public int PolizaId { get; set; }
    public int CoberturaId { get; set; }
    public decimal MontoAplicado { get; set; }

    public Poliza Poliza { get; set; } = null!;
    public Cobertura Cobertura { get; set; } = null!;
}
