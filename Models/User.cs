namespace ir_coding_task_csv_validator.Models
{
    public class User
    {  
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }        
        public string Email { get; set; } = string.Empty;
        public Int64 PhoneNumber { get; set; }        
        public int CardNumber { get; set; }        
        public string State { get; set; } = string.Empty;
        public string Postcode { get; set; } = string.Empty;
        public int Job_Code { get; set; }     
        public decimal Salary { get; set; }
        public string Notes { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
    }
}
