using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnterpriseWarehouse.Domain.Entities;
/// <summary>
/// Поставка товара
/// </summary>
[Table("supply")]
public class Supply
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    [Key]
    [Column("id")]
    public int Id { get; set; }
    /// <summary>
    /// Товар
    /// </summary>
    [Column("product")]
    [Required]
    public required Product Product { get; set; }
    /// <summary>
    /// Организация
    /// </summary>
    [Column("organization")]
    [Required]
    public required Organization Organization { get; set; }
    /// <summary>
    /// Дата поставки
    /// </summary>
    [Column("supply_date")]
    [Required]
    public required DateTime SupplyDate { get; set; }
    /// <summary>
    /// Количество поставленного товара
    /// </summary>
    [Column("quantity")]
    [Required]
    public required int Quantity { get; set; }
}

