﻿using System.ComponentModel.DataAnnotations;

namespace HITSBackEnd.Models.OrdersModels
{
    public class OrdersTable
    {
        [Key]
        public Guid Id { get; set; }
        [Key]
        public string UserEmail { get; set; }
        public DateTime DeliveryTime { get; set; }
        public DateTime OrderTime { get; set; }
        public Status Status { get; set; }
        public double Price { get; set; }
        public Guid Adress { get; set; }
    }
}
