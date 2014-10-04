﻿using System.ComponentModel.DataAnnotations;

namespace RezRouting.Demos.Crud.Controllers.Products
{
    /// <summary>
    /// The values submitted to create a product
    /// </summary>
    public class CreateInput
    {
        [Required]
        public int? ManufacturerId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}