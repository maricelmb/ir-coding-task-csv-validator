using ir_coding_task_csv_validator.Models;

namespace ir_coding_task_csv_validator.Helpers
{
    public interface IJobTitleMapper
    {
        void Initialize(List<Job> jobsFromCsv);
        bool IsValidJobCode(int jobCode);
        string MapJobTitle(int jobCode);
    }
}