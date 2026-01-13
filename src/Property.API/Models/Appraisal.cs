using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Property.API.Models;

public class Appraisal
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public Guid PropertyId { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal AppraisedValue { get; set; }

    [Required]
    public DateTime AppraisalDate { get; set; }

    [Required]
    [StringLength(100)]
    public string AppraiserName { get; set; } = string.Empty;

    [StringLength(100)]
    public string AppraisalCompany { get; set; } = string.Empty;

    [StringLength(50)]
    public string LicenseNumber { get; set; } = string.Empty;

    public AppraisalStatus Status { get; set; } = AppraisalStatus.Pending;

    [Column(TypeName = "decimal(18,2)")]
    public decimal? LandValue { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? ImprovementValue { get; set; }

    [StringLength(1000)]
    public string? Comments { get; set; }

    [StringLength(500)]
    public string? ConditionReport { get; set; }

    // Navigation
    public Property? Property { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}

public enum AppraisalStatus
{
    Pending = 1,
    Scheduled = 2,
    InProgress = 3,
    Completed = 4,
    Disputed = 5
}
