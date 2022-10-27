﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
   public class Producto
    {
        public string Codigo { get; set; }
        public string Descrpcion { get; set; }
        public int Existencia { get; set; }
        public decimal Precio { get; set; }
        public DateTime FechaCreacion { get; set; }
        public byte[] Imagen { get; set; }
    }
}
