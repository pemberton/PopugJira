﻿using System;

namespace NotificationService.BO
{
    public class PopugTask
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
    }
}