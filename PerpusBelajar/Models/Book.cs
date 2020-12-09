using PerpusBelajar.Constant;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace PerpusBelajar.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        [MinLength(13, ErrorMessage = "ISBN must be 13 characters")]
        [MaxLength(13,ErrorMessage = "ISBN must be 13 characters")]
        public string ISBN { get; set; }
        [Required]
        [MaxLength(100,ErrorMessage = "Title cannot exceed 100 characters")]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        public int Quantity { get; set; }
        public string ImageFileName { get; set; }
        [Required]
        public BookCategory? Category { get; set; }
    }
}
