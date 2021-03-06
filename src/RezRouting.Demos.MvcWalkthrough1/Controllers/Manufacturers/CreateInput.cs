﻿using System.ComponentModel.DataAnnotations;

namespace RezRouting.Demos.MvcWalkthrough1.Controllers.Manufacturers
{
    /// <summary>
    /// The values submitted to create a manufacturer
    /// </summary>
    public class CreateInput
    {
        [Required]
        public string Name { get; set; }
    }
}