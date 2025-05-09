﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Event_Applikation.Models;

[Table("Nyheder")]
public partial class Nyheder
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(45)]
    public string Indhold { get; set; }

    [Required]
    [StringLength(45)]
    public string BeskedType { get; set; }

    public int EventId { get; set; }

    [ForeignKey("EventId")]
    [InverseProperty("Nyheders")]
    public virtual Event Event { get; set; }
}