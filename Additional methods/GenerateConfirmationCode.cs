﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvrydayBook.Additional_methods
{
    public class GenerateConfirmationCode
    {
        public static string GenerateCode()
        {
            Random random = new Random();
            return random.Next(10000, 99999).ToString();            
        }
    }
}
