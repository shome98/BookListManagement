﻿using System.ComponentModel.DataAnnotations;

namespace BookListManagement.Server.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public string Title {  get; set; }  
        public string Author {  get; set; } 
    }
}
