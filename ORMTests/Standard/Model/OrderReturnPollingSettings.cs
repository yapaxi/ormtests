﻿using System;
using LinqToDB.Mapping;

namespace R1.MarketplaceManagement.OrderReturnService.DataAccess.Model
{
    [Table(Schema = "dbo", Name = "OrderReturnPollingSettings")]
    [System.ComponentModel.DataAnnotations.Schema.Table("OrderReturnPollingSettings")]
    public class OrderReturnPollingSettings
    {
        [PrimaryKey]
        [System.ComponentModel.DataAnnotations.Key]
        public virtual int SellingVendorId { get; set; }

        [Column, Nullable]
        public virtual DateTime? OrdersPolledOn { get; set; }
    }
}