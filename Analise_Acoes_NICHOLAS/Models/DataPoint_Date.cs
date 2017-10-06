﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Analise_Acoes_NICHOLAS.Models
{
    //DataContract for Serializing Data - required to serve in JSON format
    [DataContract]
    public class DataPoint_Date
    {
        public DataPoint_Date(DateTime x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        //Explicitly setting the name to be used while serializing to JSON.
        [DataMember(Name = "x")]
        public Nullable<DateTime> X = null;

        //Explicitly setting the name to be used while serializing to JSON.
        [DataMember(Name = "y")]
        public Nullable<double> Y = null;
    }
}
