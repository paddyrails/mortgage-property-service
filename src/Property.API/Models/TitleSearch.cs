using System.ComponentModel.DataAnnotations;

namespace Property.API.Models;

public class TitleSearch
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public Guid PropertyId { get; set; }

    [Required]
    public DateTime SearchDate { get; set; }

    [StringLength(100)]
    public string TitleCompany { get; set; } = string.Empty;

    [StringLength(50)]
    public string CaseNumber { get; set; } = string.Empty;

    public TitleStatus Status { get; set; } = TitleStatus.Pending;

    public bool HasLiens { get; set; }

    [StringLength(500)]
    public string? LienDetails { get; set; }

    public bool HasEasements { get; set; }

    [StringLength(500)]
    public string? EasementDetails { get; set; }

    public bool HasEncumbrances { get; set; }

    [StringLength(500)]
    public string? EncumbranceDetails { get; set; }

    public bool IsClear { get; set; }

    [StringLength(1000)]
    public string? Comments { get; set; }

    // Navigation
    public Property? Property { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}

public enum TitleStatus
{
    Pending = 1,
    InProgress = 2,
    Clear = 3,
    IssuesFound = 4,
    Resolved = 5
}
