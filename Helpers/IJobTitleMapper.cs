using ir_coding_task_csv_validator.Models;

namespace ir_coding_task_csv_validator.Helpers
{
    /// <summary>
    /// Interface for mapping job codes to job titles.
    /// </summary>
    public interface IJobTitleMapper
    {
        /// <summary>
        /// Initializes the job title mapper with a list of jobs from CSV.
        /// </summary>
        /// <param name="jobsFromCsv">Mapped data from the csv</param>
        void Initialize(List<Job> jobsFromCsv);

        /// <summary>
        /// Validates if the given job code exists in the mapping.
        /// </summary>
        /// <param name="jobCode">Code to find</param>
        /// <returns>True if valid, otherwise false</returns>
        bool IsValidJobCode(int jobCode);

        /// <summary>
        /// Maps the given job code to its corresponding job title.
        /// </summary>
        /// <param name="jobCode">Code to find</param>
        /// <returns>Corresponding job title</returns>
        string MapJobTitle(int jobCode);
    }
}