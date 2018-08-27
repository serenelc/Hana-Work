# HANA Work
Things I've worked on and learned about at Hana during my summer internship there.
Main project is to create a survey template (along the lines of google forms) so that staff in HR can create surveys about employee satisfaction in various areas and then get other members of staff to fill it out and view statistics. I am working on this project with one of the senior systems analyst.
Using vb.net, sql, html, css, asp.net and javascript.

TO DO:
- IN PROGRESS: Add a section delete button on adminCreate -> add some sort of flag to each question so we know which section it is part of, then we can delete all the questions within the section.
- Fix the issue on adminCreate where if you add many questions in a row without choosing answer types, when you click on the last question to add an answer, the answer type will be assigned to the top available question.
- IN PROGRESS: Graphs and summaries of data 
	-> make it look pretty
	-> table with percentages
	-> Question titles per section
	-> Section titles more obvious
	-> If there are 2 radio questions in a row, it condenses it into 1 pie chart
- IN PROGRESS: Make a user manual and program process flow
- Make all the alerts uniform across all pages
- Make sure each survey has at least 1 question before it allows itself to be saved
- Today's date is fucking up. Calendar is allowing users to pick dates in the past.
- Email confirmation pop up