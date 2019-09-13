﻿using System;
using System.Collections.Generic;

namespace iWasHere.Web.Domain
{
    public partial class DictionaryCurrency
    {
        public DictionaryCurrency()
        {
            Ticket = new HashSet<Ticket>();
        }

        public int DictionaryCurrencyId { get; set; }
        public string CurrencyCode { get; set; }

        public virtual ICollection<Ticket> Ticket { get; set; }
    }
}
