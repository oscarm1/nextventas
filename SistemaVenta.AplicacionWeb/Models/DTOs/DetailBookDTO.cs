﻿using SistemaVenta.Entity;

namespace SistemaVenta.AplicacionWeb.Models.DTOs
{
    public class DetailBookDTO
    {
        public int IdDetailBook { get; set; }
        public int IdBook { get; set; }
        public int IdRoom { get; set; }
        public int IdGuest { get; set; }
       // public decimal? Price { get; set; }
        public decimal? Total { get; set; }


        // public Book Book { get; set; }

        // public Room Room { get; set; }
        //public Guest Guest { get; set; }
    }
}
