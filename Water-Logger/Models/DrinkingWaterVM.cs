using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Water_Logger.Models;

public class DrinkingWaterVM
{
    public  DrinkingWater DrinkingWater { get; set; } = new DrinkingWater();
    [Display(Name = "Standard Measures")]

    public IEnumerable<SelectListItem>? StandardList { get; set;} 

    [Range(0, int.MaxValue, ErrorMessage = "Value of Numbers cannot be negative")]
    [Display(Name = "Number (Standard Measure x Number)")]
    public int Numbers {get; set;} = 1;

}
