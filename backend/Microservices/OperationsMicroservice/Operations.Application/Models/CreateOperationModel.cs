﻿namespace Operations.Application.Models
{
    public class CreateOperationModel
    {
        public int AccountId { get; set; }
        public Dictionary<string, object> Description { get; set; }
    }
}
