﻿using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R1.MarketplaceManagement.OrderReturnService.DataAccess.Model
{
    [System.ComponentModel.DataAnnotations.Schema.Table("OrderReturnRawData")]
    public class OrderReturnRawData
    {
        [PrimaryKey, NotNull]
        public int Id { get; set; }

        [Column, NotNull]
        public string Raw { get; set; }
    }
}
