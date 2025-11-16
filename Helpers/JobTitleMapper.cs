using ir_coding_task_csv_validator.Models;

namespace ir_coding_task_csv_validator.Helpers
{
    public class JobTitleMapper : IJobTitleMapper
    {
        Dictionary<int, string> mappedJobs = new();
        public JobTitleMapper()
        { }

        public void Initialize(List<Job> jobsFromCsv)
        {
            foreach (var job in jobsFromCsv)
            {
                mappedJobs.Add(job.JobCode, job.JobTitle);
            }
        }

        public bool IsValidJobCode(int jobCode)
        {
            return mappedJobs.ContainsKey(jobCode);
        }

        public string MapJobTitle(int jobCode)
        {
            mappedJobs.TryGetValue(jobCode, out string jobTitle);
            return string.IsNullOrEmpty(jobTitle) ? string.Empty : jobTitle;
        }
    }
}
