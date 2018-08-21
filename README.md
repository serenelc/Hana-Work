# HANA Work
Things I've worked on and learned about at Hana during my summer internship there.
Main project is to create a survey template (along the lines of google forms) so that staff in HR can create surveys about employee satisfaction in various areas and then get other members of staff to fill it out and view statistics. I am working on this project with one of the senior systems analyst.

TO DO:
- IN PROGRESS: Add a section delete button on adminCreate -> add some sort of flag to each question so we know which section it is part of, then we can delete all the questions within the section.
- Fix the issue on adminCreate where if you add many questions in a row without choosing answer types, when you click on the last question to add an answer, the answer type will be assigned to the top available question.
- Convert e.g. %3f into ? or %27 into '
- IN PROGRESS: Make a page to show graphs and summaries of an individual survey
- Make the survey close date only in the future on adminCreate
- Have a manual button so the admin can close the survey before the chosen close date if desired
- IN PROGRESS: Have an option so the admin can choose who they want to send the email to -> mail button will go to another page where there is a list of all employees and their emails. admin can select users from that list to email about that specific survey.
- IN PROGRESS: Staff in HR requested that the sections look more colourful and prettier.