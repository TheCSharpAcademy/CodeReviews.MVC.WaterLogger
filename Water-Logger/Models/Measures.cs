using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace Water_Logger.Models;

public static class Measures
{
    [Display(Name = "Glass")]
    public static double Glass { get; set; } = 0.5;
    [Display(Name = "Bottle")]
    public static double Bottle { get; set; } = 1;
    [Display(Name = "Big Bottle")]
    public static double Big_Bottle { get; set; } = 2;
}
