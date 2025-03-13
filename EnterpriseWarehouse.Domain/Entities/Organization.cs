using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnterpriseWarehouse.Domain.Entities;
/// <summary>
/// Организация
/// </summary>
[Table("organization")]
public class Organization
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    [Key]
    [Column("id")]
    public int Id { get; set; }
    /// <summary>
    /// Название
    /// </summary>
    [Column("name")]
    [MaxLength(50)]
    [Required]
    public required string Name { get; set; }
    /// <summary>
    /// Адрес
    /// </summary>
    [Column("address")]
    [MaxLength(50)]
    [Required]
    public required string Address { get; set; }
    ///<summary>
    /// Геометрия в виде полигона
    /// </summary>
    [Column("geometry")]
    [Required]
    public required Polygon Geometry { get; set; }
}

