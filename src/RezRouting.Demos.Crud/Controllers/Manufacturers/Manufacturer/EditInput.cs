﻿using System.ComponentModel.DataAnnotations;

namespace RezRouting.Demos.Crud.Controllers.Manufacturers.Manufacturer
{
    /// <summary>
    /// The values submitted to edit a manufacturer
    /// </summary>
    public class EditInput
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}