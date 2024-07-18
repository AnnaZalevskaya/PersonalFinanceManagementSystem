﻿using Accounts.BusinessLogic.Models.Enums;

namespace Accounts.BusinessLogic.Models
{
    public class FinancialGoalModel
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string? Name { get; set; }
        public int TypeId { get; set; }
        public FinancialGoalTypeModel? Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double Amount { get; set; }
        public double Progress { get; set; }
        public GoalStatus Status { get; set; }
    }
}
