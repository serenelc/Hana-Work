# HANA Work
Things I've worked on and learned about at Hana during my summer internship there.
Main project is to create a survey template (along the lines of google forms) so that staff in HR can create surveys about employee satisfaction in various areas and then get other members of staff to fill it out and view statistics. I am working on this project with one of the senior systems analyst.

TO DO:
- IN PROGRESS: Add a section delete button on adminCreate
- Fix the issue on adminCreate where if you add many questions in a row without choosing answer types, when you click on the last question to add an answer, the answer type will be assigned to the top available question.
- Fix the issue on adminCreate where if you've chosen an answer type you cannot change the answer type, you have to delete the question and create a new one.
- Convert e.g. %3f into ? or %27 into '
- Make a page to show graphs and summaries of an individual survey
- Make the survey close date only in the future on adminCreate
- Have a manual button so the admin can close the survey before the chosen close date if desired
- Have a button so the survey link gets sent to all desired employees
- IN PROGRESS: Create a new page so that once a user presses submit, it shows them a successful message and a back to userSurveyList button.
