# Pre – Interview Programming Task
## Task Duration:	
We understand your time is valuable, please don’t spend more than 1-2 hours on this task.


## Purpose & Objective:  
A significant portion of the technical interview will be a code review of your submission.

We are looking for a production quality design that is in your opinion, elegant, extensible and maintainable.

We would rather see half the job done well with a good clean code design.

**The task definition is intentionally a bit vague, if you are uncertain about anything, make some sensible assumptions. Submit any assumptions you make with your response.**

## The Task:
A skeleton (non-functional) Web API application based on .Net Core Web API has been provided.
Implement the 'Validate' endpoint of the 'Validator' controller to:
* Parse all columns of the data in 'Sample_Data.csv' into a class
* Set a Job Title property in this class based on the data provided in 'Job_Title_Mappings.csv'
* Validate the data according to the below validation rules
* Return the results data and validation warnings/errors

Validation rules:
* Produce a warning if names are less than four characters
* Produce an error if the code denoting the job title is not in 'Job_Title_Mappings.csv'
* Produce a warning if the salary is not a positive integer
* Produce an error if no post code exists
* Produce a warning if the postcode is not valid for the state

NOTE: The use of AI tools in completing this coding test is acceptable, however please make sure to notify us of any tools that have been used. Ensure that you are able to fully explain any code used in your submission.